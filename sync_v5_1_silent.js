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

    // רק אם יש שינוי מצב
    if (state !== lastStatus) {
      lastStatus = state;

      if (state === "DIRTY") {
        if (canPush()) {
          push();
          console.log("🚀 PUSH (STATE CHANGE)");
        } else {
          console.log("⏳ DIRTY (cooldown)");
        }
      } else {
        // לא מדפיס כלום או שקט מוחלט
      }
    }
  } catch (e) {
    console.log("⚠️ ERROR:", e.message);
  }
}

setInterval(loop, 5000);

console.log("🧠 IMA SYNC V5.1 SILENT ACTIVE");
