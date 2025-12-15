import * as THREE from 'https://cdnjs.cloudflare.com/ajax/libs/three.js/r150/three.module.min.js';
import { GLTFLoader } from 'https://cdn.jsdelivr.net/npm/three@0.150/examples/jsm/loaders/GLTFLoader.js';

export const Mom3D = {
  scene: null,
  camera: null,
  renderer: null,
  mom: null,
  clock: null,
  init() {
    this.clock = new THREE.Clock();
    this.scene = new THREE.Scene();
    this.scene.background = new THREE.Color(0xf4f5f7);

    this.camera = new THREE.PerspectiveCamera(55, window.innerWidth/window.innerHeight, 0.1, 100);
    this.camera.position.set(0,1.6,3);

    this.renderer = new THREE.WebGLRenderer({ antialias:true });
    this.renderer.setSize(window.innerWidth, window.innerHeight);
    document.getElementById("viewer").appendChild(this.renderer.domElement);

    const hemi = new THREE.HemisphereLight(0xffffff,0xdddccc,1.2);
    this.scene.add(hemi);
    const dir = new THREE.DirectionalLight(0xffffff,0.8);
    dir.position.set(2,4,2);
    this.scene.add(dir);

    // Fallback Sphere
    this.mom = new THREE.Mesh(
      new THREE.SphereGeometry(0.6,64,64),
      new THREE.MeshStandardMaterial({color:0xffc1b6,roughness:0.4})
    );
    this.mom.position.y = 1.4;
    this.scene.add(this.mom);

    // Load GLB model
    const loader = new GLTFLoader();
    loader.load('server/public/ima (2).glb', g => {
      this.scene.remove(this.mom);
      this.mom = g.scene;
      this.mom.position.set(0,-1.2,0);
      this.scene.add(this.mom);
    });

    this.animate();
    window.addEventListener("resize",()=>this.onResize());
  },
  animate() {
    requestAnimationFrame(()=>this.animate());
    const t = this.clock.getElapsedTime();
    if(this.mom){
      const s = 1 + Math.sin(t*2)*0.02;
      this.mom.scale.set(s,s,s);
      this.mom.position.y = 1.4 + Math.sin(t*1.5)*0.05;
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
