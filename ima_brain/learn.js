const fs = require("fs");

function analyze(scan) {
  const data = JSON.parse(fs.readFileSync(scan));

  const report = {
    total_sources: data.length,
    issues: [],
    patterns: []
  };

  data.forEach(s => {
    if (!s.files || s.files.length === 0) {
      report.issues.push({
        source: s.name,
        issue: "empty or unscanned repo"
      });
    }
  });

  return report;
}

const scan = "./ima_brain/analysis/scan.json";

const result = analyze(scan);

fs.writeFileSync(
  "./ima_brain/memory/learning.json",
  JSON.stringify(result, null, 2)
);

console.log("Learning complete");
