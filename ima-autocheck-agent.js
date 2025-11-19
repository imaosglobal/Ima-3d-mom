#!/usr/bin/env node
// ima-autocheck-agent.js (updated with retry on 429)
const fs = require('fs');
const path = require('path');
const axios = require('axios');

const requiredFiles = ['index.html', 'Main.js', 'style.css'];
const assetsFolder = path.join(__dirname, 'assets');
const modelFile = path.join(assetsFolder, 'model.glb');
const logFile = path.join(__dirname, 'ima-agent-log.json');
const aiModel = 'gpt-5-mini';
const MAX_RETRIES = 3;

function saveLog(entry) {
  let logs = [];
  if (fs.existsSync(logFile)) {
    try { logs = JSON.parse(fs.readFileSync(logFile)); } catch {}
  }
  logs.push({ timestamp: new Date().toISOString(), ...entry });
  fs.writeFileSync(logFile, JSON.stringify(logs, null, 2));
}

function checkLocalFiles() {
  let errors = [];
  requiredFiles.forEach(file => { if (!fs.existsSync(file)) errors.push(`Missing file: ${file}`); });
  if (!fs.existsSync(assetsFolder)) errors.push('Assets folder missing');
  if (!fs.existsSync(modelFile)) errors.push('model.glb missing');
  if (errors.length > 0) { 
    console.log('❌ Internal check failed:'); 
    errors.forEach(e => console.log(' - '+e)); 
    saveLog({type:'internal', success:false, errors}); 
    return false; 
  } else { 
    console.log('✅ Internal check passed.'); 
    saveLog({type:'internal', success:true}); 
    return true; 
  }
}

function runInternalAgents(errors) {
  if (!errors || errors.length === 0) return;
  console.log('🛠 Running internal agents to fix issues...');
  errors.forEach(err => {
    if (err.includes('Missing file')) fs.writeFileSync(err.split(': ')[1], '<!-- auto-created -->\n');
    if (err.includes('model.glb missing') || err.includes('Assets folder missing')) { 
      if (!fs.existsSync(assetsFolder)) fs.mkdirSync(assetsFolder); 
      if (!fs.existsSync(modelFile)) fs.writeFileSync(modelFile,''); 
    }
  });
  saveLog({type:'agents', action:'auto-fix applied', errors});
}

async function checkExternalAI() {
  const apiKey = process.env.OPENAI_API_KEY;
  if (!apiKey) { 
    console.log('⚠️ No API Key defined for external AI'); 
    saveLog({type:'external', success:false, error:'No API Key'}); 
    return; 
  }

  for (let attempt=1; attempt<=MAX_RETRIES; attempt++) {
    try {
      const response = await axios.post('https://api.openai.com/v1/chat/completions',{
        model: aiModel,
        messages:[
          {role:"system", content:"Review project files and suggest improvements for Ima-3d-mom."},
          {role:"user", content:`Check: ${requiredFiles.join(', ')} and ${modelFile}`}
        ]
      },{
        headers:{'Authorization':`Bearer ${apiKey}`,'Content-Type':'application/json'}
      });
      const suggestion = response.data.choices[0].message.content;
      console.log('💡 External AI suggestions:'); 
      console.log(suggestion);
      saveLog({type:'external', success:true, suggestion});
      return; // הצליח – יוצא מהלולאה
    } catch(err) {
      if (err.response && err.response.status === 429) {
        const waitTime = attempt * 2000; // 2s, 4s, 6s
        console.log(`⚠️ Rate limit hit (429). Retry #${attempt} in ${waitTime/1000}s...`);
        await new Promise(res=>setTimeout(res, waitTime));
      } else {
        console.log('⚠️ Error connecting to external AI:', err.message);
        saveLog({type:'external', success:false, error:err.message});
        return;
      }
    }
  }
}

(async function main() {
  console.log('🔗 Running Ima Autocheck Agent...');
  const ok = checkLocalFiles();
  if(!ok) { 
    const errors = JSON.parse(fs.readFileSync(logFile))
                   .filter(l=>l.type==='internal'&&!l.success)
                   .pop()?.errors;
    runInternalAgents(errors);
  }
  await checkExternalAI();
  console.log('✅ Ima Autocheck Agent finished.');
})();
