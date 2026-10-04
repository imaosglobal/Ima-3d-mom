const express = require("express");
const app = require("./world_api");

const PORT = process.env.PORT || 4000;

app.listen(PORT, () => {
  console.log("🌍 IMA LIVE SERVER ON", PORT);
});

module.exports = app;
