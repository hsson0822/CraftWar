using UnityEngine;

namespace SimpleMobileInput
{
    public class ActionButtonStaticEventTemplate : MonoBehaviour
    {
        [SerializeField]
        private ActionInputIdentifier _actionInputIdentifier = ActionInputIdentifier.None;

        private void OnEnable()
        {
            ActionInputUI.OnActionDown += OnButtonDown;
            ActionInputUI.OnActionUp += OnButtonUp;
        }

        private void OnDisable()
        {
            ActionInputUI.OnActionDown -= OnButtonDown;
            ActionInputUI.OnActionUp -= OnButtonUp;
        }

        private void OnButtonDown(ActionInputIdentifier actionInputIdentifier)
        {
            if(_actionInputIdentifier == actionInputIdentifier)
            {
                Debug.Log(string.Format("{0} - Button pressed.", actionInputIdentifier.ToString()));
            }
        }

        private void OnButtonUp(ActionInputIdentifier actionInputIdentifier)
        {
            if (_actionInputIdentifier == actionInputIdentifier)
            {
                Debug.Log(string.Format("{0} - Button released.", actionInputIdentifier.ToString()));
            }
        }
    }
}
