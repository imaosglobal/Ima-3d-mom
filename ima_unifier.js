const fs = require("fs");
const { execSync } = require("child_process");

console.log("🧠 IMA UNIFIER START");

// 1. הגדרת מקורות
const repos = [
  "IMA",
  "HERCULES",
  "BASE44"
];

// 2. תוצאות בדיקה
let report = {
  cloned: [],
  failed: [],
  summary: {}
};

// 3. קלון (כבר קיים מקומית / או בעתיד GitHub)
repos.forEach(r => {
  try {
    console.log("📦 processing:", r);

    // כאן אפשר להחליף ל-git clone אמיתי
    const path = "./" + r;

    if (!fs.existsSync(path)) {
      throw new Error("missing repo folder: " + r);
    }

    const files = fs.readdirSync(path);

    report.cloned.push({
      repo: r,
      files: files.length
    });

  } catch (e) {
    report.failed.push({
      repo: r,
      error: e.message
    });
  }
});

// 4. סיכום
report.summary = {
  total: repos.length,
  ok: report.cloned.length,
  failed: report.failed.length
};

// 5. שמירה
fs.writeFileSync("./unify_report.json", JSON.stringify(report, null, 2));

console.log("✔ UNIFY DONE");
console.log(report.summary);
