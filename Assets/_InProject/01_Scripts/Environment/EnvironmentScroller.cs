using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScroller : MonoBehaviour
{
    public float scrollSpeed = 5f; // ��� ��ũ�� �ӵ�
    public float backgroundHeight; // ��� �̹����� ����
    public Transform[] backgrounds; // ��� ��������Ʈ��

    private Vector2 screenBounds;

    void Start()
    {
        // ȭ���� ũ�⸦ ������
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        // ��� ��ġ �ʱ�ȭ
        for (int i = 1; i < backgrounds.Length; i++)
        {
            backgrounds[i].position = new Vector3(backgrounds[i].position.x, backgrounds[i - 1].position.y + backgroundHeight, backgrounds[i].position.z);
        }
    }

    void Update()
    {
        transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);

        // ��� ��ũ�Ѹ�
        foreach (Transform background in backgrounds)
        {
            // ����� ȭ�� �Ʒ��� ����� ��ġ �缳��
            if (background.position.y < -screenBounds.y - backgroundHeight / 2)
            {
                RepositionBackground(background);
            }
        }
    }

    void RepositionBackground(Transform background)
    {
        // ���� ���� �ִ� ����� y ��ġ�� ������
        float highestY = GetHighestBackgroundY();
        // ��ġ�� ���� ���� �缳��
        background.position = new Vector3(background.position.x, highestY + backgroundHeight, background.position.z);
    }

    // ���� ���� �ִ� ����� y ��ġ�� ������
    float GetHighestBackgroundY()
    {
        float highestY = backgrounds[0].position.y;
        foreach (Transform background in backgrounds)
        {
            if (background.position.y > highestY)
            {
                highestY = background.position.y;
            }
        }
        return highestY;
    }
}
