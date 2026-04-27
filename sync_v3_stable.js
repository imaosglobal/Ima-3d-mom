const { execSync } = require("child_process");
const { load, save, addMemory } = require("./kernel/memory_engine");
const stability = require("./kernel/stability");

let lastHash = "";
let lastPushTime = 0;

const COOLDOWN = 30000; // 30 שניות בין pushים

function hashProject() {
  // ⚠️ חשוב: רק קבצים אמיתיים, בלי noise
  return execSync(
    "find . -type f -not -path './node_modules/*' -not -path './.git/*' | sort | xargs sha1sum"
  ).toString();
}

function validate() {
  try {
    execSync("node -c server.js");
    const health = stability.healthCheck();
    return health?.status === "ok";
  } catch {
    return false;
  }
}

function canPush() {
  const now = Date.now();
  return now - lastPushTime > COOLDOWN;
}

function commitAndPush() {
  execSync("git add .");
  execSync(`git commit -m "auto-sync ${Date.now()}"`);
  execSync("git push");
  lastPushTime = Date.now();
}

function loop() {
  try {
    const currentHash = hashProject();

    if (currentHash !== lastHash) {
      lastHash = currentHash;

      const mem = load();
      addMemory(mem, "change detected", "sync_event");
      save(mem);

      const ok = validate();

      if (ok && canPush()) {
        commitAndPush();
        console.log("🚀 PUSH OK (stable mode)");
      } else {
        console.log("⏳ SKIP (cooldown or unstable)");
      }
    }
  } catch (e) {
    console.log("⚠️ ERROR:", e.message);
  }
}

setInterval(loop, 7000);

console.log("🧠 IMA SYNC V3 STABLE ACTIVE");
