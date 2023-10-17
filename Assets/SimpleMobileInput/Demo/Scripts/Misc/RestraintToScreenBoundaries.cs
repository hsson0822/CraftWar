using UnityEngine;

namespace SimpleMobileInput.Demo
{
    public class RestraintToScreenBoundaries : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer = null;

        private Camera _cam;
        private Vector2 _screenBounds;
        private float _objectWidth;
        private float _objectHeight;

        private void Start()
        {
            _cam = Camera.main;
            InitializeBounds();
        }

        private void LateUpdate()
        {
            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x * -1 + _objectWidth, _screenBounds.x - _objectWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, _screenBounds.y * -1 + _objectHeight, _screenBounds.y - _objectHeight);
            transform.position = viewPos;
        }

        private void OnEnable()
        {
            CheckScreenChange.OnScreenChanged += OnScreenChanged;
        }

        private void OnDisable()
        {
            CheckScreenChange.OnScreenChanged -= OnScreenChanged;
        }

        private void InitializeBounds()
        {
            _screenBounds = _cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _cam.transform.position.z));
            _objectWidth = _spriteRenderer.bounds.extents.x;
            _objectHeight = _spriteRenderer.bounds.extents.y;
        }

        private void OnScreenChanged()
        {
            InitializeBounds();
        }

    }
}