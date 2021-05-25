namespace zFrame.UI
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    public class JoyStickDirectionArrow : MonoBehaviour
    {
        private JoyStick joystick;

        protected void Start()
        {
            joystick = GetComponentInParent<JoyStick>();
            if (null == joystick)
            {
                throw new InvalidOperationException("The directional arrow is an optional part of the joystick and it relies on the instance of the joystick!");
            }

            joystick.OnPointerUp.AddListener(OnPointerUp);
            joystick.OnValueChanged.AddListener(UpdateDirectionArrow);
            gameObject.SetActive(false);
        }

        protected void OnDestroy()
        {
            joystick.OnPointerUp.RemoveListener(OnPointerUp);
            joystick.OnValueChanged.RemoveListener(UpdateDirectionArrow);
        }

        // 更新指向器的朝向
        private void UpdateDirectionArrow(Vector2 position)
        {
            if (position.magnitude != 0)
            {
                if (!gameObject.activeSelf)
                {
                    gameObject.SetActive(true);
                }
                transform.localEulerAngles = new Vector3(0, 0, Vector2.Angle(Vector2.right, position) * (position.y > 0 ? 1 : -1));
            }
        }

        void OnPointerUp(Vector2 pos)
        {
            gameObject.SetActive(false);
        }
    }
}
