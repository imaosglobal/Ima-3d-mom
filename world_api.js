const express = require("express");
const app = express();

let usageLog = [];

app.use(express.json());

// כניסה לעולם
app.post("/ima/run", (req, res) => {
  const input = req.body;

  const start = Date.now();

  // סימולציה של עיבוד אמא
  const output = {
    result: "processed",
    input
  };

  const duration = Date.now() - start;

  usageLog.push({
    input,
    duration,
    time: Date.now()
  });

  res.json(output);
});

// טלמטריה פנימית
app.get("/ima/metrics", (req, res) => {
  const avg = usageLog.reduce((a,b)=>a+b.duration,0) / (usageLog.length || 1);

  res.json({
    requests: usageLog.length,
    avgResponse: avg
  });
});

app.listen(4000, () => {
  console.log("🌍 IMA WORLD API RUNNING ON 4000");
});
