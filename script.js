// --- מעבר למסך הראשי עם Google OAuth ---
document.getElementById('google-login').addEventListener('click', async () => {
    // שימוש ב-Google Identity Services:
    // https://developers.google.com/identity/gsi/web/guides/overview
    // ניתן להוסיף כאן אימות אמיתי עם Google
    alert("כניסה עם Google זמינה בקרוב - כרגע עובר לדף הראשי");
    document.getElementById('landing-page').classList.add('hidden');
    document.getElementById('main-page').classList.remove('hidden');
    initThreeJSModel();
});

// --- טעינת דמות אמא ---
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

    // טעינת מודל אמא
    const loader = new THREE.GLTFLoader();
    loader.load('assets/ima.glb',
        (gltf) => scene.add(gltf.scene),
        undefined,
        (error) => console.error('Error loading model:', error)
    );

    function animate() {
        requestAnimationFrame(animate);
        renderer.render(scene, camera);
    }
    animate();
}

// --- טקסט ---
document.getElementById('send-text').addEventListener('click', () => {
    const text = document.getElementById('text-input').value;
    if (text) {
        console.log("User wrote:", text);
        document.getElementById('text-input').value = '';
        // כאן אפשר לשלב TTS או חיבור ל־Ima
    }
});

// --- הקלטת קול ---
document.getElementById('record-voice').addEventListener('click', async () => {
    try {
        const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
        const mediaRecorder = new MediaRecorder(stream);
        let chunks = [];
        mediaRecorder.ondataavailable = e => chunks.push(e.data);
        mediaRecorder.onstop = e => {
            const blob = new Blob(chunks, { 'type' : 'audio/ogg; codecs=opus' });
            console.log("Voice recorded:", blob);
            // כאן ניתן לשלוח ל־Ima או להפעיל TTS
        };
        mediaRecorder.start();
        alert("הקלטה החלה! לחץ שוב לעצירה.");
        setTimeout(() => mediaRecorder.stop(), 5000); // הקלטה 5 שניות לדוגמה
    } catch (err) {
        console.error("Microphone error:", err);
    }
});
