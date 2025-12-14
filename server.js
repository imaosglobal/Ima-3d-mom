// server.js - Node.js + Express
import express from 'express';
import path from 'path';

const app = express();
const PORT = process.env.PORT || 3000;

// הגדרת תיקיית public כסטטית
app.use(express.static(path.join(__dirname, 'public')));

// טעינת דף הבית
app.get('/', (req, res) => {
  res.sendFile(path.join(__dirname, 'index.html'));
});

// התחלת השרת
app.listen(PORT, () => {
  console.log(`Server running on port ${PORT}`);
});
