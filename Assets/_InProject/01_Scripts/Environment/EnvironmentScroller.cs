using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScroller : MonoBehaviour
{
    public float scrollSpeed = 5f; // 배경 스크롤 속도
    public float backgroundHeight; // 배경 이미지의 높이
    public Transform[] backgrounds; // 배경 스프라이트들

    private Vector2 screenBounds;

    void Start()
    {
        // 화면의 크기를 가져옴
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        // 배경 위치 초기화
        for (int i = 1; i < backgrounds.Length; i++)
        {
            backgrounds[i].position = new Vector3(backgrounds[i].position.x, backgrounds[i - 1].position.y + backgroundHeight, backgrounds[i].position.z);
        }
    }

    void Update()
    {
        transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);

        // 배경 스크롤링
        foreach (Transform background in backgrounds)
        {
            // 배경이 화면 아래로 벗어나면 위치 재설정
            if (background.position.y < -screenBounds.y - backgroundHeight / 2)
            {
                RepositionBackground(background);
            }
        }
    }

    void RepositionBackground(Transform background)
    {
        // 가장 위에 있는 배경의 y 위치를 가져옴
        float highestY = GetHighestBackgroundY();
        // 위치를 가장 위로 재설정
        background.position = new Vector3(background.position.x, highestY + backgroundHeight, background.position.z);
    }

    // 가장 위에 있는 배경의 y 위치를 가져옴
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
