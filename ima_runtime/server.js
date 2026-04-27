const express = require("express");
const fs = require("fs");

const app = express();
app.use(express.json());

console.log("🧠 IMA RUNTIME ONLINE");

// memory safe loader
function loadMemory() {
  try {
    return JSON.parse(fs.readFileSync("./memory.json"));
  } catch {
    return { memory: [] };
  }
}

function saveMemory(mem) {
  fs.writeFileSync("./memory.json", JSON.stringify(mem, null, 2));
}

// health
app.get("/health", (req, res) => {
  res.json({
    status: "ok",
    time: Date.now()
  });
});

// main endpoint
app.get("/", (req, res) => {
  const mem = loadMemory();

  mem.memory.push({
    type: "ping",
    time: Date.now()
  });

  saveMemory(mem);

  res.json({
    status: "IMA LIVE",
    memorySize: mem.memory.length
  });
});

// start
app.listen(3000, () => {
  console.log("🚀 IMA RUNNING ON 3000");
});
