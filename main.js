import * as THREE from 'three';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js';

// ====== פונקציית קונסול ויזואלי ======
function log(msg) {
  console.log(msg); // עדיין לשמור בקונסול של הדפדפן
  const consoleDiv = document.getElementById('console');
  if (consoleDiv) {
    consoleDiv.innerHTML += msg + '<br>';
    consoleDiv.scrollTop = consoleDiv.scrollHeight;
  }
}

// ====== סצנה, מצלמה ורנדרר ======
const scene = new THREE.Scene();
scene.background = new THREE.Color(0xf0f8ff);

const camera = new THREE.PerspectiveCamera(
  75,
  window.innerWidth / window.innerHeight,
  0.1,
  1000
);
camera.position.z = 3;

const renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

// ====== תאורה ======
const ambientLight = new THREE.AmbientLight(0xffffff, 0.6);
scene.add(ambientLight);

const directionalLight = new THREE.DirectionalLight(0xffffff, 1);
directionalLight.position.set(0, 5, 5);
scene.add(directionalLight);

// ====== טעינת מודל ======
const loader = new GLTFLoader();
loader.load(
  'assets/691ca3ed48062250a474725a.glb',
  function (gltf) {
    scene.add(gltf.scene);
    log('✅ מודל נטען בהצלחה!');

    // הסתרת הודעות טעינה
    const welcome = document.getElementById('welcome-text');
    const placeholder = document.getElementById('placeholder');
    if (welcome) welcome.style.display = 'none';
    if (placeholder) placeholder.style.display = 'none';
  },
  function (xhr) {
    const percent = (xhr.loaded / xhr.total * 100).toFixed(2);
    log(`⏳ טעינה: ${percent}%`);
    const placeholder = document.getElementById('placeholder');
    if (placeholder) placeholder.innerText = `טוען: ${percent}%`;
  },
  function (error) {
    log('❌ שגיאה בטעינת המודל');
    console.error(error);
    const placeholder = document.getElementById('placeholder');
    if (placeholder) placeholder.innerText = 'שגיאה בטעינת המודל';
  }
);

// ====== רינדור והאנימציה ======
function animate() {
  requestAnimationFrame(animate);

  // סיבוב איטי למודל (אופציונלי)
  if (scene.children.length > 0) {
    const model = scene.children.find(c => c.type === 'Group');
    if (model) model.rotation.y += 0.005;
  }

  renderer.render(scene, camera);
}
animate();

// ====== התאמת המסך לשינוי גודל ======
window.addEventListener('resize', () => {
  camera.aspect = window.innerWidth / window.innerHeight;
  camera.updateProjectionMatrix();
  renderer.setSize(window.innerWidth, window.innerHeight);
});
