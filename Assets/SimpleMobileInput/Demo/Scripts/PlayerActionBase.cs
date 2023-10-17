using UnityEngine;

namespace SimpleMobileInput.Demo
{
    public class PlayerActionBase : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawningPosition = null;
        [SerializeField]
        private GameObject _bulletPrefab = null;
        [SerializeField]
        private Animator _animator = null;

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        public void ShootBullet()
        {
            _animator.SetTrigger("Shoot");
            Instantiate(_bulletPrefab, _spawningPosition); //No object pooling just for demo purpose.
        }

        public void StopShootBullet()
        {
            _animator.SetTrigger("StopShoot");
        }
    }
}