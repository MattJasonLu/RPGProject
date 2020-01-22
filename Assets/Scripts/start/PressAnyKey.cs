using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    // 表示是否有按键按下
    private bool isAnyKeyDown = false;
    private GameObject buttonContainer;

    void Start()
    {
        buttonContainer = transform.parent.Find("btnContainer").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAnyKeyDown)
        {
            if (Input.anyKey)
            {
                ShowButton();
            }
        }
    }

    void ShowButton()
    {
        buttonContainer.SetActive(true);
        gameObject.SetActive(false);
        isAnyKeyDown = true;
    }
}
