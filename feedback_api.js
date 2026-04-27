const express = require("express");
const fs = require("fs");

const app = express();
app.use(express.json());

app.post("/ima/feedback", (req, res) => {
  const feedback = req.body;

  const log = {
    ...feedback,
    time: Date.now()
  };

  let existing = [];

  if (fs.existsSync("feedback_log.json")) {
    existing = JSON.parse(fs.readFileSync("feedback_log.json"));
  }

  existing.push(log);

  fs.writeFileSync("feedback_log.json", JSON.stringify(existing, null, 2));

  res.json({ status: "received" });
});

app.get("/ima/feedback/summary", (req, res) => {
  const data = fs.existsSync("feedback_log.json")
    ? JSON.parse(fs.readFileSync("feedback_log.json"))
    : [];

  const avgRating =
    data.reduce((a, b) => a + (b.rating || 0), 0) / (data.length || 1);

  res.json({
    count: data.length,
    avgRating
  });
});

app.listen(5000, () => {
  console.log("📡 FEEDBACK ENGINE RUNNING ON 5000");
});
