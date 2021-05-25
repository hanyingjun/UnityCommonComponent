namespace zFrame.UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [System.Serializable]
    public class JoyStickEvent : UnityEvent<Vector2> { }

    [System.Flags]
    public enum Direction
    {
        None = 0,
        Horizontal = 1 << 0,
        Vertical = 1 << 1,
        Both = Horizontal | Vertical,
    }

    [RequireComponent(typeof(Image))]
    [DisallowMultipleComponent]
    public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public Direction activatedAxis = Direction.Both;                        // 选择激活的轴向
        [SerializeField] bool dynamic = true;                                   // 动态摇杆
        [SerializeField] RectTransform m_handle = null;                         // 摇杆
        [SerializeField] RectTransform m_backGround = null;                     // 背景
        [SerializeField] float m_nHandleOffset = 0.0f;                          // Handle 偏移
        public JoyStickEvent OnValueChanged = new JoyStickEvent();              // 事件： 摇杆被 拖拽时
        public JoyStickEvent OnPointerDown = new JoyStickEvent();               // 事件： 摇杆被按下时
        public JoyStickEvent OnPointerUp = new JoyStickEvent();                 // 事件： 摇杆上抬起时

        private Vector3 backGroundOriginLocalPostion, pointerDownPosition;
        private int fingerId = int.MinValue;                                    // 当前触发摇杆的 pointerId ，预设一个永远无法企及的值
        private float m_nRadius = 0;                                            // Handle 移动最大半径

        /// <summary>
        /// 摇杆拖拽状态
        /// </summary>
        public bool IsDraging { get { return fingerId != int.MinValue; } }
        /// <summary>
        /// 运行时代码配置摇杆是否为动态摇杆
        /// </summary>
        public bool DynamicJoystick
        {
            set
            {
                if (dynamic != value)
                {
                    dynamic = value;
                    ConfigJoystick();
                }
            }
            get
            {
                return dynamic;
            }
        }

        protected void Awake()
        {
            backGroundOriginLocalPostion = m_backGround.localPosition;
            this.m_nRadius = (m_backGround.sizeDelta.x - m_handle.sizeDelta.x) * 0.5f + m_nHandleOffset;
            if (this.m_nRadius == 0)
                this.m_nRadius = 100;
        }

        protected void Update()
        {
            OnValueChanged.Invoke(m_handle.localPosition / m_nRadius);
        }

        protected void OnDisable()
        {
            RestJoystick(); //意外被 Disable 各单位需要被重置
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerId < -1 || IsDraging) return; //适配 Touch：只响应一个Touch；适配鼠标：只响应左键
            fingerId = eventData.pointerId;
            pointerDownPosition = eventData.position;
            if (dynamic)
            {
                pointerDownPosition[2] = eventData.pressEventCamera?.WorldToScreenPoint(m_backGround.position).z ?? m_backGround.position.z;
                m_backGround.position = eventData.pressEventCamera?.ScreenToWorldPoint(pointerDownPosition) ?? pointerDownPosition; ;
            }
            OnPointerDown.Invoke(eventData.position);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (fingerId != eventData.pointerId) return;
            Vector2 direction = eventData.position - (Vector2)pointerDownPosition; //得到BackGround 指向 Handle 的向量
            float radius = Mathf.Clamp(Vector3.Magnitude(direction), 0, m_nRadius); //获取并锁定向量的长度 以控制 Handle 半径
            Vector2 localPosition = new Vector2()
            {
                x = (0 != (activatedAxis & Direction.Horizontal)) ? (direction.normalized * radius).x : 0, //确认是否激活水平轴向
                y = (0 != (activatedAxis & Direction.Vertical)) ? (direction.normalized * radius).y : 0       //确认是否激活垂直轴向，激活就搞事情
            };
            m_handle.localPosition = localPosition;      //更新 Handle 位置
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (fingerId != eventData.pointerId) return;//正确的手指抬起时才会重置摇杆；
            RestJoystick();
            OnPointerUp.Invoke(eventData.position);
        }

        private void RestJoystick()
        {
            m_backGround.localPosition = backGroundOriginLocalPostion;
            m_handle.localPosition = Vector3.zero;
            fingerId = int.MinValue;
        }

        private void ConfigJoystick() //配置动态/静态摇杆
        {
            if (!dynamic) backGroundOriginLocalPostion = m_backGround.localPosition;
            GetComponent<Image>().raycastTarget = dynamic;
            m_handle.GetComponent<Image>().raycastTarget = !dynamic;
        }
    }
}
