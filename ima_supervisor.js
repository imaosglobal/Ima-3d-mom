const { spawn } = require("child_process");

const services = [
  { name: "world_api", file: "world_api.js", port: 4000 },
  { name: "feedback_api", file: "feedback_api.js", port: 5000 }
];

let processes = {};

function startService(service) {
  console.log(`🚀 STARTING ${service.name}`);

  const p = spawn("node", [service.file], {
    stdio: "inherit"
  });

  p.on("exit", (code) => {
    console.log(`⚠️ ${service.name} exited (${code}). restarting...`);
    setTimeout(() => startService(service), 2000);
  });

  processes[service.name] = p;
}

function startAll() {
  console.log("🧠 IMA SUPERVISOR STARTING ALL SYSTEMS");

  services.forEach(startService);

  console.log("✅ ALL SERVICES LAUNCHED");
}

function healthCheck() {
  console.log("🩺 HEALTH CHECK ACTIVE");

  setInterval(() => {
    Object.keys(processes).forEach(name => {
      const p = processes[name];
      if (!p || p.killed) {
        console.log(`🔁 RESTARTING ${name}`);
        const service = services.find(s => s.name === name);
        startService(service);
      }
    });
  }, 5000);
}

startAll();
healthCheck();
