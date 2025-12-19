function initScene(container) {
  if (!container) {
    console.error('canvas-container not found');
    return;
  }

  const scene = new THREE.Scene();
  const camera = new THREE.PerspectiveCamera(60, window.innerWidth / window.innerHeight, 0.1, 1000);
  camera.position.z = 3.5;

  const renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
  renderer.setSize(window.innerWidth, window.innerHeight);
  renderer.setPixelRatio(window.devicePixelRatio);
  container.appendChild(renderer.domElement);

  // תאורה רכה
  const hemiLight = new THREE.HemisphereLight(0xffffff, 0x444444, 1.2);
  scene.add(hemiLight);

  const dirLight = new THREE.DirectionalLight(0xffffff, 0.8);
  dirLight.position.set(0, 5, 5);
  scene.add(dirLight);

  // Loader ל-GLB
  const loader = new THREE.GLTFLoader();
  loader.load(
    'src/models/mom.glb', // נתיב לקובץ הדגם של אמא
    function (gltf) {
      const mom = gltf.scene;
      mom.scale.set(1.2, 1.2, 1.2);
      mom.position.set(0, -1, 0);
      scene.add(mom);

      // אנימציה בסיסית של סיבוב עדין
      function animateMom() {
        requestAnimationFrame(animateMom);
        mom.rotation.y += 0.002;
        renderer.render(scene, camera);
      }
      animateMom();
    },
    undefined,
    function (error) {
      console.error('Error loading mom.glb', error);
    }
  );

  // התאמה לחלון
  window.addEventListener('resize', () => {
    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();
    renderer.setSize(window.innerWidth, window.innerHeight);
  });

  // טקסט מצב
  const status = document.createElement('div');
  status.textContent = 'אמא נטענת — מערכת פעילה';
  status.style.position = 'absolute';
  status.style.bottom = '16px';
  status.style.left = '16px';
  status.style.color = '#e5e7eb';
  status.style.opacity = '0.8';
  container.appendChild(status);
}
