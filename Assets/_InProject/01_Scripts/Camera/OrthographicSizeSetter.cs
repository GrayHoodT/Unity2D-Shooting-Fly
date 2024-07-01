using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthographicSizeSetter : MonoBehaviour
{
    public enum AxisType
    {
        Both,
        Horizontal,
        Vertical
    }

    [SerializeField]
    private AxisType byAxis = AxisType.Both;
    [SerializeField]
    private Vector2 referenceSize;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        // 카메라의 orthographicSize는 해상도 높이의 절반 값이다.

        if (byAxis == AxisType.Both)
        {
            var width = referenceSize.x;
            var height = referenceSize.y;
            if (width > height)
            {
                cam.orthographicSize = height * 0.5f;
            }
            else
            {
                var aspect = (float)Screen.width / (float)Screen.height;
                cam.orthographicSize = (width / aspect) * 0.5f;
            }
        }
        else if (byAxis == AxisType.Horizontal)
        {
            var width = referenceSize.x;
            var height = referenceSize.y;
            var aspect = (float)Screen.width / (float)Screen.height;

            cam.orthographicSize = (width / aspect) * 0.5f;
        }
        else if (byAxis == AxisType.Vertical)
        {
            var height = referenceSize.y;
            
            cam.orthographicSize = height * 0.5f;
        }
    }
}
