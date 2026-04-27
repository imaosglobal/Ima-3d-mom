const { execSync } = require("child_process");

let lastPush = 0;
const COOLDOWN = 15000;

function hasRealChanges() {
  const status = execSync("git status --porcelain").toString().trim();
  return status.length > 0;
}

function canPush() {
  return Date.now() - lastPush > COOLDOWN;
}

function push() {
  execSync("git add -A");
  execSync(`git commit -m "auto-sync ${Date.now()}"`);
  execSync("git push");
  lastPush = Date.now();
}

function loop() {
  try {
    if (hasRealChanges() && canPush()) {
      push();
      console.log("🚀 PUSH (GIT DIFF MODE)");
    } else {
      console.log("⏳ SKIP (no real changes or cooldown)");
    }
  } catch (e) {
    console.log("⚠️ ERROR:", e.message);
  }
}

setInterval(loop, 4000);

console.log("🧠 IMA SYNC V5 GIT-DIFF ACTIVE");
