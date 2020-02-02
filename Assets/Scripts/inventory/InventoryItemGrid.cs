using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemGrid : MonoBehaviour
{
    public int id = 0;
    public int num = 0;
    private ObjectInfo info = null;
    public UILabel numLabel;

    // Start is called before the first frame update
    void Start()
    {
        numLabel = GetComponentInChildren<UILabel>();
        int a = 0;
    }

    // Update is called once per frame 
    void Update()
    {
        
    }

    public void SetId(int id, int num = 1)
    {
        this.id = id;
        info = ObjectsInfo._instance.GetObjectInfoById(id);
        InventoryItem item = GetComponentInChildren<InventoryItem>();
        item.SetIconName(id, info.icon_name);
        numLabel.enabled = true;
        this.num = num;
        numLabel.text = num.ToString();
    }

    public void PlusNumber(int num = 1)
    {
        this.num += num;
        numLabel.text = this.num.ToString();
    }

    // 用来减去数量
    public bool MinusNumber(int num =1)
    {
        if (this.num >= num)
        {
            this.num -= num;
            numLabel.text = this.num.ToString();
            if (this.num == 0)
            {
                // 清空该物品格子
                ClearInfo();
                // 销毁物品
                GameObject.Destroy(GetComponentInChildren<InventoryItem>().gameObject);
            }
            return true;
        }
        return false;
    }

    // 清空格子内的物品信息
    public void ClearInfo()
    {
        id = 0;
        info = null;
        num = 0;
        numLabel.enabled = false;
    }
}
