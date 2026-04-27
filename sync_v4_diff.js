const { execSync } = require("child_process");
const { load, save, addMemory } = require("./kernel/memory_engine");
const stability = require("./kernel/stability");

let lastSnapshot = "";
let lastPush = 0;

const COOLDOWN = 30000;

function getSnapshot() {
  return execSync(
    "find . -type f -not -path './node_modules/*' -not -path './.git/*' -not -path './pm2*' | sort | xargs -I{} sh -c 'echo {} && cat {}' 2>/dev/null"
  ).toString();
}

function isStable() {
  try {
    const health = stability.healthCheck();
    return health?.status === "ok";
  } catch {
    return false;
  }
}

function canPush() {
  return Date.now() - lastPush > COOLDOWN;
}

function push() {
  execSync("git add .");
  execSync(`git commit -m "auto-sync ${Date.now()}"`);
  execSync("git push");
  lastPush = Date.now();
}

function loop() {
  try {
    const snap = getSnapshot();

    if (snap !== lastSnapshot) {
      const diffSize = Math.abs(snap.length - lastSnapshot.length);

      lastSnapshot = snap;

      const mem = load();
      addMemory(mem, "diff detected: " + diffSize, "sync_event");
      save(mem);

      if (isStable() && canPush() && diffSize > 20) {
        push();
        console.log("🚀 PUSH (diff-based)");
      } else {
        console.log("⏳ SKIP (unstable or small diff)");
      }
    }
  } catch (e) {
    console.log("⚠️ ERROR:", e.message);
  }
}

setInterval(loop, 8000);

console.log("🧠 IMA SYNC V4 DIFF ACTIVE");
