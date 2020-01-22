using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnContainer : MonoBehaviour
{
    // 1.游戏数据的保存，和场景之间游戏数据的传递使用PlayerPrefabs
    // 2.场景的分类
        // 2.1 开始场景
        // 2.2 角色选择界面
        // 2.3 游戏玩家打怪的界面，实际运行场景（村庄，野兽等）

    // 开始新游戏
    public void OnNewGame()
    {
        PlayerPrefs.SetInt("DataFromSave", 0);
        // 加载我们的选择角色的场景

    }

    // 加载已经保存的游戏
    public void OnLoadGame()
    {
        // 表示数据来自保存
        PlayerPrefs.SetInt("DataFromSave", 1);
        // 加载游戏玩家打怪界面
    }
}
