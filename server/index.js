import express from 'express';
import cookieSession from 'cookie-session';
import cors from 'cors';
import { OAuth2Client } from 'google-auth-library';
import path from 'path';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const app = express();
const PORT = process.env.PORT || 3000;

/* ===== Google OAuth ===== */
const CLIENT_ID = process.env.GOOGLE_CLIENT_ID;
const CLIENT_SECRET = process.env.GOOGLE_CLIENT_SECRET;
const BASE_URL = process.env.BASE_URL; // למשל: https://ima-journal.onrender.com

const oauth = new OAuth2Client(
  CLIENT_ID,
  CLIENT_SECRET,
  `${BASE_URL}/auth/callback`
);

/* ===== Middlewares ===== */
app.use(cors({ credentials:true, origin: BASE_URL }));
app.use(express.json());

app.use(
  cookieSession({
    name: 'ima-session',
    keys: ['ima-secret-key'],
    maxAge: 24 * 60 * 60 * 1000
  })
);

/* ===== Static ===== */
app.use(express.static(path.join(__dirname, '../')));

/* ===== Auth Routes ===== */

// התחלת התחברות
app.get('/auth/google', (req,res)=>{
  const url = oauth.generateAuthUrl({
    access_type: 'offline',
    prompt: 'consent select_account',
    scope: ['profile','email']
  });
  res.redirect(url);
});

// חזרה מגוגל
app.get('/auth/callback', async (req,res)=>{
  const { tokens } = await oauth.getToken(req.query.code);
  oauth.setCredentials(tokens);

  const ticket = await oauth.verifyIdToken({
    idToken: tokens.id_token,
    audience: CLIENT_ID
  });

  const payload = ticket.getPayload();

  req.session.user = {
    id: payload.sub,
    name: payload.name,
    email: payload.email,
    picture: payload.picture
  };

  res.redirect('/');
});

// מי מחובר
app.get('/me', (req,res)=>{
  if(req.session.user){
    res.json({ loggedIn:true, user:req.session.user });
  } else {
    res.json({ loggedIn:false });
  }
});

// התנתקות
app.post('/logout', (req,res)=>{
  req.session = null;
  res.json({ ok:true });
});

/* ===== Start ===== */
app.listen(PORT, ()=> {
  console.log('Ima server running on port', PORT);
});
