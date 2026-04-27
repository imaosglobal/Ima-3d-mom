const { search } = require("./repo_discovery");
const { clone } = require("./repo_clone");
const { scan } = require("./repo_learn");

async function run() {
  console.log("🧠 IMA LEARNING START");

  const repos = await search();

  for (const repo of repos) {
    console.log("🔎", repo.name);

    const dir = clone(repo);
    const insights = scan(dir);

    console.log("📚 learned:", insights.length, "patterns");
  }

  console.log("✅ LEARNING DONE");
}

run();
