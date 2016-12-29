using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class TaskHaveControl : MonoBehaviour {

    private static TaskHaveControl _instance;

    static public TaskHaveControl Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("HaveTask").GetComponent<TaskHaveControl>(); ;
            return _instance;
        }
    }

    Dictionary<int, TaskState> taskDic;
    int thisId;
    Text detailTask;
    Image taskHaveImg;
    Image CancelImg;

	// Use this for initialization
	void Start () {
        thisId = 0;
        taskDic = new Dictionary<int, TaskState>();
        detailTask = transform.Find("DetailTask").GetComponent<Text>();
        taskHaveImg = transform.Find("TaskHave").GetComponent<Image>();
        CancelImg = transform.Find("Cancel").GetComponent<Image>();
        EventTriggerListener.Get(taskHaveImg.gameObject).onClick = OnHaveClick;
        EventTriggerListener.Get(CancelImg.gameObject).onClick = OnCancelClick;

        for (int i=1001;i<=1002;i++)
        {
            taskDic.Add(i, TaskState.notHaveTask);
        }
    }

   public void SetHaveTask(int id)
    {
        TaskInfo info = TaskInfoManager.Instance.GetTaskInfo(id);
        if(taskDic.ContainsKey(id))
        {
            thisId = id;
            if (info._TaskState==TaskState.notHaveTask)
            {
                transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
               GameCommon.Instance.SetPanelIndex(transform.gameObject);
                detailTask.text = info.DetailDes;
                
            }
        }
        
    }

    void OnHaveClick(GameObject go)
    {
        if(taskDic.ContainsKey(thisId))
        {
            if (taskDic[thisId] == TaskState.notHaveTask)
            {
                TaskInfoManager.Instance.SetTaskState(thisId, TaskState.unFinished);
                TaskControl.Instance.CreatTaskItem(thisId);
            }
        }
        Cancel();
    }

    void OnCancelClick(GameObject go)
    {

        Cancel();
    }
	
    void Cancel()
    {
        thisId = 0;
        transform.DOLocalMove(new Vector3(2000,0,0),0.5f);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
