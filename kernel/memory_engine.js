const fs = require("fs");

const FILE = "./memory.json";

function loadMemory() {
  try {
    if (!fs.existsSync(FILE)) {
      const init = {
        profile: {
          name: "אורי",
          mood: "calm",
          personality: "neutral"
        },
        memory: []
      };
      fs.writeFileSync(FILE, JSON.stringify(init, null, 2));
      return init;
    }

    return JSON.parse(fs.readFileSync(FILE, "utf8"));
  } catch (e) {
    return { profile: {}, memory: [] };
  }
}

function saveMemory(mem) {
  fs.writeFileSync(FILE, JSON.stringify(mem, null, 2));
}

function addMemory(mem, value, type = "general") {
  if (!mem.memory) mem.memory = [];

  mem.memory.push({
    value,
    type,
    time: Date.now()
  });

  if (mem.memory.length > 50) mem.memory.shift();

  return mem;
}

module.exports = {
  loadMemory,
  saveMemory,
  addMemory
};
