using UnityEngine;

namespace SimpleMobileInput
{
    public class MobileInputUIBase : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField]
        protected Camera _cam = null;
        [SerializeField]
        protected Canvas _parentCanvas = null;
        [SerializeField]
        protected CanvasGroup _canvasGroup = null;
        [SerializeField]
        protected GameObject _touchPanelGameObject = null;
        [SerializeField]
        protected bool _isAlwaysVisible = false;
        [SerializeField]
        [Range(0f, 1f)]
        protected float _pressedAlpha = 1f;
        [SerializeField]
        protected SimpleMobileInputEventType _eventType = SimpleMobileInputEventType.UnityEventOnly;
        [SerializeField]
        protected bool _isGizmosVisible = true;

        #endregion

        #region Properties

        public float PressedAlpha { get { return _pressedAlpha; } set { _pressedAlpha = value; } }
        public bool IsAlwaysVisible 
        { 
            get { return _isAlwaysVisible; } 
            set 
            { 
                _isAlwaysVisible = value;
                if (_isAlwaysVisible)
                {
                    ActivateControls();
                }
                else
                {
                    DesactivateControls();
                }
            } 
        }
        public SimpleMobileInputEventType EventType { get { return _eventType; } }

        #endregion

        #region Variables

        protected Vector2 _originalPosition;
        protected float _initialAlpha = 1f;
        protected RectTransform _touchPanelRect = null;
        protected bool _isActivated = false;
        protected Corners _cameraCorners;
        protected Corners _touchPanelLimitCorners;

        #endregion

        #region Unity - Methods

        protected virtual void Awake()
        {
            GetInitialComponents();
        }

        protected virtual void Start()
        {
            Initialization();
            ValidatePrerequisites();
            DesactivateControls();
        }

        protected virtual void OnEnable()
        {
            CheckScreenChange.OnScreenChanged += OnScreenChanged;
        }

        protected virtual void OnDisable()
        {
            CheckScreenChange.OnScreenChanged -= OnScreenChanged;
        }

        protected virtual void OnDrawGizmos()
        {
            if (_isGizmosVisible)
            {
#if UNITY_EDITOR
                DrawGizmos();
#endif
            }
        }

        #endregion

        #region Public - Methods

        /// <summary>
        /// Reverse the touch panel positionning on the X axis.
        /// </summary>
        public void ReverseTouchPanelPositionning()
        {
            //Keep the offset before reversing the rect transform.
            float offset;
            if (IsTouchPanelLeftPosition())
            {
                offset = Mathf.Abs(_originalPosition.x - _cameraCorners.upperLeft.x);
            }
            else
            {
                offset = Mathf.Abs(_cameraCorners.upperRight.x - _originalPosition.x);
            }

            //Reverse the rect transform.
            Helper.ReverseRectTransformX(_touchPanelRect);

            //Refresh cached touch panel limit corners.
            _touchPanelLimitCorners = Helper.GetWorldRectCorners(_touchPanelRect);

            //Update the original position.
            if (!IsTouchPanelLeftPosition())
            {
                _originalPosition = new Vector2(_cameraCorners.upperRight.x - offset, _originalPosition.y);
                ReverseTouchPanelPositionningOriginalPositionAdjustment();
            }
            else
            {
                _originalPosition = new Vector2(_cameraCorners.upperLeft.x + offset, _originalPosition.y);
                ReverseTouchPanelPositionningOriginalPositionAdjustment();
            }
        }

        public virtual void GetInitialComponents()
        {
            if (_cam == null) { _cam = _parentCanvas.worldCamera; }
            if (_touchPanelRect == null) { _touchPanelRect = _touchPanelGameObject.GetComponent<RectTransform>(); }
            if (_canvasGroup != null) { _initialAlpha = _canvasGroup.alpha; }
        }

        public bool IsCanvasCameraRenderModeValid()
        {
            return _parentCanvas != null && _parentCanvas.renderMode == RenderMode.ScreenSpaceCamera;
        }

        public bool IsCameraValid()
        {
            return _cam != null && _cam.orthographic;
        }

        #endregion

        #region Protected - Methods

        protected virtual void Initialization()
        {
            InitializeCorners();
        }

        protected virtual void ActivateControls()
        {
            _isActivated = true;
            ChangeAlpha(_pressedAlpha);
        }

        protected virtual void DesactivateControls()
        {
            _isActivated = false;
            if (_isAlwaysVisible) 
            {
                ChangeAlpha(_initialAlpha);
                return; 
            }
            ChangeAlpha(0f);
        }

        protected virtual void ChangeAlpha(float alpha)
        {
            _canvasGroup.alpha = alpha;
        }

        /// <summary>
        /// Adjust the original position according to the new position.
        /// </summary>
        protected virtual void ReverseTouchPanelPositionningOriginalPositionAdjustment()
        {
            //To be implented in herited class.
        }

        protected void ValidatePrerequisites()
        {
            if (!IsCanvasCameraRenderModeValid())
            {
                Debug.LogError("Simple Mobile Input : You will need the render mode \"ScreenSpaceCamera\" on your canvas.");
            }

            if (!IsCameraValid())
            {
                Debug.LogError("Simple Mobile Input : You will need a orthographic camera on your canvas.");
            }
        }

        protected virtual void DrawGizmos()
        {
            //To be implented in herited class.
        }

        #endregion

        #region Private - Methods

        /// <summary>
        /// Determine if the touch panel is in the left side of the screen.
        /// </summary>
        private bool IsTouchPanelLeftPosition()
        {
            float cameraCenter = (_cameraCorners.upperLeft.x + _cameraCorners.upperRight.x) * 0.5f;
            return (_touchPanelLimitCorners.upperLeft.x < cameraCenter && _touchPanelLimitCorners.upperRight.x > cameraCenter) || 
                   (_touchPanelLimitCorners.upperLeft.x < cameraCenter && _touchPanelLimitCorners.upperRight.x < cameraCenter);
        }

        private void InitializeCorners()
        {
            _cameraCorners = Helper.GetWorldCameraCorners(_cam);
            _touchPanelLimitCorners = Helper.GetWorldRectCorners(_touchPanelRect);
        }

        private void OnScreenChanged()
        {
            InitializeCorners();
        }

        #endregion
    }
}
