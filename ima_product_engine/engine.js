const fs = require("fs");

console.log("🚀 IMA PRODUCT ENGINE START");

// קלט מהבריין
const brain = JSON.parse(fs.readFileSync("./ima_brain_v2/brain.json"));

let product = {
  core: [],
  plugins: [],
  status: "building",
  issues: []
};

// 1. בחירת ליבה יציבה
brain.stable_modules.forEach(m => {
  product.core.push({
    name: m,
    type: "core"
  });
});

// 2. מודולים לא יציבים הופכים לפלאגינים בלבד
brain.unstable_modules.forEach(m => {
  product.plugins.push({
    name: m,
    type: "plugin",
    disabled: true
  });
});

// 3. המלצות הופכות לבעיות
product.issues = brain.recommendations;

// 4. בדיקת תקינות בסיסית
function validate(p) {
  if (p.core.length === 0) {
    return { ok: false, reason: "No stable core modules" };
  }

  return { ok: true };
}

const check = validate(product);

// 5. תוצאה
product.status = check.ok ? "READY" : "BROKEN";

fs.writeFileSync(
  "./ima_product_engine/product.json",
  JSON.stringify(product, null, 2)
);

console.log("✔ PRODUCT BUILT");
console.log("STATUS:", product.status);
