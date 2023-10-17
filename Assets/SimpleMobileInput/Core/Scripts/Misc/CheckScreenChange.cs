using System;
using UnityEngine;

namespace SimpleMobileInput
{
    public class CheckScreenChange : MonoBehaviour
    {
        private int _resolutionX;
        private int _resolutionY;

        public static event Action OnScreenChanged = delegate { };

        #region "Unity Methods"

        void Update()
        {
            CheckScreenChanged();
        }

        #endregion

        private void CheckScreenChanged()
        {
            if (_resolutionX == Screen.width && _resolutionY == Screen.height) return;

            OnScreenChanged();

            _resolutionX = Screen.width;
            _resolutionY = Screen.height;
        }
    }
}