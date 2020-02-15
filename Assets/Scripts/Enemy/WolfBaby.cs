using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WolfState
{
    Idle,
    Walk,
    Attack,
    Death
}
public class WolfBaby : MonoBehaviour
{
    public WolfState state = WolfState.Idle;
    public int hp = 100;
    public int attack = 10;
    public float miss_rate = 0.2f;
    public string aniname_death;
    public string aniname_idle;
    public string aniname_walk;
    public string aniname_now;
    public string aniname_normalattack;
    public float time_normalattack;
    public string aniname_crazyattack;
    public float time_crazyattack;
    public float crazyattack_rate;
    public string aniname_nowattack;
    public float time = 1;
    public float timer = 0;
    public float speed = 1;
    public AudioClip missSound;
    public GameObject hudtextPrefab;
    public int attack_rate = 1; // 攻击速率每秒
    public Transform target;
    public float minDistance = 2;
    public float maxDistance = 5;


    private Animation animation;
    private CharacterController cc;
    private Color normal;
    private bool is_attacked = false;
    private GameObject hudtextGo;
    private GameObject hudtextFollow;
    private HUDText hudtext;
    private UIFollowTarget followTarget;
    private GameObject body;
    private float attack_timer = 0;


    private void Awake()
    {
        animation = GetComponent<Animation>();
        cc = GetComponent<CharacterController>();
        body = transform.Find("Wolf_Baby").gameObject;
        aniname_now = aniname_idle;
        aniname_nowattack = aniname_nowattack;
        normal = body.GetComponent<Renderer>().material.color;
        hudtextFollow = transform.Find("HUDText").gameObject;
    }

    private void Start()
    {
        hudtextGo = Instantiate(hudtextPrefab, Vector3.zero, Quaternion.identity);
        //hudtextGo.transform.parent = HUDTextParent._instance.gameObject.transform;
        hudtextGo = NGUITools.AddChild(HUDTextParent._instance.gameObject, hudtextPrefab);

        hudtext = hudtextGo.GetComponent<HUDText>();
        followTarget = hudtextGo.GetComponent<UIFollowTarget>();
        followTarget.target = hudtextFollow.transform;
        followTarget.gameCamera = Camera.main;
        //followTarget.uiCamera = UICamera.current.GetComponent<Camera>();
    }

    private void Update()
    {
        if (state == WolfState.Death)
        {
            animation.CrossFade(aniname_death);
        }
        else if (state == WolfState.Attack)
        {
            AutoAttack();
        }
        else
        {
            animation.CrossFade(aniname_now);
            // 控制移动
            if (aniname_now == aniname_walk)
            {
                cc.SimpleMove(transform.forward * speed);
            }
            timer += Time.deltaTime;
            // 表示计时结束
            if (timer > time)
            {
                timer = 0;
                RandomState();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            TakeDamage(1);
        }
    }

    void AutoAttack()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance > maxDistance)
            {
                // 停止自动攻击
                target = null;
                state = WolfState.Idle;
            }
            else if (distance <= minDistance)
            {
                // 自动攻击
                attack_timer += Time.deltaTime;
                animation.CrossFade(aniname_nowattack);
                if (aniname_nowattack == aniname_normalattack)
                {
                    if (attack_timer > time_normalattack)
                    {
                        // 产生伤害
                        aniname_nowattack = aniname_idle;
                    }
                }
                else if (aniname_nowattack == aniname_crazyattack)
                {
                    if (attack_timer > time_crazyattack)
                    {
                        // 产生伤害
                        aniname_nowattack = aniname_idle;
                    }
                }
                if (attack_timer > (1f / attack_rate))
                {
                    RandomAttack();
                    // 再次进行攻击
                    attack_timer = 0;
                }
            }
            else
            {
                // 跟随
                transform.LookAt(target);
                cc.SimpleMove(transform.forward * speed);
                animation.CrossFade(aniname_walk);
            }
        }
        else
        {
            state = WolfState.Idle;
        }
    }

    void RandomAttack()
    {
        float value = UnityEngine.Random.Range(0, 1);
        if (value < crazyattack_rate)
        {
            aniname_nowattack = aniname_crazyattack;
        }
        else
        {
            aniname_nowattack = aniname_normalattack;
        }
    }

    void RandomState()
    {
        int value = UnityEngine.Random.Range(0, 2);
        if (value == 0)
        {
            aniname_now = aniname_idle;
        } 
        else
        {
            // 随机转向
            if (aniname_now != aniname_walk)
            {
                transform.Rotate(transform.up * UnityEngine.Random.Range(0, 360));
            }
            aniname_now = aniname_walk;
        }
    }

    // 受到伤害
    public void TakeDamage(int attack)
    {
        if (state == WolfState.Death)
        {
            return;
        }
        float value = UnityEngine.Random.Range(0f, 1f);
        // 发生miss
        if (value < miss_rate)
        {
            AudioSource.PlayClipAtPoint(missSound, transform.position);
            hudtext.Add("Miss", Color.gray, 1);
        }
        // 击中
        else
        {
            this.hp -= attack;
            StartCoroutine(ShowBodyRed());
            if (hp <= 0)
            {
                state = WolfState.Death;
                // 2秒死亡
                Destroy(this.gameObject, 2);
            }
        }
    }

    // 显示红色身体
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
