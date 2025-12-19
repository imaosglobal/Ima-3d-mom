window.onload = function () {
  const landing = document.getElementById('landing-page');
  const main = document.getElementById('main-page');
  const canvasContainer = document.getElementById('canvas-container');

  // Google Sign-In
  google.accounts.id.disableAutoSelect(); 

  function handleCredentialResponse(response) {
    console.log("JWT token:", response.credential);
    localStorage.setItem('userToken', response.credential);

    if (landing && main) {
      landing.classList.add('hidden');
      main.classList.remove('hidden');
    }

    if (canvasContainer && typeof initScene === 'function') {
      initScene(canvasContainer);
    }
  }

  google.accounts.id.initialize({
    client_id: '1093623573018-6s23clvor9u80r08135aelfatuib3a55.apps.googleusercontent.com',
    callback: handleCredentialResponse,
    auto_select: false
  });

  google.accounts.id.renderButton(
    document.getElementById('g_id_signin'),
    { theme: 'outline', size: 'large' }
  );

  google.accounts.id.prompt(); 

  // כניסה כאורח
  const guestBtn = document.getElementById('guest-button');
  guestBtn.addEventListener('click', () => {
    if (landing && main) {
      landing.classList.add('hidden');
      main.classList.remove('hidden');
    }
    if (canvasContainer && typeof initScene === 'function') {
      initScene(canvasContainer);
    }
  });
};
