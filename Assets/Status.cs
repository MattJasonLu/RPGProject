using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public static Status _instance;
    private TweenPosition tween;
    private bool isShow = false;

    private UILabel attackLabel;
    private UILabel defendLabel;
    private UILabel speedLabel;
    private UILabel pointRemainLabel;
    private UILabel summaryLabel;

    private GameObject attackBtnGo;
    private GameObject defendBtnGo;
    private GameObject speedBtnGo;

    private PlayerStatus playerStatus;

    private void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
        attackLabel = transform.Find("ack").GetComponent<UILabel>();
        defendLabel = transform.Find("def").GetComponent<UILabel>();
        speedLabel = transform.Find("spd").GetComponent<UILabel>();
        pointRemainLabel = transform.Find("point_remain").GetComponent<UILabel>();
        summaryLabel = transform.Find("summary").GetComponent<UILabel>();
        attackBtnGo = transform.Find("ack_plus").gameObject;
        defendBtnGo = transform.Find("def_plus").gameObject;
        speedBtnGo = transform.Find("spd_plus").gameObject;

        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();

    }

    private void Start()
    {
        UpdateShow();
    }

    public void TransformStatus()
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

    // 更新显示 根据playerstatus的属性值
    void UpdateShow()
    {
        attackLabel.text = playerStatus.attack + " + " + playerStatus.attack_plus;
        defendLabel.text = playerStatus.defend + " + " + playerStatus.defend_plus;
        speedLabel.text = playerStatus.speed + " + " + playerStatus.speed_plus;

        pointRemainLabel.text = playerStatus.point_remain.ToString();
        summaryLabel.text = "伤害：" + (playerStatus.attack + playerStatus.attack_plus) +
            " 防御：" + (playerStatus.defend + playerStatus.defend_plus) +
            " 速度：" + (playerStatus.speed + playerStatus.speed_plus);

        if (playerStatus.point_remain > 0)
        {
            attackBtnGo.SetActive(true);
            defendBtnGo.SetActive(true);
            speedBtnGo.SetActive(true);
        }
        else
        {
            attackBtnGo.SetActive(false);
            defendBtnGo.SetActive(false);
            speedBtnGo.SetActive(false);
        }
    }

    public void OnAttackPlusBtnClick()
    {
        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.attack_plus++;
            UpdateShow();
        }
    }
    public void OnDefendPlusBtnClick()
    {
        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.defend_plus++;
            UpdateShow();
        }
    }
    public void OnSpeedPlusBtnClick()
    {
        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.speed_plus++;
            UpdateShow();
        }
    }
}
