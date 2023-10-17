using UnityEngine;

namespace SimpleMobileInput
{
    public class JoystickMovementUnityEventTemplate : MonoBehaviour
    {
        protected float speed = 5f;

        protected Rigidbody2D rigid = null;

        protected virtual void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public void OnDirectionChanged(Vector2 direction)
        {
            //Debug.Log(string.Format("OnDirectionChanged : {0}", direction));
            rigid.MovePosition(rigid.position + (direction * speed) * Time.fixedDeltaTime); ;
        }
    }
}
