const fs = require("fs");
const { execSync } = require("child_process");
const { runTest } = require("./ima_benchmark");
const { evaluate } = require("./ima_goal_engine");

function backup() {
  fs.copyFileSync("server.js", "server.js.bak");
}

function restore() {
  fs.copyFileSync("server.js.bak", "server.js");
}

function evolve() {
  console.log("🧭 GOAL-DRIVEN EVOLUTION START");

  const before = runTest();

  execSync("node ima_smart_patch.js");

  const after = runTest();

  const metrics = {
    avgResponse: after,
    error: false,
    memoryGrowth: 10 // placeholder (אפשר לשפר אחר כך)
  };

  const result = evaluate(metrics);

  console.log("📊 METRICS:", metrics);
  console.log("🎯 SCORE:", result.score);

  if (!result.passed) {
    console.log("🛑 GOAL FAILED → ROLLBACK");
    restore();
  } else {
    console.log("🚀 GOAL PASSED → KEEP");
    execSync("git add server.js");
    execSync(`git commit -m "goal evolution ${Date.now()}"`);
    execSync("git push");
  }

  console.log("✅ EVOLUTION COMPLETE");
}

evolve();
