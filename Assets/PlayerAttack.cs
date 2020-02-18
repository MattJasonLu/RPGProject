using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    ControlWalk,
    NormalAttack,
    SkillAttack
}

public enum AttackState
{
    // 攻击时候的状态
    Moving,
    Idle,
    Attack
}

public class PlayerAttack : MonoBehaviour
{
    public PlayerState state = PlayerState.ControlWalk;
    public AttackState attack_state = AttackState.Idle;
    public string aniname_normalattack; // 普通攻击的动画
    public string aniname_idle;
    public string aniname_now;
    public float time_normalattack; // 普通攻击的时间
    public float rate_normalattack = 1;
    private float timer = 0;
    public float min_distance = 5; // 默认攻击最小距离
    private Transform target_normalattack;
    private PlayerMove move;
    private bool showEffect = false;
    public GameObject effect;
    private PlayerStatus ps;

    private void Awake()
    {
        move = GetComponent<PlayerMove>();
        ps = GetComponent<PlayerStatus>();
    }

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
                timer = 0;
                showEffect = false;
            }
            else
            {
                state = PlayerState.ControlWalk;
                target_normalattack = null;
            }
        }
        if (state == PlayerState.NormalAttack)
        {
            if (target_normalattack == null)
            {
                state = PlayerState.ControlWalk;
            }
            else
            {
                float distance = Vector3.Distance(transform.position, target_normalattack.position);
                // 进行攻击
                if (distance <= min_distance)
                {
                    attack_state = AttackState.Attack;
                    transform.LookAt(target_normalattack.position);
                    timer += Time.deltaTime;
                    GetComponent<Animation>().CrossFade(aniname_now);
                    if (timer >= time_normalattack)
                    {
                        aniname_now = aniname_idle;
                        if (!showEffect)
                        {
                            showEffect = true;
                            // 播放特效
                            Instantiate(effect, target_normalattack.position, Quaternion.identity);
                            target_normalattack.GetComponent<WolfBaby>().TakeDamage(GetAttack());
                        }
                    }
                    if (timer >= (1f / rate_normalattack))
                    {
                        timer = 0;
                        showEffect = false;
                        aniname_now = aniname_normalattack;
                    }
                }
                else
                {
                    // 走向敌人
                    attack_state = AttackState.Moving;
                    move.SimpleMove(target_normalattack.position);

                }
            }
        }
    }

    public int GetAttack()
    {
        return EquipmentUI._instance.attack + ps.attack + ps.attack_plus;
    }
    
}
