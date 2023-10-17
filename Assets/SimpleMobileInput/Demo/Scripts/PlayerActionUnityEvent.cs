namespace SimpleMobileInput.Demo
{
    public class PlayerActionUnityEvent : PlayerActionBase
    {
        public void DownAction()
        {
            base.ShootBullet();
        }

        public void UpAction()
        {
            base.StopShootBullet();
        }
    }
}