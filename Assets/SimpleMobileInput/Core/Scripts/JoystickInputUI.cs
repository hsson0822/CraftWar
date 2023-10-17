using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SimpleMobileInput
{
    [Serializable]
    public class MobileJoystickChangeDirectionEvent : UnityEvent<Vector2> { }

    /// <summary>
    /// Manage the Joystick input UI.
    /// </summary>
    public class JoystickInputUI : MobileInputUIBase, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        #region SerializeFields

        [SerializeField]
        private JoystickInputIdentifier _joystickInputIdentifier = JoystickInputIdentifier.None;
        [SerializeField]
        private GameObject _joystickButtonGameObject = null;
        [SerializeField]
        private GameObject _joystickBackgroundGameObject = null;
        [SerializeField]
        private float _joystickButtonLimit = 2f;
        [SerializeField]
        protected bool _isFixPosition = false;
        [SerializeField]
        private bool _isFollowPlayerTouch = false;
        [SerializeField]
        private float _followPlayerTouchThreshold = 1f;
        [SerializeField]
        private bool _freezeXAxis = false;
        [SerializeField]
        private bool _freezeYAxis = false;
        [SerializeField]
        private bool _isClampToMaxValue = false;
        [SerializeField]
        private float _clampToMaxValueThreshold = 0f;
        [SerializeField]
        private Color _repositioningThresholdGizmoColor = Color.red;
        [SerializeField]
        private Color _joystickButtonLimitGizmoColor = Color.green;
        [SerializeField]
        private Color _targetPositionGizmoColor = Color.cyan;
        [SerializeField]
        private Color _touchZoneLimitGizmoColor = new Color(0f, 1f, 0f, 0.1f);
        [SerializeField]
        private MobileJoystickChangeDirectionEvent _mobileJoystickChangeDirectionEvent = null;

        #endregion

        #region Properties

        public JoystickInputIdentifier JoystickInputIdentifier { get { return _joystickInputIdentifier; } }
        public float JoystickButtonLimit { get {return _joystickButtonLimit; } set {_joystickButtonLimit = value; } }
        public bool IsFixPosition { get { return _isFixPosition; } set { _isFixPosition = value; } }
        public bool FollowPlayerTouch { get { return _isFollowPlayerTouch; } set { _isFollowPlayerTouch = value; } }
        public float FollowPlayerTouchThreshold { get { return _followPlayerTouchThreshold; } set { _followPlayerTouchThreshold = value; } }
        public bool FreezeXAxis { get { return _freezeXAxis; } set { _freezeXAxis = value; } }
        public bool FreezeYAxis { get { return _freezeYAxis; } set { _freezeYAxis = value; } }
        public bool IsClampToMaxValue { get { return _isClampToMaxValue; } set { _isClampToMaxValue = value; } }
        public float ClampToMaxValueThreshold { get { return _clampToMaxValueThreshold; } set { _clampToMaxValueThreshold = value; } }

        #endregion

        #region Variables

        private RectTransform _joystickButtonRect = null;
        private RectTransform _joystickBackgroundRect = null;

        private bool _isTouchStarted = false;
        private bool _isInitialized = false;
        private Vector2 _targetPosition;

        private const float OFFSET_DIRECTION_SPEED_CLAMP = 1f;

        private Vector3 _initialJoystickButtonLocalPosition = Vector3.zero;
        private Vector3 _initialJoystickBackgroundLocalPosition = Vector3.zero;

        private bool _isStaticEventCanBeInvoked = false;
        private bool _isUnityEventCanBeInvoked = false;

        #endregion

        #region Events

        public static event Action<JoystickInputIdentifier, Vector2> OnDirectionChanged = delegate { };

        #endregion

        #region Unity - Methods

        private void Update()
        {
            if (_isTouchStarted)
            {
                Move();
            }
            else
            {
                StopMovement();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_isFixPosition)
            {
                _originalPosition = Helper.GetWorldPositionWithoutZ(_cam, eventData.position);
                Helper.ChangePositionWithoutZ(_joystickButtonRect, _originalPosition);
                Helper.ChangePositionWithoutZ(_joystickBackgroundRect, _originalPosition);
            }

            ActivateControls();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _isTouchStarted = true;
            _targetPosition = Helper.GetWorldPositionWithoutZ(_cam, eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isTouchStarted = false;
            StopMovement();
        }

        #endregion

        #region Public - Methods

        public override void GetInitialComponents()
        {
            base.GetInitialComponents();
            if (_joystickButtonRect == null) { _joystickButtonRect = _joystickButtonGameObject.GetComponent<RectTransform>(); }
            if(_joystickBackgroundRect == null) { _joystickBackgroundRect = _joystickBackgroundGameObject.GetComponent<RectTransform>();}
        }

        #endregion

        #region Protected - Methods - Overrided

        protected override void Initialization()
        {
            base.Initialization();

            _isStaticEventCanBeInvoked = Helper.IsStaticEventAllowed(_eventType);
            _isUnityEventCanBeInvoked = Helper.IsUnityEventAllowed(_eventType) && _mobileJoystickChangeDirectionEvent != null && Helper.IsUnityEventGotListeners(_mobileJoystickChangeDirectionEvent);

            _originalPosition = _joystickButtonGameObject.transform.position;

            _targetPosition = _originalPosition;

            AdjustOrthographicSize();

            _initialJoystickButtonLocalPosition = _joystickButtonGameObject.transform.localPosition;
            _initialJoystickBackgroundLocalPosition = _joystickBackgroundGameObject.transform.localPosition;

            _isInitialized = true;
        }

        protected override void ReverseTouchPanelPositionningOriginalPositionAdjustment()
        {
            base.ReverseTouchPanelPositionningOriginalPositionAdjustment();

            _targetPosition = _originalPosition;

            _joystickBackgroundGameObject.transform.localPosition = _initialJoystickBackgroundLocalPosition;
            _joystickButtonGameObject.transform.localPosition = _initialJoystickButtonLocalPosition;

            Helper.ChangePositionWithoutZ(_joystickButtonRect, _originalPosition);
            Helper.ChangePositionWithoutZ(_joystickBackgroundRect, _originalPosition);
        }

        protected override void DrawGizmos()
        {
            //Joystick Button Limit
            Gizmos.color = _joystickButtonLimitGizmoColor;
            Gizmos.DrawWireSphere(_joystickBackgroundGameObject.transform.position, _joystickButtonLimit);

            //Repositioning Threshold
            Gizmos.color = _repositioningThresholdGizmoColor;
            Gizmos.DrawWireSphere(_joystickBackgroundGameObject.transform.position, _followPlayerTouchThreshold + _joystickButtonLimit);

            //Target position
            Gizmos.color = _targetPositionGizmoColor;
            Vector2 target = _isInitialized ? _targetPosition : new Vector2(_joystickBackgroundGameObject.transform.position.x, _joystickBackgroundGameObject.transform.position.y);
            Gizmos.DrawSphere(target, 0.5f);

            //Touch surface
            Gizmos.color = _touchZoneLimitGizmoColor;
            if (_touchPanelRect == null) { _touchPanelRect = _touchPanelGameObject.GetComponent<RectTransform>(); }
            float width = Helper.GetWorldRectWidth(_touchPanelRect);
            float height = Helper.GetWorldRectHeight(_touchPanelRect);
            Vector3 size = new Vector3(width, height, 0f);
            Gizmos.DrawCube(_touchPanelRect.transform.position, size);
        }

        #endregion

        #region Protected - Methods

        /// <summary>
        /// Method that return the direction of the joystick. Send the events with the direction for player movement.
        /// </summary>
        protected virtual void ChangeDirection(Vector2 direction)
        {
            if (!_isActivated) { return; }

            float x = _freezeXAxis ? 0f : direction.x;
            float y = _freezeYAxis ? 0f : direction.y;

            Vector2 newDirection = new Vector2(x, y);

            if (_isStaticEventCanBeInvoked) { OnDirectionChanged(_joystickInputIdentifier, newDirection); }
            if (_isUnityEventCanBeInvoked) { _mobileJoystickChangeDirectionEvent.Invoke(newDirection); }
        }

        /// <summary>
        /// Move the joystick on drag.
        /// </summary>
        protected virtual void Move()
        {
            Vector2 offset = _targetPosition - _originalPosition;
            Vector2 direction = GetDirection(offset);
            ChangeDirection(direction);

            //Valid if we need to adjust the original position of the joystick.
            if (CanFollowTouch()) { PositionJoystickBackground(_originalPosition, _targetPosition); }

            PositionJoystickButton(_originalPosition, offset);
        }

        /// <summary>
        /// Get the direction of the dragged position.
        /// </summary>
        protected virtual Vector2 GetDirection(Vector2 offset)
        {
            float offsetRatio = OFFSET_DIRECTION_SPEED_CLAMP / _joystickButtonLimit;
            return _isClampToMaxValue ? GetDirectionMaxClamp(offset * offsetRatio, _clampToMaxValueThreshold) : GetDirectionNormalSpeed(offset);
        }

        /// <summary>
        /// Stop the movement of the joystick.
        /// </summary>
        protected virtual void StopMovement()
        {
            PositionJoystickButton(_originalPosition, Vector2.zero);
            ChangeDirection(Vector2.zero);
            DesactivateControls();
        }

        /// <summary>
        /// Determine if the joytick can follow the touch.
        /// </summary>
        protected virtual bool CanFollowTouch()
        {
            return _isFollowPlayerTouch && !_isFixPosition && IsTargetInRepositioningLimit(_followPlayerTouchThreshold);
        }

        #endregion

        #region Private - Methods

        private void AdjustOrthographicSize()
        {
            float baseSizeOrthographic = 10f;
            float sizeOrthographicFactor = (_cam.orthographicSize / baseSizeOrthographic);
            _joystickButtonLimit *= sizeOrthographicFactor;
            _followPlayerTouchThreshold *= sizeOrthographicFactor;
        }

        private void PositionJoystickBackground(Vector2 originalPosition, Vector2 targetPosition)
        {
            Vector3 newBasePosition = Vector2.MoveTowards(originalPosition, targetPosition, (_joystickButtonLimit * 0.25f));

            float cameraBorderOffset = _joystickButtonLimit + _followPlayerTouchThreshold;

            float minXTouchPanelLimit = _touchPanelLimitCorners.upperLeft.x;
            float maxXTouchPanelLimit = _touchPanelLimitCorners.upperRight.x;

            float minYTouchPanelLimit = _touchPanelLimitCorners.lowerLeft.y;
            float maxYTouchPanelLimit = _touchPanelLimitCorners.upperLeft.y;

            float x = Mathf.Clamp(newBasePosition.x, minXTouchPanelLimit, maxXTouchPanelLimit);
            float y = Mathf.Clamp(newBasePosition.y, minYTouchPanelLimit, maxYTouchPanelLimit);

            if (!(x == minXTouchPanelLimit || x == maxXTouchPanelLimit))
            {
                x = Mathf.Clamp(newBasePosition.x, _cameraCorners.upperLeft.x + cameraBorderOffset, _cameraCorners.upperRight.x - cameraBorderOffset);
            }

            if (!(y == minYTouchPanelLimit || y == maxYTouchPanelLimit))
            {
                y = Mathf.Clamp(newBasePosition.y, _cameraCorners.lowerLeft.y + cameraBorderOffset, _cameraCorners.upperLeft.y - cameraBorderOffset);
            }

            newBasePosition = new Vector3(x, y, 0f);

            //Change the position
            Helper.ChangePositionWithoutZ(_joystickBackgroundRect, newBasePosition);
            _originalPosition = newBasePosition;
        }

        private void PositionJoystickButton(Vector2 originalPosition, Vector2 offset)
        {
            Vector2 circleDirection = Vector2.ClampMagnitude(offset, _joystickButtonLimit);

            float x = _freezeXAxis ? 0f : circleDirection.x;
            float y = _freezeYAxis ? 0f : circleDirection.y;

            Vector3 newPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, 0f);

            Helper.ChangePositionWithoutZ(_joystickButtonRect, newPosition);
        }

        private bool IsTargetInRepositioningLimit(float threshold)
        {
            float actualDistance = Vector2.Distance(_originalPosition, _targetPosition);
            return actualDistance > _joystickButtonLimit + threshold;
        }

        private Vector2 GetDirectionNormalSpeed(Vector2 offset)
        {
            return Vector2.ClampMagnitude(offset, OFFSET_DIRECTION_SPEED_CLAMP);
        }

        private Vector2 GetDirectionMaxClamp(Vector2 offset, float threshold)
        {
            Vector2 direction;

            if (offset.x > threshold || offset.x < -threshold || offset.y > threshold || offset.y < -threshold)
            {
                direction = Helper.ClampMagnitude(offset, OFFSET_DIRECTION_SPEED_CLAMP, OFFSET_DIRECTION_SPEED_CLAMP);
            }
            else
            {
                direction = Vector2.ClampMagnitude(offset, OFFSET_DIRECTION_SPEED_CLAMP);
            }

            return direction;
        }

        #endregion
    }
}