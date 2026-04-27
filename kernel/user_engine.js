const fs = require("fs");

const FILE = "./users.json";

function loadUsers() {
  if (!fs.existsSync(FILE)) {
    fs.writeFileSync(FILE, JSON.stringify({ users: [] }, null, 2));
  }
  return JSON.parse(fs.readFileSync(FILE));
}

function getUser(users, id) {
  return users.users.find(u => u.id === id);
}

function createUser(users, id) {
  const user = {
    id,
    memory: [],
    created: Date.now()
  };

  users.users.push(user);
  return user;
}

function saveUsers(users) {
  fs.writeFileSync(FILE, JSON.stringify(users, null, 2));
}

module.exports = {
  loadUsers,
  getUser,
  createUser,
  saveUsers
};
