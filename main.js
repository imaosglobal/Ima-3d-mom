// main.js
import * as THREE from 'https://cdnjs.cloudflare.com/ajax/libs/three.js/r150/three.module.min.js';
import { ImaManagement } from './imaManagement.js';

export const Mom3D = {

  state:{
    mode:"eye",            // eye | ima | transition
    user:{
      age:10,
      progress:0,
      regulation:0,
      level:1
    },
    environment:{
      complexity:1,
      openness:0.3
    },
    ima:{
      form:"human",
      visible:false
    },
    transition:{
      active:false,
      t:0                  // 0..1
    }
  },

  scene:null,
  camera:null,
  renderer:null,
  clock:null,

  eyeSpace:null,
  eye:null,
  spiral:null,
  imaGroup:null,
  lights:{},

  init(){
    this.clock=new THREE.Clock();
    this.scene=new THREE.Scene();
    this.scene.background=new THREE.Color(0x0b1020);

    this.camera=new THREE.PerspectiveCamera(60,innerWidth/innerHeight,0.1,100);
    this.camera.position.set(0,1.6,3);

    this.renderer=new THREE.WebGLRenderer({antialias:true,alpha:true});
    this.renderer.setPixelRatio(Math.min(devicePixelRatio,2));
    this.renderer.setSize(innerWidth,innerHeight);
    document.getElementById("viewer").appendChild(this.renderer.domElement);

    this.buildLights();
    this.buildEyeWorld();
    this.buildImaEntity();

    window.addEventListener("resize",()=>this.resize());

    ImaManagement.createNewModule("Ima3DModule");

    this.addInteractionListeners();
    this.animate();
  },

  buildLights(){
    this.lights.hemi=new THREE.HemisphereLight(0xffffff,0x222244,1.2);
    this.lights.dir=new THREE.DirectionalLight(0xffffff,0.9);
    this.lights.dir.position.set(3,6,2);
    this.scene.add(this.lights.hemi,this.lights.dir);
  },

  buildEyeWorld(){
    this.eyeSpace=new THREE.Group();
    this.scene.add(this.eyeSpace);

    this.eye=new THREE.Mesh(
      new THREE.SphereGeometry(3,64,64),
      new THREE.MeshStandardMaterial({
        color:0x3355aa,
        wireframe:true,
        transparent:true,
        opacity:0.35
      })
    );

    this.spiral=new THREE.Mesh(
      new THREE.TorusKnotGeometry(1.2,0.08,160,32),
      new THREE.MeshStandardMaterial({
        color:0xffffff,
        transparent:true,
        opacity:0
      })
    );

    this.eyeSpace.add(this.eye,this.spiral);
  },

  buildImaEntity(){
    this.imaGroup=new THREE.Group();

    const core=new THREE.Mesh(
      new THREE.IcosahedronGeometry(0.45,2),
      new THREE.MeshStandardMaterial({color:0xffc9b8,roughness:0.4})
    );

    const aura=new THREE.Mesh(
      new THREE.SphereGeometry(0.7,32,32),
      new THREE.MeshStandardMaterial({
        color:0xfff0c0,
        transparent:true,
        opacity:0.15
      })
    );

    this.imaGroup.add(core,aura);
    this.imaGroup.visible=false;
    this.scene.add(this.imaGroup);
  },

  handleUserInput(text){
    const u=this.state.user;

    if(text.length>4) u.regulation++;
    if(u.regulation>5){
      u.level++;
      this.state.environment.complexity++;
      u.regulation=0;
    }

    if(u.age<=5) this.state.ima.form="human";
    else if(u.level>4) this.state.ima.form="abstract";

    ImaManagement.Ima3DModule.AI.learn({event:"userInput", text:text, level:u.level});

    this.updateWorldFromState();
  },

  updateWorldFromState(){
    const u=this.state.user;
    const env=this.state.environment;

    this.eye.material.opacity=Math.min(0.65,0.25+env.complexity*0.05);
    this.eye.scale.setScalar(1+env.complexity*0.12);

    if(u.age<=5){
      this.eye.material.wireframe=false;
      this.eye.material.color.set(0x66ccff);
    }else{
      this.eye.material.wireframe=true;
      this.eye.material.color.set(0x3355aa);
    }

    if(this.state.ima.form==="abstract"){
      this.imaGroup.scale.setScalar(1.25);
    }else{
      this.imaGroup.scale.setScalar(1);
    }
  },

  switchMode(target){
    if(this.state.transition.active) return;

    this.state.transition.active=true;
    this.state.transition.t=0;
    this.state.mode="transition";
    this.state._targetMode=target;

    // Spiral initial opacity
    this.spiral.material.opacity = (target==="ima") ? 0.4 : 0.4;
  },

  addInteractionListeners(){
    document.getElementById("viewer").addEventListener("click", e=>{
      const mouse=new THREE.Vector2(
        (e.clientX/window.innerWidth)*2-1,
        -(e.clientY/window.innerHeight)*2+1
      );
      const raycaster=new THREE.Raycaster();
      raycaster.setFromCamera(mouse,this.camera);
      const intersects=raycaster.intersectObject(this.eye,true);

      if(intersects.length>0){
        this.switchMode("ima");
        console.log("לחיצה על העין → מעבר ל-Ima");
      }
    });

    document.getElementById("viewer").addEventListener("dblclick", e=>{
      const mouse=new THREE.Vector2(
        (e.clientX/window.innerWidth)*2-1,
        -(e.clientY/window.innerHeight)*2+1
      );
      const raycaster=new THREE.Raycaster();
      raycaster.setFromCamera(mouse,this.camera);
      const intersects=raycaster.intersectObject(this.imaGroup,true);

      if(intersects.length>0){
        this.switchMode("eye");
        console.log("לחיצה כפולה על Ima → חזרה למשחק");
      }
    });
  },

  animate(){
    requestAnimationFrame(()=>this.animate());
    const dt=this.clock.getDelta();
    const t=this.clock.elapsedTime;

    this.eye.rotation.y+=0.001;
    this.spiral.rotation.x+=0.004;
    this.spiral.rotation.y+=0.006;

    if(this.state.transition.active){
      this.state.transition.t+=dt;
      const k=Math.min(this.state.transition.t,1);

      if(this.state._targetMode==="ima"){
        // כניסה ל-Ima
        this.camera.position.z=THREE.MathUtils.lerp(3,1.6,k);
        this.spiral.material.opacity=0.4*(1-k);
      } else {
        // חזרה ל-Eye
        this.camera.position.z=THREE.MathUtils.lerp(1.6,3,k);
        this.spiral.material.opacity=0.4*k;  // Spiral עולה ואז נעלם
      }

      if(k>=1){
        this.state.transition.active=false;
        this.state.mode=this.state._targetMode;
        this.imaGroup.visible=(this.state.mode==="ima");
      }
    }

    if(this.state.mode==="ima"){
      this.imaGroup.rotation.y+=0.003;
      this.imaGroup.position.y=1.4+Math.sin(t*1.2)*0.06;
    }else{
      this.camera.position.z+= (3-this.camera.position.z)*0.05;
    }

    debug.textContent=
`Mode: ${this.state.mode}
User Level: ${this.state.user.level}
Complexity: ${this.state.environment.complexity}
Ima Form: ${this.state.ima.form}
AI Memory: ${ImaManagement.Ima3DModule.AI.learningData.length}`;

    this.renderer.render(this.scene,this.camera);
  },

  resize(){
    this.camera.aspect=innerWidth/innerHeight;
    this.camera.updateProjectionMatrix();
    this.renderer.setSize(innerWidth,innerHeight);
  }

};

Mom3D.init();
