using UnityEngine;
using UnityEngine.Events;

namespace SimpleMobileInput
{
    public static class Helper 
    {
        public static Vector3 GetWorldPositionWithoutZ(Camera camera, Vector3 position)
        {
            return camera.ScreenToWorldPoint(new Vector3(position.x, position.y, 0f));
        }

        public static float GetWorldRectWidth(RectTransform rectTransform)
        {
            Corners rectCorners = GetWorldRectCorners(rectTransform);
            return Mathf.Abs(rectCorners.lowerRight.x - rectCorners.lowerLeft.x);
        }

        public static float GetWorldRectHeight(RectTransform rectTransform)
        {
            Corners rectCorners = GetWorldRectCorners(rectTransform);
            return Mathf.Abs(rectCorners.upperRight.y - rectCorners.lowerLeft.y);
        }

        public static Corners GetWorldCameraCorners(Camera camera)
        {
            return new Corners()
            {
                upperLeft = camera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)),
                lowerLeft = camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)),
                upperRight = camera.ViewportToWorldPoint(new Vector3(1f, 1f, 0f)),
                lowerRight = camera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f))
            };
        }

        public static Corners GetWorldRectCorners(RectTransform rectTransform)
        {
            Vector3[] rectCornersArray = new Vector3[4];
            rectTransform.GetWorldCorners(rectCornersArray);

            return new Corners()
            {
                lowerLeft = rectCornersArray[0],
                upperLeft = rectCornersArray[1],
                upperRight = rectCornersArray[2],
                lowerRight = rectCornersArray[3]
            };
        }

        public static void ChangePositionWithoutZ(RectTransform rectTransform, Vector3 newPosition)
        {
            rectTransform.position = newPosition;
            UpdateLocalPositionZ(rectTransform, 0f);
        }

        private static void UpdateLocalPositionZ(RectTransform rectTransform, float value)
        {
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, value);
        }

        public static Vector2 ClampMagnitude(Vector2 vector, float max, float min)
        {
            float sm = vector.sqrMagnitude;
            if (sm > (float)max * (float)max) return vector.normalized * max;
            else if (sm < (float)min * (float)min) return vector.normalized * min;
            return vector;
        }

        public static void ReverseRectTransformX(RectTransform rectTransform)
        {
            float newAnchorMinValue = 1f - rectTransform.anchorMax.x;
            float newAnchorMaxValue = 1f - rectTransform.anchorMin.x;

            float newOffsetMinX = rectTransform.offsetMax.x * -1;
            float newOffsetMaxX = rectTransform.offsetMin.x * -1;

            rectTransform.anchorMin = new Vector2(newAnchorMinValue, rectTransform.anchorMin.y);
            rectTransform.anchorMax = new Vector2(newAnchorMaxValue, rectTransform.anchorMax.y);

            rectTransform.offsetMin = new Vector2(newOffsetMinX, rectTransform.offsetMin.y);
            rectTransform.offsetMax = new Vector2(newOffsetMaxX, rectTransform.offsetMax.y);
        }

        public static bool IsStaticEventAllowed(SimpleMobileInputEventType eventType)
        {
            return eventType == SimpleMobileInputEventType.All || eventType == SimpleMobileInputEventType.StaticEventOnly;
        }

        public static bool IsUnityEventAllowed(SimpleMobileInputEventType eventType)
        {
            return eventType == SimpleMobileInputEventType.All || eventType == SimpleMobileInputEventType.UnityEventOnly;
        }

        public static bool IsUnityEventGotListeners(UnityEvent<Vector2> unityEvent)
        {
            bool hasPersistentTarget = false;
            for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
            {
                if (unityEvent.GetPersistentTarget(i) != null)
                {
                    hasPersistentTarget = true;
                    break;
                }
            }
            return hasPersistentTarget;
        }

        public static bool IsUnityEventGotListeners(UnityEvent unityEvent)
        {
            bool hasPersistentTarget = false;
            for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
            {
                if (unityEvent.GetPersistentTarget(i) != null)
                {
                    hasPersistentTarget = true;
                    break;
                }
            }
            return hasPersistentTarget;
        }

    }
}