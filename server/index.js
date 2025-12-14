// server/index.js
import express from 'express';
import bodyParser from 'body-parser';
import cors from 'cors';
import fs from 'fs';
import path from 'path';

const app = express();
const PORT = process.env.PORT || 3000;
const JOURNAL_FILE = process.env.JOURNAL_FILE || path.join(__dirname, 'journal.json');

app.use(cors());
app.use(bodyParser.json());

// קבלת כל הרשומות
app.get('/log', (req, res) => {
  fs.readFile(JOURNAL_FILE, 'utf8', (err, data) => {
    if(err) return res.status(500).json({error: 'Failed to read journal'});
    res.json(JSON.parse(data || '[]'));
  });
});

// הוספת רשומה חדשה
app.post('/log', (req, res) => {
  fs.readFile(JOURNAL_FILE, 'utf8', (err, data) => {
    const logs = data ? JSON.parse(data) : [];
    logs.push({ ...req.body, timestamp: new Date().toISOString() });
    fs.writeFile(JOURNAL_FILE, JSON.stringify(logs, null, 2), err => {
      if(err) return res.status(500).json({error:'Failed to write journal'});
      res.json({status:'ok'});
    });
  });
});

// יצירת הקובץ אם לא קיים
if(!fs.existsSync(JOURNAL_FILE)) fs.writeFileSync(JOURNAL_FILE, '[]');

app.listen(PORT, () => console.log(`Ima Journal server running on port ${PORT}`));
