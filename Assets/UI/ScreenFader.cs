using System.Collections;
using UnityEngine;

namespace Ima.UI
{
    public class ScreenFader : MonoBehaviour
    {
        private Canvas _canvas;
        private UnityEngine.UI.Image _image;

        private void Awake()
        {
            _canvas = new GameObject("ScreenFaderCanvas").AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            DontDestroyOnLoad(_canvas.gameObject);

            var imgObj = new GameObject("FaderImage");
            imgObj.transform.SetParent(_canvas.transform, false);
            _image = imgObj.AddComponent<UnityEngine.UI.Image>();
            _image.color = new Color(0, 0, 0, 0);
            var rt = _image.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }

        public IEnumerator FadeOut(float duration = 1f)
        {
            float t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Clamp01(t / duration);
                _image.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }

        public IEnumerator FadeIn(float duration = 1f)
        {
            float t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                float alpha = 1f - Mathf.Clamp01(t / duration);
                _image.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }
    }
}