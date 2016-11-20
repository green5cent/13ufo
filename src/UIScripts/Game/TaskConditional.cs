using UnityEngine;
using System.Collections;

public class TaskConditional : MonoBehaviour
{
    private static TaskConditional _instance;

    public static TaskConditional Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindGameObjectWithTag(Tags.game).GetComponent<TaskConditional>();

            return _instance;
        }
    }

    public delegate void OnConditionChanged(int id);
    public static event OnConditionChanged OnConditionChangedEvent;

    int killNumber;
    int titleDeedNum; //地契

    public int KillNumber
    {
        get
        {
            return killNumber;
        }
        set
        {
            killNumber = value;
        }
    }


    public int TitleDeedNum
    {
        get
        {
            return titleDeedNum;
        }
        set
        {
            titleDeedNum = value;
        }
    }

    public void AddkillNumber(int value)
    {
        KillNumber+=value;
        if (KillNumber >= 5)
            OnConditionChangedEvent(1001);
    }
    public void AddTitleDeedNum(int value)
    {
        TitleDeedNum += value;
        if (KillNumber >= 2)
            OnConditionChangedEvent(1002);
    }

}
