let scene, camera, renderer, loader, model;

const container = document.getElementById('container');
const loaderDiv = document.getElementById('loader');
const errorDiv = document.getElementById('error');

init();
animate();

function init() {
    scene = new THREE.Scene();
    scene.background = new THREE.Color(0xf0f4ff);

    camera = new THREE.PerspectiveCamera(75, container.clientWidth/container.clientHeight, 0.1, 1000);
    camera.position.z = 3.6;
    camera.position.y = 0.2;

    renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
    renderer.setSize(container.clientWidth, container.clientHeight, false);
    renderer.setPixelRatio(window.devicePixelRatio);
    container.appendChild(renderer.domElement);

    // LIGHTS
    const hemiLight = new THREE.HemisphereLight(0xffffee, 0x304070, 1.6);
    scene.add(hemiLight);

    const dirLight = new THREE.DirectionalLight(0xffffff, 1.06);
    dirLight.position.set(2, 7, 1.5);
    dirLight.castShadow = true;
    scene.add(dirLight);

    // FLOOR
    const groundGeo = new THREE.CircleGeometry(4.8, 40);
    const groundMat = new THREE.MeshPhongMaterial({ color: 0xf0eed8, opacity: 0.84, transparent: true });
    const ground = new THREE.Mesh(groundGeo, groundMat);
    ground.position.y = -1.18; ground.rotation.x = -Math.PI / 2;
    scene.add(ground);

    loader = new THREE.GLTFLoader();
    loader.load(
      'mom.glb',
      function(gltf) {
          model = gltf.scene;
          model.traverse(function(node) {
              if (node.isMesh) {
                  node.castShadow = true;
                  node.receiveShadow = true;
                  node.material.side = THREE.DoubleSide;
              }
          });
          model.scale.set(1.5, 1.5, 1.5);
          model.position.y = -0.55;
          scene.add(model);
          loaderDiv.style.display = 'none';
          errorDiv.style.display = 'none';
      },
      function(xhr) {
          loaderDiv.textContent = "טוען את המודל... (" + Math.round(xhr.loaded/xhr.total*100) + "%)";
      },
      function(error) {
          errorDiv.textContent = "אירעה שגיאה בטעינת mom.glb. ודאו שהקובץ בתיקייה!";
          loaderDiv.style.display = 'none';
          errorDiv.style.display = 'block';
      }
    );
    window.addEventListener('resize', onWindowResize, false);
}
function onWindowResize() {
    const w = container.clientWidth;
    const h = container.clientHeight;
    camera.aspect = w / h;
    camera.updateProjectionMatrix();
    renderer.setSize(w, h, false);
}
function animate() {
    requestAnimationFrame(animate);
    if (model) model.rotation.y += 0.008;
    renderer.render(scene, camera);
}
