using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInfo : MonoBehaviour
{
    public static ObjectsInfo _instance;
    public TextAsset objectsInfoListText;

    private Dictionary<int, ObjectInfo> objectInfoDict = new Dictionary<int, ObjectInfo>();

    void Awake()
    {
        _instance = this;
        ReadInfo();
    }

    public ObjectInfo GetObjectInfoById(int id)
    {
        ObjectInfo info = null;
        objectInfoDict.TryGetValue(id, out info);
        return info;
    }

    void ReadInfo()
    {
        // 文本文件所有字符
        string text = objectsInfoListText.text;
        string[] strArr = text.Split('\n');
        foreach (string str in strArr)
        {
            // 属性数组
            string[] proArr = str.Split(',');
            ObjectInfo info = new ObjectInfo();

            int id = int.Parse(proArr[0]);
            string name = proArr[1];
            string icon_name = proArr[2];
            string str_type = proArr[3];
            ObjectType type = ObjectType.Drug;
            switch (str_type)
            {
                case "Drug":
                    type = ObjectType.Drug;
                    break;
                case "Equip":
                    type = ObjectType.Equip;
                    break;
                case "Mat":
                    type = ObjectType.Mat;
                    break;
            }
            info.id = id;
            info.name = name;
            info.icon_name = icon_name;
            info.type = type;
            if (type == ObjectType.Drug)
            {
                int hp = int.Parse(proArr[4]);
                int mp = int.Parse(proArr[5]);
                int price_sell = int.Parse(proArr[6]);
                int price_buy = int.Parse(proArr[7]);
                info.hp = hp;
                info.mp = mp;
                info.price_buy = price_buy;
                info.price_sell = price_sell;
            }
            // 将物品添加到字典中，方便索引
            objectInfoDict.Add(id, info);
        }
    }
}

public enum ObjectType
{
    Drug,
    Equip,
    Mat
}

public class ObjectInfo
{
    public int id;
    public string name;
    public string icon_name; // 这个名称是存储在图集中名称
    public ObjectType type;
    public int hp;
    public int mp;
    public int price_sell;
    public int price_buy;
}