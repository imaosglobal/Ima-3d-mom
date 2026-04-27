const { execSync } = require("child_process");
const fs = require("fs");

function clone(repo) {
  const dir = "external_repos/" + repo.name.replace("/", "_");

  if (fs.existsSync(dir)) return dir;

  execSync(`git clone --depth=1 ${repo.clone_url} ${dir}`);
  return dir;
}

module.exports = { clone };
