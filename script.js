// Google Identity Services
function handleCredentialResponse(response) {
  const data = parseJwt(response.credential);
  console.log("User info:", data);

  // הצגת שם המשתמש
  document.getElementById('user-greeting').textContent = `שלום, ${data.name || "משתמש"}!`;

  document.getElementById('landing-page').classList.add('hidden');
  document.getElementById('main-page').classList.remove('hidden');

  initThreeJSModel();
}

function parseJwt (token) {
  const base64Url = token.split('.')[1];
  const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
  const jsonPayload = decodeURIComponent(atob(base64).split('').map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)).join(''));
  return JSON.parse(jsonPayload);
}

window.onload = function () {
  google.accounts.id.initialize({
    client_id: "1093623573018-6s23clvor9u80r08135aelfatuib3a55.apps.googleusercontent.com",
    callback: handleCredentialResponse
  });
  google.accounts.id.renderButton(
    document.querySelector(".g_id_signin"),
    { theme: "outline", size: "large", text: "signin_with", logo_alignment: "left" }
  );
  google.accounts.id.prompt();
};

// Three.js - מודל אמא 3D
function initThreeJSModel() {
  const container = document.getElementById('model-container');
  const scene = new THREE.Scene();
  const camera = new THREE.PerspectiveCamera(45, container.clientWidth / container.clientHeight, 0.1, 1000);
  camera.position.set(0, 1.5, 3);

  const renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
  renderer.setSize(container.clientWidth, container.clientHeight);
  container.appendChild(renderer.domElement);

  const ambientLight = new THREE.AmbientLight(0xffffff, 1);
  scene.add(ambientLight);

  const loader = new THREE.GLTFLoader();
  loader.load('assets/ima.glb',
    gltf => {
      gltf.scene.scale.set(1.2,1.2,1.2);
      gltf.scene.position.set(0,0,-0.5);
      scene.add(gltf.scene);
    },
    undefined,
    error => console.error('Error loading model:', error)
  );

  function animate() {
    requestAnimationFrame(animate);
    renderer.render(scene, camera);
  }
  animate();
}

// כפתור שלח טקסט
document.getElementById('send-text').addEventListener('click', () => {
  const text = document.getElementById('text-input').value.trim();
  if(text) {
    document.getElementById('text-input').value = "";
    addMessageToChat("אתה", text);

    // תגובת אמא placeholder
    setTimeout(()=> addMessageToChat("אמא", "תודה על ההודעה!"), 800);
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

      // תגובת אמא placeholder
      setTimeout(()=> addMessageToChat("אמא", "שמעתי את ההקלטה שלך!"), 1200);
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
