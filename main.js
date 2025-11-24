// main.js — Three.js minimal GLTF with Draco and basic error handling
(() => {
  const canvas = document.getElementById('c');
  const renderer = new THREE.WebGLRenderer({ canvas, antialias: true });
  renderer.setPixelRatio(window.devicePixelRatio ? Math.min(window.devicePixelRatio, 2) : 1);
  renderer.setSize(window.innerWidth, window.innerHeight);

  const scene = new THREE.Scene();
  scene.background = new THREE.Color(0x0f0f12);

  const camera = new THREE.PerspectiveCamera(50, window.innerWidth / window.innerHeight, 0.1, 1000);
  camera.position.set(0, 1.6, 3);

  const hemi = new THREE.HemisphereLight(0xffffff, 0x444444, 1.0);
  hemi.position.set(0, 1, 0);
  scene.add(hemi);
  const dir = new THREE.DirectionalLight(0xffffff, 0.8);
  dir.position.set(5,10,7.5);
  scene.add(dir);

  const controls = new THREE.OrbitControls(camera, renderer.domElement);
  controls.target.set(0,1.2,0);
  controls.update();

  // Loaders
  const dracoLoader = new THREE.DRACOLoader();
  // אם תרצה לשים נתיב ל־draco decoder מקומי - הכנס אותו כאן
  // dracoLoader.setDecoderPath('path/to/draco/');

  const loader = new THREE.GLTFLoader();
  loader.setDRACOLoader(dracoLoader);

  const overlayLoader = document.getElementById('loader');

  function onProgress(xhr){
    if (xhr.lengthComputable) {
      const pct = (xhr.loaded / xhr.total * 100).toFixed(0);
      overlayLoader.textContent = 'טוען מודל... ' + pct + '%';
    }
  }

  function onError(err){
    console.error('GLTF load error', err);
    overlayLoader.textContent = 'שגיאה בטעינת המודל. בדוק assets/model.glb';
  }

  // נסה לטעון את המודל מהassets
  const MODEL_PATH = 'assets/model.glb';

  loader.load(MODEL_PATH, gltf => {
    overlayLoader.style.display = 'none';
    const root = gltf.scene || gltf.scenes[0];
    root.traverse(node => { if (node.isMesh) { node.castShadow = true; node.receiveShadow = true; } });
    scene.add(root);
    // scale/position if needed
    root.position.set(0,0,0);
    // אם רוצים להתאים גודל אוטומטית
    const box = new THREE.Box3().setFromObject(root);
    const size = box.getSize(new THREE.Vector3()).length();
    const center = box.getCenter(new THREE.Vector3());
    root.position.x += (root.position.x - center.x);
    root.position.y += (root.position.y - center.y);

  }, onProgress, onError);

  // Resize
  window.addEventListener('resize', () => {
    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();
    renderer.setSize(window.innerWidth, window.innerHeight);
  });

  // basic RAF loop
  let running = true;
  document.addEventListener('visibilitychange', () => { running = document.visibilityState === 'visible'; });

  function animate(){
    if (running) requestAnimationFrame(animate);
    renderer.render(scene, camera);
  }
  animate();
})();
