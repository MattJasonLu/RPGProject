using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public static EquipmentUI _instance;
    public GameObject equipmentItem;

    private TweenPosition tween;
    private bool isShow = false;
    private GameObject head;
    private GameObject armor;
    private GameObject rightHand;
    private GameObject leftHand;
    private GameObject shoe;
    private GameObject accessory;
    private PlayerStatus playerStatus;

    private int attack = 0;
    private int defend = 0;
    private int speed = 0;

    void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
        head = transform.Find("Head").gameObject;
        armor = transform.Find("Armor").gameObject;
        rightHand = transform.Find("RightHand").gameObject;
        leftHand = transform.Find("LeftHand").gameObject;
        shoe = transform.Find("Shoe").gameObject;
        accessory = transform.Find("Accessory").gameObject;
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransformState()
    {
        if (!isShow)
        {
            tween.PlayForward();
            isShow = true;
        }
        else
        {
            tween.PlayReverse();
            isShow = false;
        }
    }

    // 处理物品穿戴功能
    public bool Dress(int id)
    {
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        if (info.type != ObjectType.Equip)
        {
            return false;
        }
        if (playerStatus.heroType == HeroType.Magician)
        {
            if (info.applicationType == ApplicationType.Swordman)
            {
                return false;
            }
        } 
        else if (playerStatus.heroType == HeroType.Swordman)
        {
            if (info.applicationType == ApplicationType.Magician)
            {
                return false;
            }
        }

        GameObject parent = null;
        switch (info.dressType)
        {
            case DressType.Head:
                parent = head;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.RightHand:
                parent = rightHand;
                break;
            case DressType.LeftHand:
                parent = leftHand;
                break;
            case DressType.Shoe:
                parent = shoe;
                break;
            case DressType.Accessory:
                parent = accessory;
                break;
        }
        EquipmentItem item = parent.GetComponentInChildren<EquipmentItem>();
        if (item != null)
        {
            Inventory._instance.GetId(item.id);
            item.SetInfo(info);
        }
        else
        {
            GameObject itemGo = NGUITools.AddChild(parent, equipmentItem);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.GetComponent<EquipmentItem>().SetInfo(info);
        }
        UpdateProperty();
        return true;
    }

    public void TakeOff(int id, GameObject go)
    {
        // 追加背包
        Inventory._instance.GetId(id);
        // 销毁物品
        GameObject.Destroy(go);
        UpdateProperty();
    }

    void UpdateProperty()
    {
        this.attack = 0;
        this.defend = 0;
        this.speed = 0;
        EquipmentItem headItem = head.GetComponentInChildren<EquipmentItem>();
        PlusProperty(headItem);
        EquipmentItem armorItem = armor.GetComponentInChildren<EquipmentItem>();
        PlusProperty(armorItem);
        EquipmentItem rightHandItem = rightHand.GetComponentInChildren<EquipmentItem>();
        PlusProperty(rightHandItem);
        EquipmentItem leftHandItem = leftHand.GetComponentInChildren<EquipmentItem>();
        PlusProperty(leftHandItem);
        EquipmentItem shoeItem = shoe.GetComponentInChildren<EquipmentItem>();
        PlusProperty(shoeItem);
        EquipmentItem accessoryItem = accessory.GetComponentInChildren<EquipmentItem>();
        PlusProperty(accessoryItem);
    }

    void PlusProperty(EquipmentItem item)
    {
        if (item != null)
        {
            ObjectInfo equipInfo = ObjectsInfo._instance.GetObjectInfoById(item.id);
            attack += equipInfo.attack;
            defend += equipInfo.defend;
            speed += equipInfo.speed;
        }
        

    }
}
