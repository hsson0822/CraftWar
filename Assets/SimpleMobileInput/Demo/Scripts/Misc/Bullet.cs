using UnityEngine;

namespace SimpleMobileInput.Demo
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 10f;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _screenBounds;
        private Camera _cam = null;

        private void Start()
        {
            _rigidbody2D = this.GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = new Vector2(_speed, 0f);
            _cam = Camera.main;
            InitializeBounds();
        }

        private void Update()
        {
            CheckScreenLimit();
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
        }

        private void CheckScreenLimit()
        {
            if (transform.position.x > _screenBounds.x * 2f || transform.position.y < _screenBounds.y * -2f)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnScreenChanged()
        {
            InitializeBounds();
        }
    }
}