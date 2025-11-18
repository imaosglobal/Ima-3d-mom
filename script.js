let scene, camera, renderer, model, mixer;
const container = document.getElementById("container");
const loader = document.getElementById("loader");
const errorBox = document.getElementById("error");

// --- יצירת סצנה ---
scene = new THREE.Scene();
scene.background = new THREE.Color(0xf0f0f0);

camera = new THREE.PerspectiveCamera(55, window.innerWidth / window.innerHeight, 0.1, 1000);
camera.position.set(0, 1.6, 3);

renderer = new THREE.WebGLRenderer({ antialias: true });
renderer.setSize(window.innerWidth, window.innerHeight * 0.7);
container.appendChild(renderer.domElement);

// תאורה
const light = new THREE.DirectionalLight(0xffffff, 1);
light.position.set(1, 2, 3);
scene.add(light);

// --- טעינת מודל ---
const gltfLoader = new THREE.GLTFLoader();

function loadModel(path) {
    loader.style.display = "block";
    errorBox.style.display = "none";

    gltfLoader.load(
        path,
        function (gltf) {
            if (model) scene.remove(model);

            model = gltf.scene;
            model.position.set(0, -1.2, 0);
            scene.add(model);

            mixer = new THREE.AnimationMixer(model);

            if (gltf.animations.length > 0) {
                mixer.clipAction(gltf.animations[0]).play(); // אנימציה בסיסית
            }

            loader.style.display = "none";
        },
        undefined,
        function (error) {
            errorBox.style.display = "block";
            errorBox.innerText = "שגיאה בטעינת המודל";
            console.error(error);
        }
    );
}

// טעינת ברירת מחדל
loadModel("models/mom_default.glb");

// --- שינוי דמות ---
document.getElementById("modelSelect").addEventListener("change", (e) => {
    loadModel(e.target.value);
});

// --- שינוי צבע שיער (מניח שיש אובייקט בשם Hair) ---
document.getElementById("hairColor").addEventListener("input", (e) => {
    if (!model) return;

    model.traverse(obj => {
        if (obj.name.toLowerCase().includes("hair")) {
            obj.material.color = new THREE.Color(e.target.value);
        }
    });
});

// --- צחוק ---
document.getElementById("laughBtn").addEventListener("click", () => {
    if (!mixer) return;

    const laugh = THREE.AnimationClip.findByName(mixer._root.animations, "Laugh");

    if (laugh) mixer.clipAction(laugh).play();
});

// --- רינדור ---
function animate() {
    requestAnimationFrame(animate);
    if (mixer) mixer.update(0.01);
    renderer.render(scene, camera);
}

animate();
