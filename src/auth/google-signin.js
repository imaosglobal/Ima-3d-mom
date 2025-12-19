// ===== Google Sign-In Initialization =====
window.onload = function () {
  // אם רוצים לאפשר החלפת חשבון
  google.accounts.id.disableAutoSelect();

  // callback לאחר התחברות
  function handleCredentialResponse(response) {
    console.log("Encoded JWT ID token: " + response.credential);

    // שמירה ב-localStorage
    localStorage.setItem('userToken', response.credential);

    // הסתרת דף כניסה
    const landing = document.getElementById('landing-page');
    if (landing) landing.classList.add('hidden');

    // הצגת הדף הראשי
    const main = document.getElementById('main-page');
    if (main) main.classList.remove('hidden');

    // הפעלה של הרנדר אם רוצים (אפשר לקרוא לפונקציה מ-loader.js)
    if (typeof initScene === 'function') {
      const canvasContainer = document.getElementById('canvas-container');
      initScene(canvasContainer);
    }
  }

  // אתחול Google Sign-In
  google.accounts.id.initialize({
    client_id: 'YOUR_CLIENT_ID_HERE', // החלף ב-Client ID שלך
    callback: handleCredentialResponse
  });

  // הצגת הכפתור
  google.accounts.id.renderButton(
    document.getElementById('g_id_signin'),
    { theme: 'outline', size: 'large' }
  );

  // prompt אוטומטי לבחירת חשבון (יכול להיות false אם רוצים ידני)
  google.accounts.id.prompt(); 
};
