const fs = require("fs");

let content = fs.readFileSync("server.js", "utf-8");

if (!content.includes("req._startTime")) {
  content = content.replace(
    "app.use(",
    `app.use((req,res,next)=>{
  req._startTime = Date.now();
  res.on("finish", ()=>{
    const duration = Date.now() - req._startTime;
    console.log("⏱", req.method, req.url, duration+"ms");
  });
  next();
});
app.use(`
  );

  fs.writeFileSync("server.js", content);
  console.log("📊 METRICS ADDED");
} else {
  console.log("⏳ METRICS ALREADY EXISTS");
}
