const { execSync } = require("child_process");
const fs = require("fs");

const { load, save, addMemory } = require("./kernel/memory_engine");
const stability = require("./kernel/stability");

let lastHash = "";

function hashProject() {
  return execSync("find . -type f -not -path './node_modules/*' | sort | xargs cat | sha1sum").toString();
}

function validate() {
  try {
    execSync("node -c server.js");
    const health = stability.healthCheck();
    return health.status === "ok";
  } catch (e) {
    return false;
  }
}

function commitAndPush() {
  execSync("git add .");

  const msg = "auto-sync " + Date.now();
  execSync(`git commit -m "${msg}"`);

  execSync("git push");
}

function kernelDecision(health, changed) {
  // היברידי:
  // אם המערכת יציבה → אוטומטי
  // אם יש חריגה → דורש שקט (לא push אגרסיבי)

  if (!health) return "BLOCK";

  if (changed > 5) return "SAFE_AUTO_PUSH";
  return "SAFE_AUTO_PUSH";
}

function loop() {
  try {
    const currentHash = hashProject();

    if (currentHash !== lastHash) {
      lastHash = currentHash;

      const mem = load();
      addMemory(mem, "change detected in repo", "sync_event");
      save(mem);

      const health = validate();

      const decision = kernelDecision(health, mem.memory.length);

      if (decision === "SAFE_AUTO_PUSH") {
        commitAndPush();
        console.log("🚀 AUTO PUSH DONE");
      } else {
        console.log("🛑 PUSH BLOCKED BY KERNEL");
      }
    }
  } catch (e) {
    console.log("⚠️ SYNC ERROR:", e.message);
  }
}

setInterval(loop, 5000);

console.log("🧠 IMA SYNC V2 ACTIVE");
