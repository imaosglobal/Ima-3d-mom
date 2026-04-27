const QUERY = "nodejs automation ai";
const MIN_STARS = 50;

async function search() {
  const res = await fetch(
    `https://api.github.com/search/repositories?q=${encodeURIComponent(QUERY)}&sort=stars&order=desc`,
    {
      headers: {
        "User-Agent": "ima-learning-engine"
      }
    }
  );

  const data = await res.json();

  return (data.items || [])
    .filter(r => r.stargazers_count >= MIN_STARS)
    .slice(0, 5)
    .map(r => ({
      name: r.full_name,
      clone_url: r.clone_url,
      stars: r.stargazers_count
    }));
}

module.exports = { search };
