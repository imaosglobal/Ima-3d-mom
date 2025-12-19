import { initScene } from './loader.js';

document.addEventListener('DOMContentLoaded', () => {
  const container = document.getElementById('canvas-container');
  initScene(container);
});
