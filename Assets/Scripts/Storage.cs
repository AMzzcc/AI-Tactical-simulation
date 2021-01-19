using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private static Storage instance = null;

    private Storage() { }

    public static Storage GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Storage>();
            if (instance == null)
            {
                GameObject obj = new GameObject("Storage");
                instance = obj.AddComponent<Storage>();
            }
        }
        return instance;
    }

    private void Start()
    {
        
    }
}
