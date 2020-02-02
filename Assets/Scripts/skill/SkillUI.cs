using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public static SkillUI _instance;
    public int[] magicianSkillIdList;
    public int[] swordmanSkillIdList;
    public UIGrid grid;
    public GameObject skillItemPrefab;
    private TweenPosition tween;
    private bool isShow = false;
    private PlayerStatus playerStatus;

    private void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
    }

    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        int[] idList = null;
        switch (playerStatus.heroType)
        {
            case HeroType.Magician:
                idList = magicianSkillIdList;
                break;
            case HeroType.Swordman:
                idList = swordmanSkillIdList;
                break;
        }
        foreach (int id in idList)
        {
            GameObject itemGo = NGUITools.AddChild(grid.gameObject, skillItemPrefab);
            grid.AddChild(itemGo.transform);
            itemGo.GetComponent<SkillItem>().SetId(id);
        }
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
