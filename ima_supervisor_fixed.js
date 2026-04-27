const { spawn } = require("child_process");

const services = [
  { name: "world_api", file: "world_api.js", port: 4000 },
  { name: "feedback_api", file: "feedback_api.js", port: 5000 }
];

let processes = {};

function startService(service) {
  if (processes[service.name]) {
    console.log(`⏭ SKIP ${service.name} already managed`);
    return;
  }

  console.log(`🚀 STARTING ${service.name}`);

  const p = spawn("node", [service.file], {
    stdio: "inherit"
  });

  processes[service.name] = p;

  p.on("exit", (code) => {
    console.log(`⚠️ ${service.name} exited (${code})`);
    delete processes[service.name];

    setTimeout(() => {
      startService(service);
    }, 3000);
  });
}

function startAll() {
  console.log("🧠 IMA FIXED SUPERVISOR START");

  services.forEach(startService);
}

startAll();
