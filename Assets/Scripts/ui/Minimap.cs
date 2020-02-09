using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private Camera minimapCamera;

    private void Awake()
    {
        minimapCamera = GameObject.FindGameObjectWithTag(Tags.minimap).GetComponent<Camera>();
    }

    public void OnZoomInClick()
    {
        minimapCamera.orthographicSize--;
    }

    public void OnZoomOutClick()
    {
        minimapCamera.orthographicSize++;
    }
}
