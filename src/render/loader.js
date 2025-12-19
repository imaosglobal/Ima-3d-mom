export function initScene(container) {
  const scene = new THREE.Scene();
  const camera = new THREE.PerspectiveCamera(60, container.clientWidth / container.clientHeight, 0.1, 1000);
  const renderer = new THREE.WebGLRenderer({ antialias: true });

  renderer.setSize(container.clientWidth, container.clientHeight);
  container.appendChild(renderer.domElement);

  // אור פשוט
  const light = new THREE.HemisphereLight(0xffffff, 0x444444);
  scene.add(light);

  camera.position.z = 5;

  // אנימציית רינדור בסיסית
  function animate() {
    requestAnimationFrame(animate);
    renderer.render(scene, camera);
  }

  animate();

  // fallback טקסט אם אין מודל
  const text = document.createElement('div');
  text.textContent = "אין מודל לתצוגה — המערכת פועלת!";
  text.style.position = 'absolute';
  text.style.color = '#fff';
  text.style.top = '10px';
  container.appendChild(text);
}
