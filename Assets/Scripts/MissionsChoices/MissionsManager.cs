using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MissionsManager : MonoBehaviour
{
    private static MissionsManager instance;

    /// <summary>
    /// 章节表
    /// </summary>
    public GameObject Chapters;
    /// <summary>
    /// 关卡描述
    /// </summary>
    public GameObject Description;
    /// <summary>
    /// 关卡表，序号与章节序号对应
    /// </summary>
    public List<GameObject> MissionsLists;

    public static MissionsManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 当前的章节序号
    /// </summary>
    private int currentChapter = -1;

    /// <summary>
    /// 当前的关卡
    /// </summary>
    private int currentMission = -1;


    /// <summary>
    /// 设置当前章节
    /// </summary>
    /// <param name="num">第num章，不用-1</param>
    public void SetChapter(int num)
    {
        currentChapter = num - 1;
        MissionsLists[currentChapter].SetActive(true);
        Chapters.SetActive(false);
        Debug.Log("进入第" + (currentChapter + 1).ToString() + "章");
    }

    /// <summary>
    /// 设置当前关卡
    /// </summary>
    /// <param name="num">第num关卡，不用-1</param>
    public void SetMission(int num)
    {
        currentMission = num - 1;
        Description.GetComponent<MissionDescription>().SetMissionName(currentChapter, currentMission);
        Description.SetActive(true);
        Debug.Log("选中" + (currentMission + 1).ToString() + "关");
    }

    /// <summary>
    /// 返回上一级
    /// </summary>
    public void Back()
    {
        if (currentMission >= 0)
        {
            Description.SetActive(false);
            currentMission = -1;
            Debug.Log("返回第" + (currentChapter + 1).ToString() + "章的关卡选择界面");
            return;
        }

        if (currentChapter >= 0)
        {
            MissionsLists[currentChapter].SetActive(false);
            currentChapter = -1;
            Chapters.SetActive(true);
            Debug.Log("返回章节选择界面");
            return;
        }
        //运行到这里应该是在章节按返回，但是目前还没想好怎么干
        Debug.Log("在章节界面按返回");
    }

    /// <summary>
    /// 按当前关卡开始
    /// </summary>
    public void Play()
    {
        Debug.Log("执行第" + (currentChapter + 1).ToString() + "第" + (currentMission + 1).ToString() + "关");
    }

}
