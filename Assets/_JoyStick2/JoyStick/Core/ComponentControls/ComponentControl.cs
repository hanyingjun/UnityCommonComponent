using System;
using UnityEngine;

[DisallowMultipleComponent]
[Serializable]
public abstract class ComponentControl : MonoBehaviour, IComponentControl
{
    private int _lastUpdateFrame = -1;

    public abstract void ClearValue();

    void IComponentControl.Update()
    {
        int frameCount = Time.frameCount;
        if (_lastUpdateFrame == frameCount)
            return;

        _lastUpdateFrame = frameCount;
        this.OnUpdate();
    }

    internal virtual void OnUpdate()
    {
    }

    protected virtual void Awake()
    {
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void Start()
    {
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void OnDestroy()
    {
    }
}
