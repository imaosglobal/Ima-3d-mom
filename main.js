import * as THREE from 'https://cdnjs.cloudflare.com/ajax/libs/three.js/r150/three.module.min.js';

export const Mom3D = {

  /* ===== מצב אחוד (SOURCE OF TRUTH) ===== */
  state:{
    mode:"eye",   // eye | ima
    user:{
      age:10,
      progress:0,
      regulation:0,
      level:1
    },
    environment:{
      complexity:1,
      mood:"neutral"
    },
    ima:{
      form:"human",   // human | abstract | light
      visible:false
    }
  },

  /* ===== Core ===== */
  scene:null,
  camera:null,
  renderer:null,
  clock:null,

  /* ===== Entities ===== */
  eyeSpace:null,
  eye:null,
  imaGroup:null,

  /* ===== INIT ===== */
  init(){
    this.clock=new THREE.Clock();
    this.scene=new THREE.Scene();
    this.scene.background=new THREE.Color(0xf4f5f7);

    this.camera=new THREE.PerspectiveCamera(60,innerWidth/innerHeight,0.1,100);
    this.camera.position.set(0,1.6,3);

    this.renderer=new THREE.WebGLRenderer({antialias:true,alpha:true});
    this.renderer.setSize(innerWidth,innerHeight);
    viewer.appendChild(this.renderer.domElement);

    this.buildLights();
    this.buildEyeSpace();
    this.buildIma();

    window.addEventListener("resize",()=>this.resize());
    this.animate();
  },

  /* ===== LIGHT ===== */
  buildLights(){
    this.hemi=new THREE.HemisphereLight(0xffffff,0x222233,1.2);
    this.dir=new THREE.DirectionalLight(0xffffff,0.8);
    this.dir.position.set(2,4,2);
    this.scene.add(this.hemi,this.dir);
  },

  /* ===== מרחב העין ===== */
  buildEyeSpace(){
    this.eyeSpace=new THREE.Group();
    this.scene.add(this.eyeSpace);

    const geo=new THREE.SphereGeometry(3,64,64);
    const mat=new THREE.MeshStandardMaterial({
      color:0x335577,
      wireframe:true,
      transparent:true,
      opacity:0.3
    });

    this.eye=new THREE.Mesh(geo,mat);
    this.eyeSpace.add(this.eye);
  },

  /* ===== אמא מודולרית ===== */
  buildIma(){
    this.imaGroup=new THREE.Group();

    const core=new THREE.Mesh(
      new THREE.SphereGeometry(0.45,64,64),
      new THREE.MeshStandardMaterial({color:0xffc9b8,roughness:0.4})
    );

    const halo=new THREE.Mesh(
      new THREE.TorusGeometry(0.6,0.05,16,64),
      new THREE.MeshStandardMaterial({color:0xffffcc})
    );
    halo.rotation.x=Math.PI/2;

    this.imaGroup.add(core,halo);
    this.imaGroup.visible=false;
    this.scene.add(this.imaGroup);
  },

  /* ===== קלט משתמש ===== */
  handleUserInput(text){
    const u=this.state.user;

    if(text.length>4) u.regulation++;
    if(u.regulation>5){
      u.level++;
      this.state.environment.complexity++;
    }

    if(u.age<6){
      this.state.ima.form="human";
    } else if(u.level>5){
      this.state.ima.form="abstract";
    }

    this.updateSpaceByState();
  },

  /* ===== התאמת המרחב ===== */
  updateSpaceByState(){
    const u=this.state.user;
    const env=this.state.environment;

    this.eye.material.opacity=Math.min(0.7,0.2+u.level*0.05);
    this.eye.scale.setScalar(1+env.complexity*0.15);

    if(u.age<6){
      this.eye.material.wireframe=false;
      this.eye.material.color.set(0x66ccff);
    } else {
      this.eye.material.wireframe=true;
      this.eye.material.color.set(0x335577);
    }

    if(this.state.ima.form==="abstract"){
      this.imaGroup.scale.setScalar(1.2);
    } else {
      this.imaGroup.scale.setScalar(1);
    }
  },

  /* ===== מעבר Eye ⇄ Ima ===== */
  switchMode(m){
    this.state.mode=m;
    this.state.ima.visible=(m==="ima");
    this.imaGroup.visible=this.state.ima.visible;
  },

  /* ===== LOOP ===== */
  animate(){
    requestAnimationFrame(()=>this.animate());
    const t=this.clock.getElapsedTime();

    /* eye */
    this.eye.rotation.y+=0.001;

    /* ima */
    this.imaGroup.rotation.y+=0.002;
    this.imaGroup.position.y=1.4+Math.sin(t)*0.05;

    /* camera */
    this.camera.position.z +=
      ((this.state.mode==="ima"?1.6:3)-this.camera.position.z)*0.05;

    /* debug */
    debug.textContent=
`Mode: ${this.state.mode}
Age: ${this.state.user.age}
Level: ${this.state.user.level}
Complexity: ${this.state.environment.complexity}
ImaForm: ${this.state.ima.form}`;

    this.renderer.render(this.scene,this.camera);
  },

  resize(){
    this.camera.aspect=innerWidth/innerHeight;
    this.camera.updateProjectionMatrix();
    this.renderer.setSize(innerWidth,innerHeight);
  }
};

Mom3D.init();
