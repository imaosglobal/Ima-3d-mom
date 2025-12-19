window.onload = function () {
  google.accounts.id.disableAutoSelect(); // מבטל חשבון אוטומטי

  function handleCredentialResponse(response) {
    console.log("JWT token:", response.credential); // בדיקה
    localStorage.setItem('userToken', response.credential);

    const landing = document.getElementById('landing-page');
    const main = document.getElementById('main-page');

    if (landing && main) {
      landing.classList.add('hidden');
      main.classList.remove('hidden');
    }

    const canvasContainer = document.getElementById('canvas-container');
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

  google.accounts.id.prompt(); // force prompt תמיד
};
