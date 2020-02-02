using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public static SkillUI _instance;
    private TweenPosition tween;
    private bool isShow = false;

    private void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransformState()
    {
        if (!isShow)
        {
            isShow = true;
            tween.PlayForward();
        }
        else
        {
            isShow = false;
            tween.PlayReverse();
        }
    }
}
