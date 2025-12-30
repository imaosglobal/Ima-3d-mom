import * as THREE from 'https://cdnjs.cloudflare.com/ajax/libs/three.js/r150/three.module.min.js';

export const Mom3D = {

  scene:null, camera:null, renderer:null, clock:null,

  // מצב
  mode:"eye",                     // eye | ima
  profile:{ age:10, progress:0, regulation:0 },
  eyeLevel:1,

  // ישויות
  eyeSpace:null,
  ima:null,

  init(){
    this.clock=new THREE.Clock();
    this.scene=new THREE.Scene();

    this.camera=new THREE.PerspectiveCamera(60,innerWidth/innerHeight,0.1,100);
    this.camera.position.set(0,1.6,3);

    this.renderer=new THREE.WebGLRenderer({antialias:true});
    this.renderer.setSize(innerWidth,innerHeight);
    viewer.appendChild(this.renderer.domElement);

    this.buildEyeSpace();
    this.buildIma();

    window.addEventListener("resize",()=>this.resize());
    this.animate();
  },

  /* ===== מרחב המשחק עצמו ===== */
  buildEyeSpace(){
    this.eyeSpace=new THREE.Group();
    this.scene.add(this.eyeSpace);

    const geo=new THREE.SphereGeometry(3,64,64);
    const mat=new THREE.MeshStandardMaterial({
      color:0x224466, wireframe:true, transparent:true, opacity:0.3
    });
    this.eye=new THREE.Mesh(geo,mat);
    this.eyeSpace.add(this.eye);

    const l=new THREE.HemisphereLight(0xffffff,0x222222,1.2);
    this.scene.add(l);
  },

  /* ===== אמא ===== */
  buildIma(){
    const g=new THREE.SphereGeometry(0.5,64,64);
    const m=new THREE.MeshStandardMaterial({color:0xffc9b8});
    this.ima=new THREE.Mesh(g,m);
    this.ima.visible=false;
    this.scene.add(this.ima);
  },

  /* ===== קלט משתמש ===== */
  handleUserInput(text){
    if(text.length>4) this.profile.regulation++;
    if(this.profile.regulation>5) this.eyeLevel++;

    this.updateSpaceByProfile();
  },

  /* ===== התאמת המרחב לאדם ===== */
  updateSpaceByProfile(){
    this.eye.material.opacity = Math.min(0.6,0.2+this.profile.regulation*0.05);
    this.eye.scale.setScalar(1+this.eyeLevel*0.1);
  },

  /* ===== מעבר Eye <-> Ima ===== */
  switchMode(m){
    this.mode=m;
    if(m==="ima"){
      this.ima.visible=true;
    } else {
      this.ima.visible=false;
    }
  },

  animate(){
    requestAnimationFrame(()=>this.animate());
    const t=this.clock.getElapsedTime();

    this.eye.rotation.y+=0.001;
    this.ima.rotation.y+=0.002;

    if(this.mode==="ima"){
      this.camera.position.z += (1.6 - this.camera.position.z)*0.05;
      this.ima.position.y=1.4+Math.sin(t)*0.05;
    } else {
      this.camera.position.z += (3 - this.camera.position.z)*0.05;
    }

    debug.textContent=
      `Mode: ${this.mode}
EyeLevel: ${this.eyeLevel}
Regulation: ${this.profile.regulation}`;

    this.renderer.render(this.scene,this.camera);
  },

  resize(){
    this.camera.aspect=innerWidth/innerHeight;
    this.camera.updateProjectionMatrix();
    this.renderer.setSize(innerWidth,innerHeight);
  }

};

Mom3D.init();
