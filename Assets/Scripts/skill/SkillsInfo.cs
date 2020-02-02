using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInfo : MonoBehaviour
{
    public static SkillsInfo _instance;
    public TextAsset skillsInfoText;
    private Dictionary<int, SkillInfo> skillInfoDict = new Dictionary<int, SkillInfo>();

    void Awake()
    {
        _instance = this;
        // 初始化技能信息字典
        InitSkillInfoDict();
    }

   void InitSkillInfoDict()
    {

    }
}

public enum ApplicableRole
{
    Swordman,
    Magician
}
// 作用类型
public enum ApplyType
{
    Passive,
    Buff,
    SingleTarget,
    MultiTarget
}
// 作用属性
public enum ApplyProperty
{
    Attack,
    Defend,
    Speed,
    AttackSpeed,
    HP,
    MP
}

public enum ReleaseType
{
    Self,
    Enemy,
    Position
}
// 技能信息
public class SkillInfo
{
    public int id;
    public string name;
    public string icon_name;
    public string des;
    public ApplyType applyType;
    public ApplyProperty applyProperty;
    public int applyValue;
    public int applyTime;
    public int mp;
    public int coldTime;
    public ApplicableRole applicableRole;
    public int level;
    public ReleaseType releaseType;
    public float distance;
}
