using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SimpleMobileInput
{
    [Serializable]
    public class MobileActionDownEvent : UnityEvent { }
    [Serializable]
    public class MobileActionUpEvent : UnityEvent { }

    /// <summary>
    /// Manage the Action button input UI.
    /// </summary>
    public class ActionInputUI : MobileInputUIBase, IPointerDownHandler, IPointerUpHandler
    {
        #region SerializeFields

        [SerializeField]
        private ActionInputIdentifier _actionInputIdentifier = ActionInputIdentifier.None;
        [SerializeField]
        private GameObject _buttonGameObject = null;
        [SerializeField]
        private bool _isFixPosition = false;
        [SerializeField]
        private MobileActionDownEvent _mobileActionDownEvent = null;
        [SerializeField]
        private MobileActionUpEvent _mobileActionUpEvent = null;
        [SerializeField]
        private Color _targetPositionGizmoColor = Color.cyan;
        [SerializeField]
        private Color _touchZoneLimitGizmoColor = new Color(0f, 1f, 0f, 0.1f);

        #endregion

        #region Properties

        public ActionInputIdentifier ActionInputIdentifier { get {return _actionInputIdentifier; } }
        public bool IsFixPosition { get { return _isFixPosition; } set { _isFixPosition = value; } }

        #endregion

        #region Variables

        private RectTransform _buttonRect = null;
        private Vector3 _initialButtonLocalPosition = Vector3.zero;
        private bool _isStaticEventCanBeInvoked = false;
        private bool _isUnityEventUpCanBeInvoked = false;
        private bool _isUnityEventDownCanBeInvoked = false;

        #endregion

        #region Events

        public static event Action<ActionInputIdentifier> OnActionDown = delegate { };
        public static event Action<ActionInputIdentifier> OnActionUp = delegate { };

        #endregion

        #region Unity - Methods

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isFixPosition)
            {
                _originalPosition = Helper.GetWorldPositionWithoutZ(_cam, eventData.position);
                Helper.ChangePositionWithoutZ(_buttonRect, _originalPosition);
            }

            ActivateControls();
            ActionDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            DesactivateControls();
            ActionUp();
        }

        #endregion

        #region Public - Methods

        public override void GetInitialComponents()
        {
            base.GetInitialComponents();
            if (_buttonRect == null) { _buttonRect = _buttonGameObject.GetComponent<RectTransform>(); }
        }

        #endregion

        #region Protected - Methods - Overrided

        protected override void Initialization()
        {
            base.Initialization();

            _isStaticEventCanBeInvoked = Helper.IsStaticEventAllowed(_eventType);
            _isUnityEventDownCanBeInvoked = Helper.IsUnityEventAllowed(_eventType) && _mobileActionDownEvent != null && Helper.IsUnityEventGotListeners(_mobileActionDownEvent);
            _isUnityEventUpCanBeInvoked = Helper.IsUnityEventAllowed(_eventType) && _mobileActionUpEvent != null && Helper.IsUnityEventGotListeners(_mobileActionUpEvent);

            GetInitialComponents();
            _originalPosition = _buttonGameObject.transform.position;

            _initialButtonLocalPosition = _buttonGameObject.transform.localPosition;
        }

        protected override void ReverseTouchPanelPositionningOriginalPositionAdjustment()
        {
            base.ReverseTouchPanelPositionningOriginalPositionAdjustment();
            Helper.ChangePositionWithoutZ(_buttonRect, _originalPosition);

            _buttonGameObject.transform.localPosition = _initialButtonLocalPosition;
        }

        protected override void DrawGizmos()
        {
            //Target position
            Gizmos.color = _targetPositionGizmoColor;
            Vector2 target = _buttonGameObject.transform.position;
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

        protected virtual void ActionDown()
        {
            if (_isStaticEventCanBeInvoked) { OnActionDown(_actionInputIdentifier); }
            if (_isUnityEventDownCanBeInvoked) {  _mobileActionDownEvent.Invoke();  } 
        }

        protected virtual void ActionUp()
        {
            if (_isStaticEventCanBeInvoked) { OnActionUp(_actionInputIdentifier); }
            if (_isUnityEventUpCanBeInvoked) { _mobileActionUpEvent.Invoke(); }
        }


        #endregion
    }
}