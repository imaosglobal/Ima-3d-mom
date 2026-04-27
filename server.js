const { analyze } = require('./kernel/brain');
const { updatePersonality } = require('./kernel/personality_engine');
const { loadMemory, saveMemory, remember } = require('./kernel/memory_engine');
const express = require("express");
const cors = require("cors");
const kernel = require("./kernel/runtime");

const app = express();
app.use((req,res,next)=>{
  req._startTime = Date.now();
  res.on("finish", ()=>{
    const duration = Date.now() - req._startTime;
    console.log("⏱", req.method, req.url, duration+"ms");
  });
  next();
});
app.use(cors());
app.use(express.json());

app.post("/ask", (req, res) => { console.log("⚡ route hit", Date.now()); 
  const msg = req.body?.message || "";
  const reply = kernel.run(msg);

  res.json({ reply });
});

app.get("/health", (req,res)=>{ console.log("⚡ route hit", Date.now()); res.json({status:"alive",time:Date.now()});});

app.listen(3000, () => {
  console.log("🧠 IMA KERNEL RUNNING | optimized " + Date.now());
});
