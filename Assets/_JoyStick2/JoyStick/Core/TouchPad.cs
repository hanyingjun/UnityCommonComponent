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

        private Vector2 pointerDownPosition;
        private Vector2 m_v2MoveDir = Vector2.zero;

        public Vector2 _dir = Vector2.zero;
        private int fingerId = int.MinValue;                                    // 当前触发摇杆的 pointerId ，预设一个永远无法企及的值

        private bool m_isDraging = false;
        /// <summary>
        /// 摇杆拖拽状态
        /// </summary>
        public bool IsDraging
        {
            get
            {
                return m_isDraging && (fingerId != int.MinValue);
            }
        }

        //protected virtual void Update()
        //{
        //    if (this.IsDraging)
        //    {
        //        OnValueChangedEvent.Invoke(m_v2MoveDir);
        //    }
        //}

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

        protected override void OnBeginDrag(PointerEventData eventData)
        {
            if (fingerId != eventData.pointerId) return;
            pointerDownPosition = eventData.position;
        }

        protected override void OnDrag(PointerEventData eventData)
        {
            if (fingerId != eventData.pointerId) return;
            //string mm = string.Empty;
            Vector2 direction = eventData.position - pointerDownPosition;
            //mm = mm + " y " + direction.y + " nor " + direction.normalized.y;
            direction.Normalize();
            //mm = mm + " nor2 " + direction.y;
            //UnityTools.LogColor(Color.green, mm);
            m_v2MoveDir.y = (0 != (activatedAxis & Direction.Vertical)) ? direction.y : 0;
            m_v2MoveDir.x = (0 != (activatedAxis & Direction.Horizontal)) ? direction.x : 0;
            //this.m_isDraging = true;
            this.OnValueChangedEvent.Invoke(this.m_v2MoveDir);
        }

        protected override void OnEndDrag(PointerEventData eventData)
        {
            if (fingerId != eventData.pointerId) return;
            this.m_isDraging = false;
        }
    }
}
