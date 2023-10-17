using UnityEngine;

namespace SimpleMobileInput.Demo
{
    public class PlayerActionStaticEvent : PlayerActionBase
    {
        [SerializeField]
        private ActionInputIdentifier _actionInputIdentifier = ActionInputIdentifier.None;

        protected override void OnEnable()
        {
            ActionInputUI.OnActionDown += OnActionDown;
            ActionInputUI.OnActionUp += OnActionUp;
        }

        protected override void OnDisable()
        {
            ActionInputUI.OnActionDown -= OnActionDown;
            ActionInputUI.OnActionUp -= OnActionUp;
        }

        private void OnActionDown(ActionInputIdentifier actionInputIdentifier)
        {
            if (_actionInputIdentifier != actionInputIdentifier) { return; }
            base.ShootBullet();
        }

        private void OnActionUp(ActionInputIdentifier actionInputIdentifier)
        {
            if (_actionInputIdentifier != actionInputIdentifier) { return; }
            base.StopShootBullet();
        }
    }
}