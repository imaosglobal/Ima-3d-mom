import * as THREE from 'three';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js';

// --- סצנה ---
const scene = new THREE.Scene();
scene.background = new THREE.Color(0xf7f7f7);

const camera = new THREE.PerspectiveCamera(45, window.innerWidth/window.innerHeight, 0.1, 1000);
camera.position.set(0, 1.6, 3);

const renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

// --- תאורה ---
scene.add(new THREE.HemisphereLight(0xffffff, 0x444444, 1));
const dirLight = new THREE.DirectionalLight(0xffffff, 0.8);
dirLight.position.set(-3, 10, -10);
scene.add(dirLight);

// --- טעינת מודל ---
const loader = new GLTFLoader();

// *** כאן הקישור שאתה ביקשת שיטען תמיד ***
const modelURL = "https://models.readyplayer.me/691ca3ed48062250a474725a.glb";

let avatarModel;
const mixers = [];

function loadAvatar() {
  loader.load(
    modelURL,
    gltf => {
      avatarModel = gltf.scene;
      avatarModel.position.set(0, 0, 0);
      avatarModel.scale.set(1.2, 1.2, 1.2);
      avatarModel.rotation.y = Math.PI;

      scene.add(avatarModel);

      const ph = document.getElementById('placeholder');
      if (ph) ph.style.display = 'none';

      if (gltf.animations.length) {
        const mixer = new THREE.AnimationMixer(avatarModel);
        gltf.animations.forEach(clip => mixer.clipAction(clip).play());
        mixers.push(mixer);
      }
    },
    xhr => {
      console.log(`Loading ${ (xhr.loaded / xhr.total * 100).toFixed(0) }%`);
    },
    err => {
      console.error("GLB Load Error:", err);
      const ph = document.getElementById('placeholder');
      if (ph) ph.textContent = 'טעינת דמות נכשלה';
    }
  );
}

// --- Google Sign-In ---
function initGoogleSignIn() {
  if (!window.google) {
    console.warn("Google API לא נטען");
    showUIButtons();
    return;
  }

  google.accounts.id.initialize({
    client_id: '1093623573018-6s23clvor9u80r08135aelfatuib3a55.apps.googleusercontent.com',
    callback: handleCredentialResponse
  });

  google.accounts.id.renderButton(
    document.getElementById('google-signin'),
    { theme: 'outline', size: 'large', width: 250 }
  );
}

function handleCredentialResponse(response) {
  const payload = decodeJwt(response.credential);
  const lang = navigator.language || 'he';
  const momName = lang.startsWith('he') ? 'אמא' : 'Mom';

  document.getElementById('welcome-text').textContent = `ברוכים הבאים ל-${momName}!`;
  document.getElementById('google-signin').style.display = 'none';

  loadAvatar();
  showUIButtons();
}

function decodeJwt(token) {
  try {
    const base64Url = token.split('.')[1];
    return JSON.parse(window.atob(base64Url));
  } catch (e) {
    console.error("JWT decode failed", e);
    return {};
  }
}

// --- UI Buttons ---
function showUIButtons() {
  if (document.querySelector('.button-container')) return;

  const container = document.createElement('div');
  container.className = 'button-container';

  const msgBtn = document.createElement('button');
  msgBtn.textContent = 'שלח הודעה';
  msgBtn.onclick = () => alert('כאן תשלח הודעה לאמא');

  const recBtn = document.createElement('button');
  recBtn.textContent = 'הקלט קול';
  recBtn.onclick = () => alert('כאן מתחיל הקלטת קול');

  container.appendChild(msgBtn);
  container.appendChild(recBtn);
  document.body.appendChild(container);
}

// --- לולאת אנימציה ---
const clock = new THREE.Clock();
let angle = 0;

function animate() {
  requestAnimationFrame(animate);

  const delta = clock.getDelta();
  mixers.forEach(m => m.update(delta));

  if (avatarModel) {
    angle += 0.002;
    avatarModel.rotation.y = Math.PI + Math.sin(angle) * 0.05;
    avatarModel.position.y = Math.sin(angle * 2) * 0.02;
  }

  renderer.render(scene, camera);
}

animate();

// --- התאמה לגודל מסך ---
window.addEventListener('resize', () => {
  camera.aspect = window.innerWidth/window.innerHeight;
  camera.updateProjectionMatrix();
  renderer.setSize(window.innerWidth, window.innerHeight);
});

// --- אתחול ---
window.onload = () => {
  initGoogleSignIn();
  loadAvatar();      // נטען בכל מקרה
  showUIButtons();   // מציג כפתורים בכל מקרה
};
