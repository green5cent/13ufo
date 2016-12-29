using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour {

    public int id;

    Image img;
    
	// Use this for initialization
	void Start () {
        img = transform.GetComponent<Image>();
        EventTriggerListener.Get(img.gameObject).onClick = onImgClick;
        TaskConditional.OnConditionChangedEvent += DealTask;
	}
	
    void onImgClick(GameObject go)
    {
        TaskInfo info = TaskInfoManager.Instance.GetTaskInfo(id);

        if (info._TaskState == TaskState.Finished)
        {
            Destroy(gameObject);
        }
    }

	// Update is called once per frame
	void Update () {


    }

    void DealTask(int id)
    {

        TaskInfo info = TaskInfoManager.Instance.GetTaskInfo(this.id);
        if (info._TaskState == TaskState.unFinished&&this.id==id)
        {
            transform.FindChild("TaskText").GetComponent<Text>().text = "完成";
            TaskInfoManager.Instance.SetTaskState(id, TaskState.Finished);
            foreach(int key in info.RewardObject.Keys)
            {
                int num = info.RewardObject[key];
                for (int i = 0; i < num; i++)
                {
                    BagControl.Instance.AddItem(key);
                }
            }

        }
    }
}
