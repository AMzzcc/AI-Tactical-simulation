using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DataStorageTypes
{
    /// <summary>
    /// 兵种类型
    /// <para>侦察，突击，大盾，狙击</para>
    /// </summary>
    public enum TroopType
    {
        /// <summary>
        /// 侦察
        /// </summary>
        Scout,
        /// <summary>
        /// 突击
        /// </summary>
        Vanguard,
        /// <summary>
        /// 大盾
        /// </summary>
        Montagne,
        /// <summary>
        /// 狙击
        /// </summary>
        Sniper
    }
    /// <summary>
    /// 内在属性值
    /// <para>体质，敏捷，专注</para>
    /// </summary>
    public class InherentAttributeValues
    {
        /// <summary>
        /// 体质
        /// </summary>
        public float physique;
        /// <summary>
        /// 敏捷度
        /// </summary>
        public float agility;
        /// <summary>
        /// 专注值
        /// </summary>
        public float focus;
    }

    /// <summary>
    /// 外在属性值(显示的)
    /// <para>经验值 血量，士气，护甲，速度 ,准度，负重</para>
    /// </summary>
    public class ExternalAttributeValues
    {        
        /// <summary>
        /// 经验值
        /// </summary>
        public float EXP;
        /// <summary>
        /// 血量
        /// </summary>
        public float health;
        /// <summary>
        /// 士气值
        /// </summary>
        public float morale;
        /// <summary>
        /// 护甲
        /// </summary>
        public float armor;
        /// <summary>
        /// 移动速度
        /// </summary>
        public float speed;
        /// <summary>
        /// 精准度
        /// </summary>
        public float Accuracy;
        /// <summary>
        /// 附中能力
        /// </summary>
        public float floatAccuracy;
    }

    /// <summary>
    /// 等级阶段(所要求的经验值)
    /// <para>2  3  4</para>
    /// </summary>
    public class EXPStage
    {
        public float Level_2;
        public float Level_3;
        public float Level_4;
    }

    /// <summary>
    /// 人物性格
    /// <para>大胆，平衡，保守</para>
    /// <para>100，70，40</para>
    /// </summary>
    public enum Character
    {
        /// <summary>
        /// 大胆的
        /// </summary>
        audacious = 100,
        /// <summary>
        /// 平衡
        /// </summary>
        balance = 70,
        /// <summary>
        /// 保守的
        /// </summary>
        conservative = 40

    }



    /// <summary>
    /// 武器/道具属性 
    /// <para>ID  名字  伤害  有效范围  弹药量  射击/抛射速度  重量  兵种要求</para>
    /// </summary>
    [CreateAssetMenu(fileName = "new WeaponData", menuName = "CustomCreate/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        /// <summary>
        /// 武器引索
        /// </summary>
        public int ID;   //索引
        /// <summary>
        /// 武器名字
        /// </summary>
        public string WeaponName;
        /// <summary>
        /// 伤害
        /// </summary>
        public float Damage;
        /// <summary>
        /// 有效范围
        /// </summary>
        public float EffectiveRange;
        /// <summary>
        /// 最大弹夹容量
        /// </summary>
        public float AmmoCapacity;
        /// <summary>
        /// 设计速度
        /// </summary>
        public float ShootingSpeed;
        /// <summary>
        /// 武器重量
        /// </summary>
        public float Weight;
        /// <summary>
        /// 兵种需求
        /// </summary>
        public TroopType troopType;
    }
}
