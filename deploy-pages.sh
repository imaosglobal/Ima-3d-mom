#!/bin/bash
# ===============================
# Ima-3d-mom GitHub Pages deploy
# ===============================

echo "🔹 Starting deployment to GitHub Pages..."

# Step 1: בדיקה שהקבצים החיוניים קיימים
REQUIRED=("index.html" "Main.js" "style.css" "assets/model.glb")
MISSING=0
for f in "${REQUIRED[@]}"; do
  if [ ! -f "$f" ] && [ ! -d "$f" ]; then
    echo "❌ Missing: $f"
    MISSING=1
  fi
done

if [ $MISSING -eq 1 ]; then
  echo "❌ Some required files are missing. Fix them before deploying."
  exit 1
fi

echo "✅ All required files are present."

# Step 2: Add + commit
git add .
git commit -m "Deploy GitHub Pages update" || echo "No changes to commit."

# Step 3: Push to main branch
git push

echo "🔹 Deployment pushed. Wait 1-2 minutes for GitHub Pages to update."
echo "✅ Done!"
