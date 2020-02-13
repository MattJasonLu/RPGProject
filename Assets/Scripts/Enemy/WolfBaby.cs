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
    public float miss_rate = 0.2f;
    public string aniname_death;
    public string aniname_idle;
    public string aniname_walk;
    public string aniname_now;
    public float time = 1;
    public float timer = 0;
    public float speed = 1;
    public float red_time = 1; // 显示被击中的时间
    public float attack_timer = 0;
    public AudioClip missSound;
    public GameObject hudtextPrefab;

    private Animation animation;
    private CharacterController cc;
    private Color normal;
    private bool is_attacked = false;
    private GameObject hudtextGo;
    private GameObject hudtextFollow;
    private HUDText hudtext;
    private UIFollowTarget followTarget;
    private GameObject body;

    private void Awake()
    {
        animation = GetComponent<Animation>();
        cc = GetComponent<CharacterController>();
        body = transform.Find("Wolf_Baby").gameObject;
        aniname_now = aniname_idle;
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

    void RandomState()
    {
        int value = Random.Range(0, 2);
        if (value == 0)
        {
            aniname_now = aniname_idle;
        } 
        else
        {
            // 随机转向
            if (aniname_now != aniname_walk)
            {
                transform.Rotate(transform.up * Random.Range(0, 360));
            }
            aniname_now = aniname_walk;
        }
    }

    // 受到伤害
    public void TakeDamage(int attack)
    {
        float value = Random.Range(0f, 1f);
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
