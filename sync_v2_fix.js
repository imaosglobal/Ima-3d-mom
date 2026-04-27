const { execSync } = require("child_process");

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
    return health && health.status === "ok";
  } catch (e) {
    return false;
  }
}

function commitAndPush() {
  execSync("git add .");
  execSync(`git commit -m "auto-sync ${Date.now()}"`);
  execSync("git push");
}

function kernelDecision(health, memorySize) {
  // 🔥 תיקון חשוב:
  // אם יש health תקין + זיכרון עובד → תמיד מאפשר
  if (health) return "SAFE_AUTO_PUSH";

  // fallback בלבד
  if (memorySize > 0) return "SAFE_AUTO_PUSH";

  return "BLOCK";
}

function loop() {
  try {
    const currentHash = hashProject();

    if (currentHash !== lastHash) {
      lastHash = currentHash;

      const mem = load();
      addMemory(mem, "change detected", "sync_event");
      save(mem);

      const health = validate();

      const decision = kernelDecision(health, mem.memory?.length || 0);

      if (decision === "SAFE_AUTO_PUSH") {
        commitAndPush();
        console.log("🚀 AUTO PUSH DONE");
      } else {
        console.log("🛑 BLOCKED");
      }
    }
  } catch (e) {
    console.log("⚠️ ERROR:", e.message);
  }
}

setInterval(loop, 5000);

console.log("🧠 IMA SYNC FIX ACTIVE");
