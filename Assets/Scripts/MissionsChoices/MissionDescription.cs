using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionDescription : MonoBehaviour
{
    public Text MissionName;



    public void SetMissionName(int chapter, int mission)
    {
        MissionName.text = "第" + (chapter + 1).ToString() + "章：第" + (mission + 1).ToString() + "关";
    }
}
