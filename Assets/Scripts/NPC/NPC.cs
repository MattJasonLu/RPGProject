using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    protected AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnMouseEnter()
    {
        CursorManager._instance.SetNpcTalk();
    }

    void OnMouseExit()
    {
        CursorManager._instance.SetNormal();
    }
}
