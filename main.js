import * as THREE from 'https://cdnjs.cloudflare.com/ajax/libs/three.js/r150/three.module.min.js';
import { GLTFLoader } from 'https://cdn.jsdelivr.net/npm/three@0.150/examples/jsm/loaders/GLTFLoader.js';

let scene, camera, renderer, mom, clock;

export function init3D() {
    clock = new THREE.Clock();
    scene = new THREE.Scene();
    scene.background = new THREE.Color(0xf4f5f7);

    camera = new THREE.PerspectiveCamera(55, window.innerWidth/window.innerHeight, 0.1, 100);
    camera.position.set(0,1.6,3);

    renderer = new THREE.WebGLRenderer({ antialias:true });
    renderer.setSize(window.innerWidth, window.innerHeight);
    document.getElementById('viewer').appendChild(renderer.domElement);

    scene.add(new THREE.HemisphereLight(0xffffff,0xdddccc,1.2));
    const d = new THREE.DirectionalLight(0xffffff,0.8);
    d.position.set(2,4,2);
    scene.add(d);

    // Fallback Sphere
    mom = new THREE.Mesh(
        new THREE.SphereGeometry(0.6,64,64),
        new THREE.MeshStandardMaterial({ color:0xffc1b6, roughness:0.4 })
    );
    mom.position.y = 1.4;
    scene.add(mom);

    // Load GLB
    const loader = new GLTFLoader();
    loader.load('server/public/ima (2).glb', g => {
        scene.remove(mom);
        mom = g.scene;
        mom.position.set(0,-1.2,0);
        scene.add(mom);
    });

    animate();
}

function animate() {
    requestAnimationFrame(animate);
    const t = clock.getElapsedTime();
    if(mom){
        const s = 1 + Math.sin(t*2)*0.02;
        mom.scale.set(s,s,s);
        mom.position.y = 1.4 + Math.sin(t*1.5)*0.05;
    }
    renderer.render(scene,camera);
}

window.addEventListener('resize', ()=>{
    if(!camera) return;
    camera.aspect = window.innerWidth/window.innerHeight;
    camera.updateProjectionMatrix();
    renderer.setSize(window.innerWidth, window.innerHeight);
});
