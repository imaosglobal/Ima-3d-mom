import * as THREE from 'https://cdnjs.cloudflare.com/ajax/libs/three.js/r150/three.module.min.js';
import { GLTFLoader } from 'https://cdn.jsdelivr.net/npm/three@0.150/examples/jsm/loaders/GLTFLoader.js';

export const Mom3D = {
    // core
    scene: null,
    camera: null,
    renderer: null,
    clock: null,

    // state
    mom: null,
    emotion: "neutral",
    mode: "guest",         // guest | google
    eyeLevel: 1,           // The Eye progression
    stabilityScore: 0,     // emotional regulation over time

    // lights
    hemiLight: null,
    dirLight: null,
    auraLight: null,

    /* ================= INIT ================= */
    init(config = {}) {
        this.mode = config.mode || "guest";
        this.clock = new THREE.Clock();

        this.scene = new THREE.Scene();
        this.scene.background = new THREE.Color(0xf4f5f7);

        this.camera = new THREE.PerspectiveCamera(
            55,
            window.innerWidth / window.innerHeight,
            0.1,
            100
        );
        this.camera.position.set(0, 1.6, 3);

        this.renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
        this.renderer.setSize(window.innerWidth, window.innerHeight);
        document.getElementById("viewer").appendChild(this.renderer.domElement);

        this.setupLights();
        this.loadMomModel();

        this.animate();
        window.addEventListener("resize", () => this.onResize());
    },

    /* ================= LIGHTS ================= */
    setupLights() {
        this.hemiLight = new THREE.HemisphereLight(0xffffff, 0xdddccc, 1.2);
        this.scene.add(this.hemiLight);

        this.dirLight = new THREE.DirectionalLight(0xffffff, 0.8);
        this.dirLight.position.set(2, 4, 2);
        this.scene.add(this.dirLight);

        this.auraLight = new THREE.PointLight(0xffe0b2, 0.2, 5);
        this.auraLight.position.set(0, 1.4, 0);
        this.scene.add(this.auraLight);
    },

    /* ================= MODEL ================= */
    loadMomModel() {
        // fallback
        this.mom = new THREE.Mesh(
            new THREE.SphereGeometry(0.6, 64, 64),
            new THREE.MeshStandardMaterial({ color: 0xffc1b6, roughness: 0.4 })
        );
        this.mom.position.y = 1.4;
        this.scene.add(this.mom);

        const loader = new GLTFLoader();
        loader.load(
            'server/public/3DModel.glb',
            gltf => {
                this.scene.remove(this.mom);
                this.mom = gltf.scene;
                this.mom.position.set(0, -1.2, 0);
                this.scene.add(this.mom);
            },
            undefined,
            e => console.error("GLB load failed:", e)
        );
    },

    /* ================= EMOTION & INTENT ================= */
    reactToEmotion(text) {
        text = text.toLowerCase();

        let prevEmotion = this.emotion;

        if (text.includes("קשה") || text.includes("עצוב")) {
            this.emotion = "sad";
        } else if (text.includes("טוב") || text.includes("נח") || text.includes("שמחה")) {
            this.emotion = "happy";
        } else if (text.includes("כועס") || text.includes("לחוץ")) {
            this.emotion = "angry";
        } else {
            this.emotion = "neutral";
        }

        // stability logic (this is The Eye)
        if (this.emotion === "neutral" || this.emotion === "happy") {
            this.stabilityScore += 1;
        } else {
            this.stabilityScore = Math.max(0, this.stabilityScore - 1);
        }

        this.evaluateEyeProgress(prevEmotion);
    },

    /* ================= THE EYE LOGIC ================= */
    evaluateEyeProgress(prevEmotion) {
        if (this.mode === "guest" && this.eyeLevel >= 1) return;

        // open next eye only with stability, not excitement
        if (this.stabilityScore > 6 && this.eyeLevel === 1) {
            this.eyeLevel = 2;
            window.MomSay?.("העין השנייה נפתחת. לא מיהרת.");
        }

        if (this.mode === "google" && this.stabilityScore > 14 && this.eyeLevel === 2) {
            this.eyeLevel = 3;
            window.MomSay?.("עין שלישית נפתחת. זה כבר משחק משותף.");
        }
    },

    /* ================= ANIMATION ================= */
    animate() {
        requestAnimationFrame(() => this.animate());
        const t = this.clock.getElapsedTime();

        if (this.mom) {
            const floatY = 1.4 + Math.sin(t * 1.5) * 0.05;
            let scale = 1 + Math.sin(t * 2) * 0.02;

            let color = new THREE.Color(0xffc1b6);
            let hemiI = 1.2, dirI = 0.8;
            let auraI = 0.2;

            switch (this.emotion) {
                case "sad":
                    color.set(0xaaaacc);
                    hemiI = 0.8;
                    dirI = 0.5;
                    scale *= 0.98;
                    break;
                case "happy":
                    color.set(0xfff0c2);
                    hemiI = 1.5;
                    dirI = 1.0;
                    scale *= 1.03;
                    break;
                case "angry":
                    color.set(0xff9999);
                    dirI = 1.2;
                    scale *= 1.05;
                    break;
            }

            this.mom.traverse(o => {
                if (o.isMesh && o.material) {
                    o.material.color.lerp(color, 0.05);
                }
            });

            this.mom.scale.set(scale, scale, scale);
            this.mom.position.y = floatY;
            this.mom.rotation.y = Math.sin(t * (0.4 + this.eyeLevel * 0.2)) * 0.05;

            this.hemiLight.intensity += (hemiI - this.hemiLight.intensity) * 0.05;
            this.dirLight.intensity += (dirI - this.dirLight.intensity) * 0.05;
            this.auraLight.intensity += (auraI - this.auraLight.intensity) * 0.05;
            this.auraLight.position.y = floatY;
        }

        this.updateDebug();
        this.renderer.render(this.scene, this.camera);
    },

    /* ================= DEBUG ================= */
    updateDebug() {
        const d = document.getElementById("debug");
        if (!d) return;
        d.textContent =
            `Mode: ${this.mode}\n` +
            `Eye: ${this.eyeLevel}\n` +
            `Emotion: ${this.emotion}\n` +
            `Stability: ${this.stabilityScore}`;
    },

    onResize() {
        this.camera.aspect = window.innerWidth / window.innerHeight;
        this.camera.updateProjectionMatrix();
        this.renderer.setSize(window.innerWidth, window.innerHeight);
    }
};
