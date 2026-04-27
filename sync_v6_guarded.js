const { execSync } = require("child_process");

let lastStatus = "unknown";
let lastPush = 0;
const COOLDOWN = 15000;

function gitHasChanges() {
  return execSync("git status --porcelain").toString().trim().length > 0;
}

function canPush() {
  return Date.now() - lastPush > COOLDOWN;
}

function isSystemHealthy() {
  try {
    const output = execSync("curl -s http://localhost:3000/health").toString();
    return output.includes('"status":"ok"') || output.includes('"alive"');
  } catch {
    return false;
  }
}

function isCodeValid() {
  try {
    execSync("node -c server.js");
    return true;
  } catch {
    return false;
  }
}

function push() {
  execSync("git add -A");
  execSync(`git commit -m "auto-sync ${Date.now()}"`);
  execSync("git push");
  lastPush = Date.now();
}

function loop() {
  try {
    const hasChanges = gitHasChanges();
    const state = hasChanges ? "DIRTY" : "CLEAN";

    if (state !== lastStatus) {
      lastStatus = state;

      if (state === "DIRTY") {
        if (!canPush()) return;

        if (!isCodeValid()) {
          console.log("🛑 BLOCKED: syntax error");
          return;
        }

        if (!isSystemHealthy()) {
          console.log("🛑 BLOCKED: system not healthy");
          return;
        }

        push();
        console.log("🚀 PUSH (GUARDED)");
      }
    }
  } catch (e) {
    console.log("⚠️ ERROR:", e.message);
  }
}

setInterval(loop, 5000);

console.log("🧠 IMA SYNC V6 GUARDED ACTIVE");
