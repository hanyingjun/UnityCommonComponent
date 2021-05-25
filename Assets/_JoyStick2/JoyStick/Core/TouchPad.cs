namespace zFrame.UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    [DisallowMultipleComponent]
    public class TouchPad : TouchInteractable
    {
        public Direction activatedAxis = Direction.Both;                        // 选择激活的轴向

        public JoyStickEvent OnValueChangedEvent = new JoyStickEvent();
        public JoyStickEvent OnPointerDownEvent = new JoyStickEvent();
        public JoyStickEvent OnPointerUpEvent = new JoyStickEvent();

        private Vector3 pointerDownPosition;
        private Vector2 m_v2MoveDir = Vector2.zero;

        public Vector2 _dir = Vector2.zero;
        private int fingerId = int.MinValue;                                    // 当前触发摇杆的 pointerId ，预设一个永远无法企及的值

        /// <summary>
        /// 摇杆拖拽状态
        /// </summary>
        public bool IsDraging { get { return fingerId != int.MinValue; } }

        protected virtual void Update()
        {
            if (this.IsDraging)
            {
                OnValueChangedEvent.Invoke(m_v2MoveDir);
            }
        }

        protected override void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerId < -1 || IsDraging) return; //适配 Touch：只响应一个Touch；适配鼠标：只响应左键
            fingerId = eventData.pointerId;
            pointerDownPosition = eventData.position;
            OnPointerDownEvent.Invoke(eventData.position);
        }

        protected override void OnPointerUp(PointerEventData eventData)
        {
            if (fingerId != eventData.pointerId) return;
            fingerId = int.MinValue;
            OnPointerUpEvent.Invoke(eventData.position);
        }

        protected override void OnDrag(PointerEventData eventData)
        {
            if (fingerId != eventData.pointerId) return;
            Vector2 direction = eventData.position - (Vector2)pointerDownPosition;
            Vector2 localPosition = new Vector2()
            {
                x = (0 != (activatedAxis & Direction.Horizontal)) ? direction.normalized.x : 0,
                y = (0 != (activatedAxis & Direction.Vertical)) ? direction.normalized.y : 0
            };

            m_v2MoveDir = localPosition;
        }
    }
}
