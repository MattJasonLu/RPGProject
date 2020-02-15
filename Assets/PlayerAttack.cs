using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    ControlWalk,
    NormalAttack,
    SkillAttack
}

public class PlayerAttack : MonoBehaviour
{
    public PlayerState state = PlayerState.ControlWalk;
    public string aniname_normalattack; // 普通攻击的动画
    public float time_normalattack; // 普通攻击的时间
    private float timer = 0;
    public float min_distance = 5; // 默认攻击最小距离
    private Transform target_normalattack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 点击敌人时
        if (Input.GetMouseButtonDown(0))
        {
            // 做射线检测
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == Tags.enemy)
            {
                // 当我们点击敌人时
                target_normalattack = hitInfo.collider.transform;
                // 进入普通攻击模式
                state = PlayerState.NormalAttack;
            }
            else
            {
                state = PlayerState.ControlWalk;
                target_normalattack = null;
            }
        }
        if (state == PlayerState.NormalAttack)
        {

        }
    }
}
