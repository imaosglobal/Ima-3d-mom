const express = require("express");
const { loadMemory, saveMemory, addMemory } = require("./kernel/memory_engine");
const { decide } = require("./kernel/brain_core");
const { dispatch } = require("./kernel/action_dispatcher");

const app = express();
app.use(express.json());

app.post("/ima/run", async (req, res) => {
  const msg = req.body.message;

  const mem = loadMemory();
  const decision = decide(msg, mem);

  addMemory(mem, msg, "input");

  let actionResult = null;

  if (decision.actions.length > 0) {
    actionResult = await dispatch(decision.actions[0], { message: msg });
  }

  saveMemory(mem);

  res.json({
    result: actionResult?.result || "IMA processed message",
    debug: decision,
    action: actionResult
  });
});

app.listen(4000, () => {
  console.log("🌍 IMA CORE RUNNING ON 4000");
});
