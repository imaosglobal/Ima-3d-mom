let mixer, clock, modelScene, actions = [], blinkAction;

// פונקציה לקבלת שם אמא לפי שפה
function getImaName() {
  let userLang = navigator.language || "he";
  return userLang.startsWith("he") ? "אמא" : "Ima";
}

// Google Identity Services
function handleCredentialResponse(response) {
  const data = parseJwt(response.credential);

  // הצגת שם אמא לפי שפה
  document.querySelector('.welcome-text').textContent = `ברוכים הבאים ל-${getImaName()}`;

  // הסתרת מסך כניסה
  document.getElementById('landing-page').classList.add('hidden');
  document.getElementById('main-page').classList.remove('hidden');
  document.getElementById('user-greeting').textContent = `שלום, ${data.name || "משתמש"}!`;

  // טעינת מודל 3D
  initThreeJSModel();
}

function parseJwt(token) {
  const base64Url = token.split('.')[1];
  const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
  const jsonPayload = decodeURIComponent(
    atob(base64)
      .split('')
      .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
      .join('')
  );
  return JSON.parse(jsonPayload);
}

// בדיקה אם המשתמש כבר מחובר
function checkGoogleSignIn() {
  google.accounts.id.initialize({
    client_id: "1093623573018-6s23clvor9u80r08135aelfatuib3a55.apps.googleusercontent.com",
    callback: handleCredentialResponse
  });

  google.accounts.id.renderButton(
    document.querySelector(".g_id_signin"),
    { theme: "outline", size: "large", text: "signin_with", logo_alignment: "left" }
  );

  google.accounts.id.prompt(notification => {
    if(notification.isNotDisplayed() || notification.isSkippedMoment()) {
      // משתמש כבר מחובר – הצג מסך ראשי וטען מודל
      document.getElementById('landing-page').classList.add('hidden');
      document.getElementById('main-page').classList.remove('hidden');

      document.querySelector('.welcome-text').textContent = `ברוכים הבאים ל-${getImaName()}`;
      document.getElementById('user-greeting').textContent = `שלום!`;

      initThreeJSModel();
    }
  });
}

window.onload = function() {
  checkGoogleSignIn();
};

// Three.js - מודל אמא 3D עם אנימציה
function initThreeJSModel() {
  const container = document.getElementById('model-container');
  const scene = new THREE.Scene();
  modelScene = scene;
  const camera = new THREE.PerspectiveCamera(45, container.clientWidth / container.clientHeight, 0.1, 1000);
  camera.position.set(0, 1.5, 3);

  const renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
  renderer.setSize(container.clientWidth, container.clientHeight);
  container.appendChild(renderer.domElement);

  const ambientLight = new THREE.AmbientLight(0xffffff, 1);
  scene.add(ambientLight);

  const loader = new THREE.GLTFLoader();
  loader.load('assets/model.glb', // שם הקובץ שלך
    gltf => {
      const model = gltf.scene;
      model.scale.set(1.2,1.2,1.2);
      model.position.set(0,0,-0.5);
      scene.add(model);

      // mixer לאנימציות
      if(gltf.animations && gltf.animations.length > 0){
        mixer = new THREE.AnimationMixer(model);
        gltf.animations.forEach(clip => {
          const action = mixer.clipAction(clip);
          action.play();
          actions.push(action);
        });
        blinkAction = actions[0]; // אנימציה בסיסית
      }

      // ⚡ הצגת הכפתורים לאחר טעינת המודל
      document.getElementById('interaction-panel').classList.remove('hidden');
    },
    undefined,
    error => console.error('Error loading model:', error)
  );

  clock = new THREE.Clock();

  function animate() {
    requestAnimationFrame(animate);
    if(mixer) mixer.update(clock.getDelta());
    renderer.render(scene, camera);
  }
  animate();

  // התאמה ל‑responsive
  window.addEventListener('resize', () => {
    camera.aspect = container.clientWidth / container.clientHeight;
    camera.updateProjectionMatrix();
    renderer.setSize(container.clientWidth, container.clientHeight);
  });
}

// אנימציה קצרה בזמן שליחה/הקלטה
function playTemporaryAnimation(duration = 1.5){
  if(!mixer || actions.length === 0) return;
  const action = actions[0];
  action.reset();
  action.play();
  setTimeout(()=> action.stop(), duration * 1000);
}

// כפתור שלח טקסט
document.getElementById('send-text').addEventListener('click', () => {
  const text = document.getElementById('text-input').value.trim();
  if(text) {
    document.getElementById('text-input').value = "";
    addMessageToChat("אתה", text);
    playTemporaryAnimation();
    setTimeout(()=> addMessageToChat(getImaName(), "תודה על ההודעה!"), 800);
  }
});

function addMessageToChat(sender, text) {
  const chat = document.getElementById('chat-box');
  const msg = document.createElement('div');
  msg.textContent = `${sender}: ${text}`;
  chat.appendChild(msg);
  chat.scrollTop = chat.scrollHeight;
}

// הקלטת קול
document.getElementById('record-voice').addEventListener('click', async () => {
  try {
    const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
    const mediaRecorder = new MediaRecorder(stream);
    let chunks = [];

    mediaRecorder.ondataavailable = e => chunks.push(e.data);
    mediaRecorder.onstop = () => {
      const blob = new Blob(chunks, { type: 'audio/webm' });
      const url = URL.createObjectURL(blob);
      addAudioToChat("אתה", url);
      playTemporaryAnimation();
      setTimeout(()=> addMessageToChat(getImaName(), "שמעתי את ההקלטה שלך!"), 1200);
    };

    mediaRecorder.start();
    setTimeout(()=> mediaRecorder.stop(), 5000);
    alert("הקלטה התחילה - תסתיים אוטומטית לאחר 5 שניות");

  } catch (err) {
    console.error("Mic error:", err);
    alert("לא ניתן לגשת למיקרופון");
  }
});

function addAudioToChat(sender, url) {
  const chat = document.getElementById('chat-box');
  const msg = document.createElement('div');
  msg.innerHTML = `${sender}: <audio controls src="${url}"></audio>`;
  chat.appendChild(msg);
  chat.scrollTop = chat.scrollHeight;
}
