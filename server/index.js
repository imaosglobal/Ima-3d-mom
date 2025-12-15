// server/index.js
import express from 'express';
import cookieSession from 'cookie-session';
import cors from 'cors';
import { OAuth2Client } from 'google-auth-library';
import fs from 'fs';
import path from 'path';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const app = express();
const PORT = process.env.PORT || 3000;
const CLIENT_ID = process.env.GOOGLE_CLIENT_ID;
const CLIENT_SECRET = process.env.GOOGLE_CLIENT_SECRET;
const BASE_URL = process.env.BASE_URL; // לדוגמה: https://ima-journal.onrender.com
const JOURNAL_FILE = path.join(__dirname, 'journal.json');

const oauth = new OAuth2Client(
  CLIENT_ID,
  CLIENT_SECRET,
  `${BASE_URL}/auth/callback`
);

/* ===== Middlewares ===== */
app.use(cors({ credentials: true, origin: BASE_URL }));
app.use(express.json());
app.use(
  cookieSession({
    name: 'ima-session',
    keys: ['ima-secret-key'],
    maxAge: 24 * 60 * 60 * 1000,
  })
);
app.use(express.static(path.join(__dirname, '../')));

/* ===== Auth Routes ===== */
// התחלת התחברות
app.get('/auth/google', (req, res) => {
  const url = oauth.generateAuthUrl({
    access_type: 'offline',
    prompt: 'consent select_account',
    scope: ['profile', 'email'],
  });
  res.redirect(url);
});

// חזרה מגוגל
app.get('/auth/callback', async (req, res) => {
  try {
    const { tokens } = await oauth.getToken(req.query.code);
    oauth.setCredentials(tokens);

    const ticket = await oauth.verifyIdToken({
      idToken: tokens.id_token,
      audience: CLIENT_ID,
    });

    const payload = ticket.getPayload();
    req.session.user = {
      id: payload.sub,
      name: payload.name,
      email: payload.email,
      picture: payload.picture,
    };

    res.redirect('/');
  } catch (err) {
    console.error(err);
    res.redirect('/?error=auth');
  }
});

// מי מחובר
app.get('/me', (req, res) => {
  if (req.session.user) {
    res.json({ loggedIn: true, user: req.session.user });
  } else {
    res.json({ loggedIn: false });
  }
});

// התנתקות
app.post('/logout', (req, res) => {
  req.session = null;
  res.json({ ok: true });
});

/* ===== Journal API ===== */
// יצירת קובץ אם לא קיים
if (!fs.existsSync(JOURNAL_FILE)) fs.writeFileSync(JOURNAL_FILE, '{"entries":[],"lastUpdated":null}');

// קריאה ליומן
app.get('/log', (req, res) => {
  fs.readFile(JOURNAL_FILE, 'utf8', (err, data) => {
    if (err) return res.status(500).json({ error: 'Failed to read journal' });
    res.json(JSON.parse(data || '{"entries":[],"lastUpdated":null}'));
  });
});

// הוספת רשומה
app.post('/log', (req, res) => {
  fs.readFile(JOURNAL_FILE, 'utf8', (err, data) => {
    const logs = data ? JSON.parse(data) : { entries: [], lastUpdated: null };
    logs.entries.push({ ...req.body, timestamp: new Date().toISOString() });
    logs.lastUpdated = new Date().toISOString();
    fs.writeFile(JOURNAL_FILE, JSON.stringify(logs, null, 2), (err) => {
      if (err) return res.status(500).json({ error: 'Failed to write journal' });
      res.json({ status: 'ok' });
    });
  });
});

/* ===== Serve index.html for SPA ===== */
app.get('*', (req, res) => {
  res.sendFile(path.join(__dirname, '../index.html'));
});

/* ===== Start Server ===== */
app.listen(PORT, () => {
  console.log('Ima server running on port', PORT);
});
