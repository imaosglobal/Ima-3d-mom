#!/bin/bash

REPO_NAME="ima-unified-system"
BRANCH="main"

echo "📡 Syncing IMA system to GitHub..."

git init
git add .
git commit -m "IMA Unified System Sync"

# אתה חייב להגדיר remote פעם אחת
# git remote add origin https://github.com/imaosglobal/$REPO_NAME.git

git push -u origin $BRANCH

echo "✔ Sync complete"
