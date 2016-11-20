using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskInfoManager : MonoBehaviour {
    private static TaskInfoManager _instance;
    public static TaskInfoManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag(Tags.game).GetComponent<TaskInfoManager>();
            }
            return _instance;
        }
    }

    private TaskInfoManager() { }


    public TextAsset taskInfoText;
    private string taskInfoStr;

    private Dictionary<int, TaskInfo> taskInfoDic = new Dictionary<int, TaskInfo>();



    void Awake()
    {
        taskInfoStr = taskInfoText.ToString();
        SetTaskInfo();
        //Debug.Log(objectInfoDic.Keys.Count);
    }


    public TaskInfo GetTaskInfo(int id)
    {
        TaskInfo info = null;

        taskInfoDic.TryGetValue(id, out info);

        return info;
    }

    void SetTaskInfo()
    {
        string[] rowStrs = taskInfoStr.Split('\n');
        foreach (var item in rowStrs)
        {
            string[] cloumnStrs = item.Split(',');
            TaskInfo info = new TaskInfo();

            info.Id = int.Parse(cloumnStrs[0]);
            info.Name = cloumnStrs[1];
            info.Des = cloumnStrs[2];
            info.DetailDes = cloumnStrs[3];
            info.RewardExp = int.Parse(cloumnStrs[4]);
            info.RewardCoin = int.Parse(cloumnStrs[5]);

            info.RewardObject = new Dictionary<int, int>();
            for(int i=6;i<cloumnStrs.Length;i+=2)
            {
                info.RewardObject.Add(int.Parse(cloumnStrs[i]), int.Parse(cloumnStrs[i + 1]));
            }

            info._TaskState =  TaskState.notHaveTask;
            taskInfoDic.Add(info.Id, info);
        }
    }

    public void SetTaskState(int id ,TaskState state)
    {
        TaskInfo info =  GetTaskInfo(id);
        info._TaskState = state;
        taskInfoDic.Remove(id);
        taskInfoDic.Add(id, info);
    }

}

public enum TaskState
{
    notHaveTask,
    unFinished,
    Finished
}


public class TaskInfo
{

    public int Id
    {
        get;
        set;
    }

    public string Name
    {
        get;
        set;
    }

    public string Des
    {
        get;
        set;
    }

    public int RewardExp
    {
        get;
        set;
    }

    public int RewardCoin
    {
        get;
        set;
    }

    public Dictionary<int,int> RewardObject
    {
        get;
        set;
    }


    public string DetailDes
    {
        get;
        set;
    }

    public TaskState _TaskState
    {
        get;
        set;
    }

}

