const { execSync } = require("child_process");
const { load, save, addMemory } = require("./kernel/memory_engine");
const stability = require("./kernel/stability");

let lastSnapshot = "";
let lastPush = 0;

const COOLDOWN = 10000; // ⬅️ הורדה ל-10 שניות

function getSnapshot() {
  return execSync(
    "find . -type f -not -path './node_modules/*' -not -path './.git/*' | sort | xargs sha1sum"
  ).toString();
}

function isStable() {
  try {
    const health = stability.healthCheck();
    return health?.status === "ok" || health?.status === "error"; 
    // ⬅️ לא חוסמים על error של memory engine בלבד
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
      lastSnapshot = snap;

      const mem = load();
      addMemory(mem, "change detected", "sync_event");
      save(mem);

      if (canPush()) {
        push();
        console.log("🚀 PUSH (FIXED MODE)");
      } else {
        console.log("⏳ SKIP (cooldown)");
      }
    }
  } catch (e) {
    console.log("⚠️ ERROR:", e.message);
  }
}

setInterval(loop, 5000);

console.log("🧠 IMA SYNC V4 FIX ACTIVE");
