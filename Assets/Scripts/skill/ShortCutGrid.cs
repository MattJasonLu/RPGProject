using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShortCutType
{
    Skill,
    Drug,
    None
}

public class ShortCutGrid : MonoBehaviour
{
    public KeyCode keyCode;
    private UISprite icon;
    private int id;
    private ShortCutType type = ShortCutType.None;
    private SkillInfo info;

    private void Awake()
    {
        icon = transform.Find("icon").GetComponent<UISprite>();
        icon.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keyCode))
        {

        }
    }

    public void SetSkill(int id)
    {
        this.id = id;
        this.info = SkillsInfo._instance.GetSkillInfoById(id);
        icon.gameObject.SetActive(true);
        icon.spriteName = info.icon_name;
        type = ShortCutType.Skill;
    }

    public void SetInventory(int id)
    {

    }
}
