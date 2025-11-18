// כניסה עם Google (placeholder)
document.getElementById('google-login').addEventListener('click', () => {
  document.getElementById('landing-page').classList.add('hidden');
  document.getElementById('main-page').classList.remove('hidden');
  initThreeJSModel();
});

// טעינת מודל אמא
function initThreeJSModel() {
  const container = document.getElementById('model-container');
  const scene = new THREE.Scene();
  const camera = new THREE.PerspectiveCamera(45, container.clientWidth / container.clientHeight, 0.1, 1000);
  camera.position.set(0, 1.5, 3);

  const renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
  renderer.setSize(container.clientWidth, container.clientHeight);
  container.appendChild(renderer.domElement);

  const ambientLight = new THREE.AmbientLight(0xffffff, 1);
  scene.add(ambientLight);

  const loader = new THREE.GLTFLoader();
  loader.load('assets/ima.glb',
    gltf => scene.add(gltf.scene),
    undefined,
    error => console.error('Error loading model:', error)
  );

  function animate() {
    requestAnimationFrame(animate);
    renderer.render(scene, camera);
  }
  animate();
}

// כפתור שלח
document.getElementById('send-text').addEventListener('click', () => {
  const text = document.getElementById('text-input').value;
  if(text.trim() !== "") {
    console.log("User wrote:", text);
    document.getElementById('text-input').value = "";
    alert("הטקסט נשלח לאמא (placeholder)");
  }
});

// הקלטת קול
document.getElementById('record-voice').addEventListener('click', async () => {
  try {
    const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
    const mediaRecorder = new MediaRecorder(stream);
    let chunks = [];
    mediaRecorder.ondataavailable = e => chunks.push(e.data);
    mediaRecorder.onstop = () => {
      const blob = new Blob(chunks, { type: 'audio/webm' });
      console.log("Voice recorded:", blob);
      alert("הקלטה הושלמה (placeholder)");
    };
    mediaRecorder.start();
    setTimeout(() => mediaRecorder.stop(), 5000);
    alert("הקלטה התחילה - תסתיים אוטומטית לאחר 5 שניות");
  } catch (err) {
    console.error("Mic error:", err);
    alert("לא ניתן לגשת למיקרופון");
  }
});
