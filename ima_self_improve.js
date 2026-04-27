const fs = require("fs");
const { execSync } = require("child_process");

function loadLearning() {
  return JSON.parse(fs.readFileSync("learning_log.json"));
}

function improve() {
  console.log("🧠 IMA SELF IMPROVE START");

  const data = loadLearning();

  let improvement = `// AUTO IMPROVEMENT ${Date.now()}\n`;

  data.forEach(d => {
    improvement += `// learned from ${d.repo} → patterns: ${d.patterns}\n`;
  });

  // מוסיף ידע לקובץ kernel (לא שובר קוד)
  fs.appendFileSync("kernel/learning_insights.js", improvement);

  console.log("📚 improvement written");

  try {
    execSync("git add kernel/learning_insights.js");
    execSync(`git commit -m "self improve ${Date.now()}"`);
    execSync("git push");
    console.log("🚀 IMPROVEMENT PUSHED");
  } catch (e) {
    console.log("⚠️ GIT:", e.message);
  }

  console.log("✅ SELF IMPROVE DONE");
}

improve();
