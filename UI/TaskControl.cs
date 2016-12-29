using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class TaskControl : MonoBehaviour {
    public bool isOpen;
    Image taskClose;
    private static TaskControl _instance;

    

    static public TaskControl Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("Task").GetComponent<TaskControl>(); ;
            return _instance;
        }
    }
    // Use this for initialization
    void Start () {
        isOpen = false;

        taskClose = transform.FindChild("TaskClose").GetComponent<Image>();
        EventTriggerListener.Get(taskClose.gameObject).onClick = OnCloseClick;

	
	}


    public void CreatTaskItem(int id)
    {
        Transform obj;
        obj = Instantiate(Resources.Load<Transform>("TaskItem"));
        obj.SetParent(GameObject.Find("TaskGrild").transform);
        obj.localScale = Vector3.one;
        obj.localPosition = Vector3.zero;
        TaskInfo info = new TaskInfo();
        info = TaskInfoManager.Instance.GetTaskInfo(id);
        obj.GetComponent<TaskItem>().id = info.Id;
        obj.transform.FindChild("TaskDesText").GetComponent<Text>().text = info.Des;
        if (info._TaskState == TaskState.unFinished)
            obj.transform.FindChild("TaskText").GetComponent<Text>().text = "未完成";
    }


    void OnCloseClick(GameObject go)
    {
        if (isOpen)
            transform.DOLocalMove(new Vector3(2000, 0, 0), 0.5f);
        isOpen = !isOpen;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
