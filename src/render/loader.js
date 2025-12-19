function initScene(container) {
  if (!container) {
    console.error('canvas-container not found');
    return;
  }

  const scene = new THREE.Scene();

  const camera = new THREE.PerspectiveCamera(
    60,
    window.innerWidth / window.innerHeight,
    0.1,
    1000
  );
  camera.position.z = 4;

  const renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
  renderer.setSize(window.innerWidth, window.innerHeight);
  renderer.setPixelRatio(window.devicePixelRatio);
  container.appendChild(renderer.domElement);

  // תאורה רכה
  const light = new THREE.HemisphereLight(0xffffff, 0x444444, 1.2);
  scene.add(light);

  // אובייקט זמני (כדור לבדיקה)
  const geometry = new THREE.SphereGeometry(1, 32, 32);
  const material = new THREE.MeshStandardMaterial({ color: 0xffd1dc });
  const sphere = new THREE.Mesh(geometry, material);
  scene.add(sphere);

  function animate() {
    requestAnimationFrame(animate);
    sphere.rotation.y += 0.002;
    renderer.render(scene, camera);
  }

  animate();

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
