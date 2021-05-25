namespace zFrame.UI
{
    using UnityEngine;

    public class JSMoveMent : MonoBehaviour
    {
        [SerializeField] JoyStick m_joyStick = null;
        [SerializeField] float m_nMoveSpeed = 5.0f;

        private CharacterController m_controller = null;
        private Vector3 m_v3MoveDir = Vector3.zero;

        public void SetJoyStick(JoyStick stick)
        {
            this.m_joyStick = stick;
            this.BindEventListener();
        }

        protected void Awake()
        {
            this.m_controller = this.GetComponent<CharacterController>();
        }

        protected void Start()
        {
            this.BindEventListener();
        }

        private void BindEventListener()
        {
            if (this.m_joyStick != null)
            {
                this.m_joyStick.OnValueChanged.RemoveAllListeners();
                this.m_joyStick.OnValueChanged.AddListener(OnValueChange);
            }
        }

        private void OnValueChange(Vector2 moveDir)
        {
            if (moveDir.magnitude != 0)
            {
                this.m_v3MoveDir.x = moveDir.x;
                this.m_v3MoveDir.z = moveDir.y;
                this.m_controller.Move(this.m_v3MoveDir * m_nMoveSpeed * Time.deltaTime);
                this.transform.rotation = Quaternion.LookRotation(this.m_v3MoveDir);
            }
        }
    }
}
