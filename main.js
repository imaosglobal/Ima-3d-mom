import * as THREE from 'https://cdnjs.cloudflare.com/ajax/libs/three.js/r150/three.module.min.js';
import { GLTFLoader } from 'https://cdn.jsdelivr.net/npm/three@0.150/examples/jsm/loaders/GLTFLoader.js';

export const Mom3D = {
    scene: null,
    camera: null,
    renderer: null,
    mom: null,
    clock: null,
    emotion: "neutral",
    hemiLight: null,
    dirLight: null,
    auraLight: null,

    init() {
        this.clock = new THREE.Clock();
        this.scene = new THREE.Scene();
        this.scene.background = new THREE.Color(0xf4f5f7);

        this.camera = new THREE.PerspectiveCamera(55, window.innerWidth/window.innerHeight, 0.1, 100);
        this.camera.position.set(0,1.6,3);

        this.renderer = new THREE.WebGLRenderer({ antialias:true, alpha:true });
        this.renderer.setSize(window.innerWidth,window.innerHeight);
        document.getElementById("viewer").appendChild(this.renderer.domElement);

        // Lights
        this.hemiLight = new THREE.HemisphereLight(0xffffff,0xdddccc,1.2);
        this.scene.add(this.hemiLight);
        this.dirLight = new THREE.DirectionalLight(0xffffff,0.8);
        this.dirLight.position.set(2,4,2);
        this.scene.add(this.dirLight);

        // Soft Aura Light
        this.auraLight = new THREE.PointLight(0xffe0b2, 0.2, 5);
        this.auraLight.position.set(0,1.4,0);
        this.scene.add(this.auraLight);

        // Fallback Sphere
        this.mom = new THREE.Mesh(
            new THREE.SphereGeometry(0.6,64,64),
            new THREE.MeshStandardMaterial({color:0xffc1b6,roughness:0.4})
        );
        this.mom.position.y = 1.4;
        this.scene.add(this.mom);

        // Load GLB model
        const loader = new GLTFLoader();
        loader.load(
            'server/public/3DModel.glb',
            g => {
                this.scene.remove(this.mom);
                this.mom = g.scene;
                this.mom.position.set(0,-1.2,0);
                this.scene.add(this.mom);
            },
            undefined,
            e => console.error("Failed to load 3DModel.glb:", e)
        );

        this.animate();
        window.addEventListener("resize",()=>this.onResize());
    },

    reactToEmotion(text){
        text = text.toLowerCase();
        if(text.includes("קשה") || text.includes("עצוב")){
            this.emotion = "sad";
        } else if(text.includes("טוב") || text.includes("נח")){
            this.emotion = "happy";
        } else if(text.includes("כועס") || text.includes("מוטרדת")){
            this.emotion = "angry";
        } else {
            this.emotion = "neutral";
        }
    },

    animate() {
        requestAnimationFrame(()=>this.animate());
        const t = this.clock.getElapsedTime();

        if(this.mom){
            const baseScale = 1;
            const floatY = 1.4 + Math.sin(t*1.5)*0.05;
            let scaleFactor = baseScale + Math.sin(t*2)*0.02;

            let color = new THREE.Color(0xffc1b6);
            let hemiIntensity = 1.2;
            let dirIntensity = 0.8;
            let auraColor = new THREE.Color(0xffe0b2);
            let auraIntensity = 0.2;

            switch(this.emotion){
                case "sad":
                    scaleFactor *= 0.98;
                    this.mom.rotation.y = Math.sin(t*0.5)*0.05;
                    color.set(0xaaaacc);
                    hemiIntensity = 0.8;
                    dirIntensity = 0.5;
                    auraColor.set(0xaaaaff);
                    auraIntensity = 0.15;
                    break;
                case "happy":
                    scaleFactor *= 1.02;
                    this.mom.rotation.y = Math.sin(t*1)*0.08;
                    color.set(0xffe0b2);
                    hemiIntensity = 1.5;
                    dirIntensity = 1.0;
                    auraColor.set(0xfff0c2);
                    auraIntensity = 0.25;
                    break;
                case "angry":
                    scaleFactor *= 1.05;
                    this.mom.rotation.y = Math.sin(t*3)*0.15;
                    color.set(0xff9999);
                    hemiIntensity = 1.0;
                    dirIntensity = 1.2;
                    auraColor.set(0xffc0b0);
                    auraIntensity = 0.2;
                    break;
                default:
                    this.mom.rotation.y = Math.sin(t*0.5)*0.03;
                    color.set(0xffc1b6);
                    hemiIntensity = 1.2;
                    dirIntensity = 0.8;
                    auraColor.set(0xffe0b2);
                    auraIntensity = 0.2;
                    break;
            }

            if(this.mom.material) this.mom.material.color.lerp(color,0.1);
            this.mom.traverse(c => { if(c.isMesh && c.material) c.material.color.lerp(color,0.05); });

            this.mom.scale.set(scaleFactor, scaleFactor, scaleFactor);
            this.mom.position.y = floatY;

            this.hemiLight.intensity += (hemiIntensity - this.hemiLight.intensity)*0.05;
            this.dirLight.intensity += (dirIntensity - this.dirLight.intensity)*0.05;

            this.auraLight.color.lerp(auraColor, 0.05);
            this.auraLight.intensity += (auraIntensity + Math.sin(t*2)*0.02 - this.auraLight.intensity)*0.05;
            this.auraLight.position.y = this.mom.position.y;
        }

        // Update debug overlay
        const debugEl = document.getElementById("debug");
        if(debugEl){
            debugEl.textContent = 
                `User: ${window.currentUser ? window.currentUser.name : "לא מחובר"}\n` +
                `Model: ${this.mom ? "Loaded" : "Fallback"}\nEmotion: ${this.emotion}`;
        }

        this.renderer.render(this.scene,this.camera);
    },

    onResize() {
        if(!this.camera) return;
        this.camera.aspect = window.innerWidth/window.innerHeight;
        this.camera.updateProjectionMatrix();
        this.renderer.setSize(window.innerWidth,window.innerHeight);
    }
};
