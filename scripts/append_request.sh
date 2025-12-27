#!/usr/bin/env bash
# Append a new request to REQUESTS_LOG.md and commit locally
set -e
if [ -z "$1" ]; then
  echo "Usage: $0 'Request text'"
  exit 1
fi
TEXT="$1"
TS=$(date -u +"%Y-%m-%d %H:%M:%SZ")
cat >> REQUESTS_LOG.md <<EOF

---

## $TS — Manual Append
- Request:
  "$TEXT"

EOF

git add REQUESTS_LOG.md
git commit -m "chore: append request to REQUESTS_LOG.md"

echo "Appended and committed request. Review and push as needed."
