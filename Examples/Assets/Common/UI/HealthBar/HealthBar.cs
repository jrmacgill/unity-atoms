using UnityEngine;
using UnityEngine.UI;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms.Examples
{
    /// <summary>
    /// A healthbar component that sets the fill amount of its associated UI Image accordingly.
    /// </summary>
    [AddComponentMenu("Unity Atoms/Examples/HealthBar")]
    public class HealthBar : MonoBehaviour
    {
        public IntReference InitialHealth { get => _initialHealth; }

        [SerializeField]
        private IntReference _initialHealth = null;

        [SerializeField]
        private Image _image;

        private RectTransform _canvasRectTransform;

        void Awake()
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }

            if (_canvasRectTransform == null)
            {
                var canvas = GetComponentInParent<Canvas>();
                _canvasRectTransform = canvas.gameObject.GetComponent<RectTransform>();
            }
        }

        public void HealthChanged(int health) => _image.fillAmount = 1.0f * health / _initialHealth.Value;

        public void PositionChagned(Vector3 pos)
        {
            Vector2 viewportPos = Camera.main.WorldToViewportPoint(pos);

            Vector2 healthBarPos = new Vector2(
                (viewportPos.x * _canvasRectTransform.sizeDelta.x) - (_canvasRectTransform.sizeDelta.x * 0.5f),
                (viewportPos.y * _canvasRectTransform.sizeDelta.y) - (_canvasRectTransform.sizeDelta.y * 0.5f) + 38f
            );
            gameObject.GetComponent<RectTransform>().anchoredPosition = healthBarPos;
        }
    }
}
