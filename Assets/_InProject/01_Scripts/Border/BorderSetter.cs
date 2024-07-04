using System.Collections.Generic;
using UnityEngine;

public class BorderSetter : MonoBehaviour
{
    private List<BoxCollider2D> borders = new List<BoxCollider2D>();
    public float borderSize;
    public float spacing;

    private void Awake()
    {
        borders.AddRange(transform.GetComponentsInChildren<BoxCollider2D>());

        SetBorder();
    }

    public void SetBorder()
    {
        Camera cam = Camera.main;
        float aspectRatio = cam.aspect;
        float height = cam.orthographicSize * 2;
        float width = height * aspectRatio;

        foreach (BoxCollider2D border in borders)
        {
            switch (border.name)
            {
                case "Top":
                    border.size = new Vector2(width + spacing, borderSize);
                    border.transform.position = new Vector2(0, (height + borderSize + spacing) / 2);
                    break;
                case "Bottom":
                    border.size = new Vector2(width + spacing, borderSize);
                    border.transform.position = new Vector2(0, -(height + borderSize + spacing) / 2);
                    break;
                case "Left":
                    border.size = new Vector2(borderSize, height + spacing);
                    border.transform.position = new Vector2(-(width + borderSize + spacing) / 2, 0);
                    break;
                case "Right":
                    border.size = new Vector2(borderSize, height + spacing);
                    border.transform.position = new Vector2((width + borderSize + spacing) / 2, 0);
                    break;
            }
        }
    }
}
