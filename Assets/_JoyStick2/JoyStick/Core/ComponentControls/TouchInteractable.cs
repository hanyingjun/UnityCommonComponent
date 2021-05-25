using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInteractable : TouchControl, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
{
    public override void ClearValue()
    {
        throw new System.NotImplementedException();
    }
    #region Unity_FUNC
    protected override void Awake()
    {
        base.Awake();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        this.OnPointerDown(eventData);
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        this.OnPointerUp(eventData);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        this.OnPointerEnter(eventData);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        this.OnPointerExit(eventData);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        this.OnBeginDrag(eventData);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        this.OnDrag(eventData);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        this.OnEndDrag(eventData);
    }

    #endregion

    internal virtual bool IsPressed()
    {
        return this.isActiveAndEnabled;
    }

    internal override void OnCustomControllerUpdate()
    {
    }

    protected virtual void OnPointerDown(PointerEventData eventData)
    {
    }

    protected virtual void OnPointerUp(PointerEventData eventData)
    {
    }

    protected virtual void OnPointerEnter(PointerEventData eventData)
    {
    }

    protected virtual void OnPointerExit(PointerEventData eventData)
    {
    }

    protected virtual void OnBeginDrag(PointerEventData eventData)
    {
    }

    protected virtual void OnDrag(PointerEventData eventData)
    {
    }

    protected virtual void OnEndDrag(PointerEventData eventData)
    {
    }
}
