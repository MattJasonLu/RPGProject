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
    private SkillInfo skillInfo;
    private ObjectInfo objectInfo;
    private PlayerStatus ps;

    private void Awake()
    {
        icon = transform.Find("icon").GetComponent<UISprite>();
        icon.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (type == ShortCutType.Drug)
            {
                OnDrugUse();
            } 
            else if (type == ShortCutType.Skill)
            {

            }
        }
    }

    public void SetSkill(int id)
    {
        this.id = id;
        this.skillInfo = SkillsInfo._instance.GetSkillInfoById(id);
        icon.gameObject.SetActive(true);
        icon.spriteName = skillInfo.icon_name;
        type = ShortCutType.Skill;
    }

    public void SetInventory(int id)
    {
        this.id = id;
        objectInfo = ObjectsInfo._instance.GetObjectInfoById(id);
        if (objectInfo.type == ObjectType.Drug)
        {
            icon.gameObject.SetActive(true);
            icon.spriteName = objectInfo.icon_name;
            type = ShortCutType.Drug;
        }

    }

    public void OnDrugUse()
    {
        bool success = Inventory._instance.MinusId(id);
        if (success)
        {
            ps.GetDrug(objectInfo.hp, objectInfo.mp);
        }
        else
        {
            type = ShortCutType.None;
            icon.gameObject.SetActive(false);
            id = 0;
            skillInfo = null;
            objectInfo = null;
        }
    }
}
