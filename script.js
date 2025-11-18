document.getElementById('google-login').addEventListener('click', () => {
  document.getElementById('landing-page').classList.add('hidden');
  document.getElementById('main-page').classList.remove('hidden');
  initThreeJSModel();
});

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

// טקסט והקלטת קול נשארים כפי שהוגדרו
