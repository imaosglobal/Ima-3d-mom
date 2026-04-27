const { execSync } = require("child_process");
const { getChanges, classify } = require("./change_intelligence");

let lastPush = 0;
const COOLDOWN = 15000;

function hasChanges() {
  return getChanges().length > 0;
}

function canPush() {
  return Date.now() - lastPush > COOLDOWN;
}

function isHealthy() {
  try {
    const out = execSync("curl -s http://localhost:3000/health").toString();
    return out.includes("ok") || out.includes("alive");
  } catch {
    return false;
  }
}

function isValid() {
  try {
    execSync("node -c server.js");
    return true;
  } catch {
    return false;
  }
}

function push(type) {
  execSync("git add -A");
  execSync(`git commit -m "auto-${type} ${Date.now()}"`);
  execSync("git push");
  lastPush = Date.now();
}

function loop() {
  try {
    if (!hasChanges() || !canPush()) return;

    const changes = getChanges();
    const type = classify(changes);

    // חוקים
    if (type === "docs") {
      console.log("📄 DOC CHANGE → skip push");
      return;
    }

    if (!isValid()) {
      console.log("🛑 BLOCKED: syntax");
      return;
    }

    if (!isHealthy()) {
      console.log("🛑 BLOCKED: health");
      return;
    }

    push(type);
    console.log(`🚀 PUSH (${type})`);

  } catch (e) {
    console.log("⚠️ ERROR:", e.message);
  }
}

setInterval(loop, 5000);

console.log("🧠 IMA SYNC V7 INTELLIGENT ACTIVE");
