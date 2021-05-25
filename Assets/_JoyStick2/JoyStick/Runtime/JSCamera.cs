namespace zFrame.UI
{
    using UnityEngine;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class JSCamera : MonoBehaviour
    {
        [Header("方向灵敏度")]
        [SerializeField] float m_nRotateRange = 120;
        [Header("上下最大视角(Y视角)")]
        [SerializeField] float m_nViewUpRange = 60;
        [SerializeField] float m_nViewDownRange = -45.0f;
        [Header("操作触摸板")]
        [SerializeField] TouchPad m_touchPad = null;
        [SerializeField] float m_nSmoothTime = 0.3f;

        private float m_nRotateX = 0.0f;
        private Vector3 m_velocity = Vector3.zero;
        private Vector3 m_v3Target;

        public void SetTouchPad(TouchPad touchPad)
        {
            m_touchPad = touchPad;
            this.AddEventListener();
        }

        protected void Awake()
        {
            this.AddEventListener();
            m_v3Target = this.transform.localEulerAngles;
            this.m_nRotateX = this.transform.localEulerAngles.x;
        }

        protected void Update()
        {
            this.transform.localEulerAngles = Vector3.SmoothDamp(this.transform.localEulerAngles, m_v3Target, ref m_velocity, m_nSmoothTime);
        }

        private void AddEventListener()
        {
            if (m_touchPad != null)
            {
                m_touchPad.OnPointerDownEvent.AddListener(OnPointerDown);
                m_touchPad.OnValueChangedEvent.AddListener(OnValueChange);
            }
        }

        private void OnPointerDown(Vector2 upWorldPos)
        {
            m_nRotateX = this.transform.localEulerAngles.x;
        }

        /// <summary>
        /// 操作回调
        /// </summary>
        /// <param name="moveDir"></param>
        private void OnValueChange(Vector2 moveDir)
        {
            float rotationx = Mathf.Clamp(m_nRotateX + moveDir.y * m_nViewUpRange, m_nViewDownRange, m_nViewUpRange);
            this.m_v3Target.x = rotationx;
            //this.transform.localEulerAngles = new Vector3(rotationx, 0, 0);
        }
    }
}
