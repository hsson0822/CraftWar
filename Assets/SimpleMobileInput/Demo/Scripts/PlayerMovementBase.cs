using UnityEngine;

namespace SimpleMobileInput.Demo
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementBase : MonoBehaviour
    {
        [SerializeField]
        protected float _speed = 10f;

        protected Rigidbody2D _rigidbody2D = null;

        protected virtual void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        public virtual void Move(Vector2 moveDirection)
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + (moveDirection * _speed) * Time.fixedDeltaTime);
        }
    }
}