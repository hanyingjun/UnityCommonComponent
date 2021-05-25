using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public abstract class TouchControl : CustomControllerControl
{
    private Canvas _canvas = null;
    private RectTransform _rectTransform = null;
}
