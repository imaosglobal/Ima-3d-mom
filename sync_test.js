const fs = require("fs");
const { execSync } = require("child_process");

console.log("🧪 IMA SYNC TEST (DRY RUN)");

function run(cmd) {
  try {
    const out = execSync(cmd).toString();
    return out;
  } catch (e) {
    return e.message;
  }
}

// בדיקות בסיס
const checks = {
  gitExists: run("git --version").includes("git"),
  repoExists: fs.existsSync(".git"),
  files: fs.readdirSync(".").length
};

console.log("CHECKS:", checks);

// סימולציה של push
if (checks.repoExists) {
  console.log("📡 Simulating push...");
  console.log(run("git status"));
} else {
  console.log("⚠️ No git repo initialized");
}

console.log("✔ TEST COMPLETE");
