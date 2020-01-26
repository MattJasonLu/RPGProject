using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarNPC : NPC
{

    public TweenPosition tweenPosition;
    public UILabel desLabel;
    public GameObject acceptBtnGo;
    public GameObject okBtnGo;
    public GameObject cancelBtnGo;
    public PlayerMove playerMove;

    // 是否接入任务
    public bool isInTask = false;
    // 任务进度
    public int killCount = 0;

    private PlayerStatus playerStatus;

    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    // 当鼠标位于这个Collider之上的时候，会在每一帧调用该方法
    void OnMouseOver()
    {
        // 点击目标
        if (Input.GetMouseButtonDown(0))
        {
            ShowQuest();
            if (isInTask)
            {
                ShowTaskProgress();
            }
            else
            {
                ShowTaskDes();
            }
        }
    }

    void ShowQuest()
    {
        tweenPosition.gameObject.SetActive(true);
        tweenPosition.PlayForward();
        playerMove.playerState = PlayerState.Task;
    }

    void HideQuest()
    {
        tweenPosition.PlayReverse();
        playerMove.playerState = PlayerState.Idle;
    }

    // 任务初始状态
    void ShowTaskDes()
    {
        desLabel.text = "任务：\n杀死10只狼\n\n奖励：1000金币";
        okBtnGo.SetActive(false);
        acceptBtnGo.SetActive(true);
        cancelBtnGo.SetActive(true);
    }

    // 任务进行中
    void ShowTaskProgress()
    {
        desLabel.text = "任务：\n你已经杀死了" + killCount + "/10只狼\n\n奖励：1000金币";
        okBtnGo.SetActive(true);
        acceptBtnGo.SetActive(false);
        cancelBtnGo.SetActive(false);
    }

    // 关闭
    public void OnCloseBtnClick()
    {
        HideQuest();
    }

    public void OnAcceptBtnClick()
    {
        ShowTaskProgress();
        isInTask = true;
    }

    public void OnOkBtnClick()
    {
        // 完成任务
        if (killCount >= 10)
        {
            playerStatus.GetCoin(1000);
            killCount = 0;
            isInTask = false;
        }
        else // 未完成任务
        {

        }
        HideQuest();
    }

    public void OnCancelBtnClick()
    {
        HideQuest();
    }

}
