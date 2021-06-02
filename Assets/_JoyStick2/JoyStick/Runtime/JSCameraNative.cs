using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JSCameraNative : MonoBehaviour
{
    [SerializeField] GameObject cameraHandle = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    private Vector3 lastMousePos;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
        }

        if (Input.touchCount > 1)
        {
        }
        else
        {
            if (!isPointOverUI() && Input.GetMouseButtonDown(0))
            {
                this.lastMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                this.lastMousePos = Vector3.zero;
            }

            if (this.lastMousePos != Vector3.zero && Input.GetMouseButton(0))
            {
                Vector2 dv = Input.mousePosition - this.lastMousePos;
                this.lastMousePos = Input.mousePosition;
                dv.x = dv.x / UnityEngine.Screen.width * 2;
                dv.y = dv.y / UnityEngine.Screen.height * 2;

                Vector3 rt = this.cameraHandle.transform.eulerAngles;

                if (rt.x > 180)
                {
                    rt.x = rt.x - 360;
                }

                rt.x = between(rt.x - dv.y * 10, -60, 60);

                rt.y = rt.y + between(dv.x, -0.1f, 0.1f) * 35;

                if (rt.y > 180)
                {
                    rt.y = rt.y - 360;
                }
                else if (rt.y < -180)
                {
                    rt.y = rt.y + 360;
                }
                rt.y = between(rt.y, -25, 25);

                this.cameraHandle.transform.eulerAngles = rt;
            }
        }
    }

    public bool isPointOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public float between(float n, float min, float max)
    {
        return Mathf.Min(Mathf.Max(n, min), max);
    }
}
