// server/index.js
import express from "express";
import cors from "cors";
import bodyParser from "body-parser";
import cookieSession from "cookie-session";
import { OAuth2Client } from "google-auth-library";
import path from "path";
import { fileURLToPath } from "url";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const app = express();
const PORT = process.env.PORT || 3000;

const GOOGLE_CLIENT_ID = process.env.GOOGLE_CLIENT_ID;
const client = new OAuth2Client(GOOGLE_CLIENT_ID);

/* ---------- Middleware ---------- */
app.use(cors({
  origin: true,
  credentials: true
}));

app.use(bodyParser.json());

app.use(
  cookieSession({
    name: "ima-session",
    keys: [process.env.SESSION_SECRET],
    maxAge: 24 * 60 * 60 * 1000
  })
);

/* ---------- Static ---------- */
app.use(express.static(path.join(__dirname, "../")));

/* ---------- Auth ---------- */

// התחברות
app.post("/auth/google", async (req, res) => {
  try {
    const { credential } = req.body;

    const ticket = await client.verifyIdToken({
      idToken: credential,
      audience: GOOGLE_CLIENT_ID
    });

    const payload = ticket.getPayload();

    req.session.user = {
      id: payload.sub,
      email: payload.email,
      name: payload.name,
      picture: payload.picture
    };

    res.json({ ok: true, user: req.session.user });
  } catch (err) {
    console.error("Auth error:", err);
    res.status(401).json({ ok: false });
  }
});

// בדיקת התחברות
app.get("/auth/me", (req, res) => {
  if (!req.session.user) {
    return res.status(401).json({ loggedIn: false });
  }
  res.json({ loggedIn: true, user: req.session.user });
});

// התנתקות
app.post("/auth/logout", (req, res) => {
  req.session = null;
  res.json({ ok: true });
});

/* ---------- Fallback ---------- */
app.get("*", (req, res) => {
  res.sendFile(path.join(__dirname, "../index.html"));
});

app.listen(PORT, () =>
  console.log(`Ima server running on port ${PORT}`)
);
