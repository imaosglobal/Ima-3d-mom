import * as THREE from 'three';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js';

// -------------------- סצנה, מצלמה ורנדרר --------------------
const scene = new THREE.Scene();
scene.background = new THREE.Color(0xf7f7f7);

const camera = new THREE.PerspectiveCamera(
  45,
  window.innerWidth / window.innerHeight,
  0.1,
  1000
);
camera.position.set(0, 1.6, 3);

const renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

// -------------------- אור --------------------
const hemiLight = new THREE.HemisphereLight(0xffffff, 0x444444, 1);
hemiLight.position.set(0, 20, 0);
scene.add(hemiLight);

const dirLight = new THREE.DirectionalLight(0xffffff, 0.8);
dirLight.position.set(-3, 10, -10);
scene.add(dirLight);

// -------------------- טעינת מודל GLB --------------------
const loader = new GLTFLoader();
const modelURL = 'https://imaosglobal.github.io/Ima-3d-mom/assets/model.glb';
let animateMixers = [];
let avatarModel;
let placeholderMesh;

// פונקציה ליצירת placeholder בסיסי
function createPlaceholder() {
  const geometry = new THREE.BoxGeometry(0.5, 1, 0.3);
  const material = new THREE.MeshStandardMaterial({ color: 0x888888 });
  placeholderMesh = new THREE.Mesh(geometry, material);
  placeholderMesh.position.set(0, 0.5, 0);
  scene.add(placeholderMesh);
}

// טוען מודל עם timeout fallback
function loadAvatar() {
  let loaded = false;

  loader.load(
    modelURL,
    function (gltf) {
      loaded = true;
      if (placeholderMesh) scene.remove(placeholderMesh);

      avatarModel = gltf.scene;
      avatarModel.position.set(0, 0, 0);
      avatarModel.scale.set(1.2, 1.2, 1.2);
      avatarModel.rotation.y = Math.PI;
      scene.add(avatarModel);

      if (gltf.animations && gltf.animations.length > 0) {
        const mixer = new THREE.AnimationMixer(avatarModel);
        gltf.animations.forEach((clip) => mixer.clipAction(clip).play());
        animateMixers.push(mixer);
      }

      showUIButtons();
    },
    undefined,
    function (error) {
      console.error('Error loading GLB:', error);
      if (!loaded && !placeholderMesh) createPlaceholder();
    }
  );

  // Timeout fallback אחרי 5 שניות
  setTimeout(() => {
    if (!loaded && !placeholderMesh) {
      console.warn("GLB didn't load in time, showing placeholder");
      createPlaceholder();
      showUIButtons();
    }
  }, 5000);
}

// -------------------- Google Sign-In --------------------
function initGoogleSignIn() {
  try {
    google.accounts.id.initialize({
      client_id: '1093623573018-6s23clvor9u80r08135aelfatuib3a55.apps.googleusercontent.com',
      callback: handleCredentialResponse,
    });
    google.accounts.id.renderButton(
      document.getElementById('google-signin'),
      { theme: 'outline', size: 'large', width: '250' }
    );
  } catch (e) {
    console.warn("Google Sign-In not available:", e);
    loadAvatar(); // נטען גם בלי Google
  }
}

function handleCredentialResponse(response) {
  try {
    const payload = decodeJwtResponse(response.credential);
    const userLang = navigator.language || 'he';
    const momName = userLang.startsWith('he') ? 'אמא' : 'Ima';
    document.getElementById('welcome-text').textContent = `ברוכים הבאים ל-${momName}!`;
    document.getElementById('google-signin').style.display = 'none';
  } catch (e) {
    console.warn("JWT decode failed:", e);
  } finally {
    loadAvatar(); // נטען תמיד
  }
}

function decodeJwtResponse(token) {
  const base64Url = token.split('.')[1];
  const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
  return JSON.parse(window.atob(base64));
}

// -------------------- כפתורי UI --------------------
function showUIButtons() {
  if (document.querySelector('.button-container')) return;

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

// -------------------- רינדור ואנימציה --------------------
const clock = new THREE.Clock();
let rotateAngle = 0;

function animate() {
  requestAnimationFrame(animate);
  const delta = clock.getDelta();
  animateMixers.forEach((mixer) => mixer.update(delta));

  if (avatarModel) {
    rotateAngle += 0.002;
    avatarModel.rotation.y = Math.PI + Math.sin(rotateAngle) * 0.05;
    avatarModel.position.y = 0.02 * Math.sin(rotateAngle * 2);
  } else if (placeholderMesh) {
    rotateAngle += 0.002;
    placeholderMesh.rotation.y = Math.sin(rotateAngle) * 0.1;
  }

  renderer.render(scene, camera);
}
animate();

// -------------------- התאמת חלון --------------------
window.addEventListener('resize', () => {
  camera.aspect = window.innerWidth / window.innerHeight;
  camera.updateProjectionMatrix();
  renderer.setSize(window.innerWidth, window.innerHeight);
});

// -------------------- אתחול --------------------
window.onload = () => {
  initGoogleSignIn();
};
