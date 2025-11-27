(() => {
// Container
const container = document.getElementById('scene-container');
const overlayLoader = document.getElementById('loader');

// Renderer
const renderer = new THREE.WebGLRenderer({ antialias: true });
renderer.setPixelRatio(Math.min(window.devicePixelRatio, 2));
renderer.setSize(container.clientWidth, container.clientHeight);
renderer.shadowMap.enabled = true;
container.appendChild(renderer.domElement);

// Scene & Camera
const scene = new THREE.Scene();
scene.background = new THREE.Color(0x0f0f12);

const camera = new THREE.PerspectiveCamera(50, container.clientWidth / container.clientHeight, 0.1, 1000);
camera.position.set(0, 1.6, 3);

// Lights
const hemi = new THREE.HemisphereLight(0xffffff, 0x444444, 1.0);
hemi.position.set(0, 1, 0);
scene.add(hemi);

const dir = new THREE.DirectionalLight(0xffffff, 0.8);
dir.position.set(5, 10, 7.5);
dir.castShadow = true;
scene.add(dir);

// Controls
const controls = new THREE.OrbitControls(camera, renderer.domElement);
controls.target.set(0, 1.2, 0);
controls.update();

// Loaders
const dracoLoader = new THREE.DRACOLoader();
const loader = new THREE.GLTFLoader();
loader.setDRACOLoader(dracoLoader);

function onProgress(xhr){
if(xhr.lengthComputable){
overlayLoader.textContent = 'טוען מודל... ' + Math.floor(xhr.loaded / xhr.total * 100) + '%';
}
}

function onError(err){
console.error('GLTF load error', err);
overlayLoader.textContent = 'שגיאה בטעינת המודל. בדוק assets/3DModel.glb';
}

// Load 3D Model
const MODEL_PATH = 'assets/3DModel.glb';
loader.load(
MODEL_PATH,
gltf => {
overlayLoader.style.display = 'none';
const root = gltf.scene || gltf.scenes[0];
root.traverse(node => {
if(node.isMesh){
node.castShadow = true;
node.receiveShadow = true;
}
});
scene.add(root);

  // Center & scale model
  const box = new THREE.Box3().setFromObject(root);
  const center = box.getCenter(new THREE.Vector3());
  root.position.x -= center.x;
  root.position.y -= center.y;
  root.position.z -= center.z;
},
onProgress,
onError

);

// Handle Resize
window.addEventListener('resize', () => {
camera.aspect = container.clientWidth / container.clientHeight;
camera.updateProjectionMatrix();
renderer.setSize(container.clientWidth, container.clientHeight);
});

// Render Loop
let running = true;
document.addEventListener('visibilitychange', () => { running = document.visibilityState === 'visible'; });

function animate(){
if(running) requestAnimationFrame(animate);
renderer.render(scene, camera);
}
animate();
})();
