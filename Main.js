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

function createPlaceholderAvatar() {
  // יצירת דמות בסיסית במקום GLB
  const geometry = new THREE.BoxGeometry(0.5, 1.6, 0.3);
  const material = new THREE.MeshStandardMaterial({ color: 0x8888ff });
  avatarModel = new THREE.Mesh(geometry, material);
  avatarModel.position.set(0, 0.8, 0); // חצי גובה כדי שיהיה על הקרקע
  scene.add(avatarModel);
  showUIButtons();
}

function loadAvatar() {
  loader.load(
    modelURL,
    function (gltf) {
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
      console.warn('Error loading GLB, using placeholder avatar:', error);
      createPlaceholderAvatar();
    }
  );
}

// -------------------- Google Sign-In --------------------
function initGoogleSignIn() {
  if (!window.google || !google.accounts) {
    console.warn('Google Sign-In library not loaded, continuing without sign-in.');
    loadAvatar(); // טען דמות גם אם Google Sign-In לא טעון
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
    const payload = decodeJwtResponse(response.credential);

    const userLang = navigator.language || 'he';
    const momName = userLang.startsWith('he') ? 'אמא' : 'Ima';
    document.getElementById('welcome-text').textContent = `ברוכים הבאים ל-${momName}!`;

    document.getElementById('google-signin').style.display = 'none';

    loadAvatar();
  } catch (err) {
    console.error('Error decoding Google credential:', err);
    loadAvatar();
  }
}

function decodeJwtResponse(token) {
  try {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    return JSON.parse(window.atob(base64));
  } catch (err) {
    console.error('Invalid JWT token', err);
    return {};
  }
}

// -------------------- כפתורי UI --------------------
function showUIButtons() {
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
  try {
    initGoogleSignIn();
  } catch (err) {
    console.error('Error during initialization:', err);
    loadAvatar();
  }
};
