import fs from 'fs';
import path from 'path';

const JOURNAL_FILE = process.env.JOURNAL_FILE || path.join(process.cwd(), 'server/journal.json');

function updateJournal() {
  if(!fs.existsSync(JOURNAL_FILE)) fs.writeFileSync(JOURNAL_FILE, '{"entries":[],"lastUpdated":null}');
  const data = fs.readFileSync(JOURNAL_FILE, 'utf8');
  const journal = data ? JSON.parse(data) : {entries:[], lastUpdated:null};
  journal.lastUpdated = new Date().toISOString();
  fs.writeFileSync(JOURNAL_FILE, JSON.stringify(journal, null, 2));
  console.log('Journal updated at', new Date().toISOString());
}

updateJournal();
