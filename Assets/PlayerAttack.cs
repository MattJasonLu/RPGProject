using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    ControlWalk,
    NormalAttack,
    SkillAttack,
    Death
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
    public string aniname_death;
    public float time_normalattack; // 普通攻击的时间
    public float rate_normalattack = 1;
    private float timer = 0;
    public float min_distance = 5; // 默认攻击最小距离
    private Transform target_normalattack;
    private PlayerMove move;
    private bool showEffect = false;
    public GameObject effect;
    private PlayerStatus ps;
    public float miss_rate;
    public GameObject hudtextPrefab;
    private GameObject hudtextGo;
    private HUDText hudtext;
    private GameObject hudtextFollow;
    public AudioClip missSound;
    public GameObject body;
    private Color normal;

    private void Awake()
    {
        move = GetComponent<PlayerMove>();
        ps = GetComponent<PlayerStatus>();
        hudtextFollow = transform.Find("HUDText").gameObject;
        normal = body.GetComponent<Renderer>().material.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        hudtextGo = NGUITools.AddChild(HUDTextParent._instance.gameObject, hudtextPrefab);
        hudtext = hudtextGo.GetComponent<HUDText>();
        UIFollowTarget followTarget = hudtextGo.GetComponent<UIFollowTarget>();
        followTarget.target = hudtextFollow.transform;
        followTarget.gameCamera = Camera.main;
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
        else if (state == PlayerState.Death)
        {
            GetComponent<Animation>().CrossFade(aniname_death);
        }
    }

    public int GetAttack()
    {
        return EquipmentUI._instance.attack + ps.attack + ps.attack_plus;
    }

    public void TakeDamage(int attack)
    {
        if (state == PlayerState.Death) return;
        float def = EquipmentUI._instance.defend + ps.defend + ps.defend_plus;
        int temp = (int) (attack * ((200 - def) / 200));
        if (temp < 1)
        {
            temp = 1;
        }
        float value = Random.Range(0f, 1f);
        if (value < miss_rate)
        {
            // miss
            AudioSource.PlayClipAtPoint(missSound, transform.position);
            hudtext.Add("MISS", Color.gray, 1);
        }
        else
        {
            // 受伤
            hudtext.Add("-" + temp, Color.red, 1);
            ps.hp_remain -= temp;
            StartCoroutine(ShowBodyRed());
            if (ps.hp_remain <= 0)
            {
                ps.hp_remain = 0;
                state = PlayerState.Death;
            }
        }
        // 更新UI
        HeadStatusUI._instance.UpdateShow(ps);
    }

    IEnumerator ShowBodyRed()
    {
        body.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        body.GetComponent<Renderer>().material.color = normal;
    }

    private void OnDestroy()
    {
        Destroy(hudtextGo);
    }

}
