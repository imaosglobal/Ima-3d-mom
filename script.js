// מעבר מהמסך פתיחה ל־main page
document.getElementById('google-login').addEventListener('click', () => {
    // כאן אפשר להוסיף אימות Google בעתיד
    document.getElementById('landing-page').classList.add('hidden');
    document.getElementById('main-page').classList.remove('hidden');
    initThreeJSModel();
});

// טעינת מודל אמא עם Three.js
function initThreeJSModel() {
    const container = document.getElementById('model-container');

    const scene = new THREE.Scene();
    const camera = new THREE.PerspectiveCamera(45, container.clientWidth / container.clientHeight, 0.1, 1000);
    camera.position.set(0, 1.5, 3);

    const renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
    renderer.setSize(container.clientWidth, container.clientHeight);
    container.appendChild(renderer.domElement);

    // אור
    const ambientLight = new THREE.AmbientLight(0xffffff, 1);
    scene.add(ambientLight);

    // טעינת מודל
    const loader = new THREE.GLTFLoader();
    loader.load('assets/ima.glb', function(gltf){
        scene.add(gltf.scene);
    }, undefined, function(error){
        console.error(error);
        alert('שגיאה בטעינת המודל של אמא');
    });

    function animate() {
        requestAnimationFrame(animate);
        renderer.render(scene, camera);
    }
    animate();
}

// אירועים לממשק טקסט וקול
document.getElementById('send-text').addEventListener('click', () => {
    const text = document.getElementById('text-input').value;
    if(text) {
        // כאן אפשר לשלוח ל־Ima TTS או chat API
        console.log('User sent:', text);
        document.getElementById('text-input').value = '';
    }
});

document.getElementById('record-voice').addEventListener('click', () => {
    // אפשרות להקליט קול ולשלוח ל־TTS
    console.log('Voice recording started...');
});
