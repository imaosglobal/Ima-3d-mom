import * as THREE from 'https://cdn.jsdelivr.net/npm/three@0.157.0/build/three.module.js';
import { GLTFLoader } from 'https://cdn.jsdelivr.net/npm/three@0.157.0/examples/jsm/loaders/GLTFLoader.js';

// ---------------- סצנה, מצלמה ורנדרר ----------------
const scene = new THREE.Scene();
scene.background = new THREE.Color(0xf7f7f7);

const camera = new THREE.PerspectiveCamera(45, window.innerWidth / window.innerHeight, 0.1, 1000);
camera.position.set(0, 1.6, 3);

const renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

// ---------------- אור ----------------
const hemiLight = new THREE.HemisphereLight(0xffffff, 0x444444, 1);
hemiLight.position.set(0, 20, 0);
scene.add(hemiLight);

const dirLight = new THREE.DirectionalLight(0xffffff, 0.8);
dirLight.position.set(-3, 10, -10);
scene.add(dirLight);

// ---------------- טעינת מודל ----------------
const loader = new GLTFLoader();
const modelURL = 'https://imaosglobal.github.io/Ima-3d-mom/assets/model.glb';
let avatarModel = null;
const animateMixers = [];

function loadAvatar() {
  loader.load(
    modelURL,
    gltf => {
      avatarModel = gltf.scene;
      avatarModel.position.set(0, 0, 0);
      avatarModel.scale.set(1.2, 1.2, 1.2);
      avatarModel.rotation.y = Math.PI;
      scene.add(avatarModel);

      if (gltf.animations && gltf.animations.length > 0) {
        const mixer = new THREE.AnimationMixer(avatarModel);
        gltf.animations.forEach(clip => mixer.clipAction(clip).play());
        animateMixers.push(mixer);
      }

      showUIButtons();
    },
    undefined,
    err => {
      console.warn('מודל לא נטען, מציג UI בלבד:', err);
      showUIButtons();
    }
  );
}

// ---------------- Google Sign-In ----------------
function initGoogleSignIn() {
  if (!window.google || !google.accounts) {
    console.error('Google API לא נטען');
    document.getElementById('welcome-text').textContent = 'ברוכים הבאים!';
    showUIButtons();
    return;
  }
  google.accounts.id.initialize({
    client_id: '1093623573018-6s23clvor9u80r08135aelfatuib3a55.apps.googleusercontent.com',
    callback: handleCredentialResponse,
  });
  google.accounts.id.renderButton(
    document.getElementById('google-signin'),
    { theme: 'outline', size: 'large', width: 250 }
  );
}

function handleCredentialResponse(response) {
  try {
    const payload = decodeJwt(response.credential);
    const userLang = navigator.language || 'he';
    const momName = userLang.startsWith('he') ? 'אמא' : 'Mom';
    document.getElementById('welcome-text').textContent = `ברוכים הבאים ל-${momName}!`;
    window.userProfile = payload;
    document.getElementById('google-signin').style.display = 'none';
    loadAvatar();
  } catch (e) {
    console.error('שגיאה בעיבוד Google Credential:', e);
    document.getElementById('welcome-text').textContent = 'ברוכים הבאים!';
    showUIButtons();
  }
}

function decodeJwt(token) {
  try {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    return JSON.parse(window.atob(base64));
  } catch {
    return {};
  }
}

// ---------------- UI Buttons ----------------
function showUIButtons() {
  if (document.querySelector('.button-container')) return; // למנוע כפילות

  const btnContainer = document.createElement('div');
  btnContainer.className = 'button-container';
  document.body.appendChild(btnContainer);

  const sendBtn = document.createElement('button');
  sendBtn.textContent = 'שלח הודעה';
  sendBtn.onclick = () => alert('כאן תשלח את ההודעה שלך לאמא');
  btnContainer.appendChild(sendBtn);

  const recordBtn = document.createElement('button');
  recordBtn.textContent = 'הקלט קול';
  recordBtn.onclick = () => alert('כאן תתחיל הקלטת קול');
  btnContainer.appendChild(recordBtn);
}

// ---------------- אנימציה ורינדור ----------------
const clock = new THREE.Clock();
let rotateAngle = 0;

function animate() {
  requestAnimationFrame(animate);
  const delta = clock.getDelta();
  animateMixers.forEach(m => m.update(delta));

  if (avatarModel) {
    rotateAngle += 0.002;
    avatarModel.rotation.y = Math.PI + Math.sin(rotateAngle) * 0.05;
    avatarModel.position.y = 0.02 * Math.sin(rotateAngle * 2);
  }

  renderer.render(scene, camera);
}
animate();

// ---------------- רספונסיב ----------------
window.addEventListener('resize', () => {
  camera.aspect = window.innerWidth / window.innerHeight;
  camera.updateProjectionMatrix();
  renderer.setSize(window.innerWidth, window.innerHeight);
});

// ---------------- אתחול ----------------
window.addEventListener('load', initGoogleSignIn);
