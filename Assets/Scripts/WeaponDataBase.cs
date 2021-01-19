using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStorageTypes;

public class WeaponDataBase : MonoBehaviour
{
    //读取excel插件生成的json文件
    public TextAsset weaponDataJson;
    //存储WeaponData的列表
    private List<WeaponData> weaponData = new List<WeaponData>();

    //全局单例模式
    static WeaponDataBase instance;
    public static WeaponDataBase GetInstance()
    {
        if (instance == null)  // 如果没有找到
    　　{                                       
       　　 GameObject go = new GameObject("WeaponDataBase"); // 创建一个新的GameObject
            DontDestroyOnLoad(go);  // 防止被销毁
            instance = go.AddComponent<WeaponDataBase>(); // 将实例挂载到GameObject上
    　　}
    　　return instance;
    }

    //根据ID获取相应的WeaponData对象
    public WeaponData GetWeaponData(int ID)
    {
        return weaponData[ID];
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
