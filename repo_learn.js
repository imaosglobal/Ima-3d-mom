const fs = require("fs");
const path = require("path");

function scan(dir) {
  let insights = [];

  function walk(p) {
    const files = fs.readdirSync(p);

    for (const f of files) {
      const full = path.join(p, f);

      if (fs.statSync(full).isDirectory()) {
        if (f === "node_modules" || f === ".git") continue;
        walk(full);
      } else {
        if (f.endsWith(".js")) {
          const content = fs.readFileSync(full, "utf-8");

          if (content.includes("function") || content.includes("=>")) {
            insights.push({
              file: full,
              size: content.length
            });
          }
        }
      }
    }
  }

  walk(dir);
  return insights.slice(0, 10);
}

module.exports = { scan };
