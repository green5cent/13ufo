using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipItem : MonoBehaviour {

  public  int id;
    Image img;
    GameObject obj;

    public int GetId()
    {
       // Debug.Log(id);
        return this.id;
    }

    public void SetId(int i)
    {
        this.id = i;
    }

    // Use this for initialization
    void Start()
    {
        obj = null;
        img = transform.GetComponent<Image>();
        EventTriggerListener.Get(img.gameObject).onRightClick = OnImgClick;
        EventTriggerListener.Get(img.gameObject).onEnter = OnImgEnter;
        EventTriggerListener.Get(img.gameObject).onExit = OnImgExit;

        AddInfo();
     //   PlayerInfo.Instance.AddDef(info.);

    }

    void OnImgEnter(GameObject go)
    {
        if (obj == null && id != 0)
        {
            obj = ObjInfoControl.Instance.SetObj(id,transform);
        }
    }

    void OnImgExit(GameObject go)
    {
        if (obj != null)
        {
            Destroy(obj);
        }
    }

    void OnImgClick(GameObject go)
    {
        if (obj != null)
            Destroy(obj);
        BagControl.Instance.AddItem(id);
        DesInfo();
        Destroy(gameObject);
    }

     void AddInfo()
    {
        ObjectInfo info = new ObjectInfo();
        info = ObjectInfoManager.Instance.GetObjectInfo(id);
        PlayerInfo.Instance.AddDem(info.dam);
    }

    public void DesInfo()
    {
        ObjectInfo info = new ObjectInfo();
        info = ObjectInfoManager.Instance.GetObjectInfo(id);
        PlayerInfo.Instance.DesDem(info.dam);
    }


	
	// Update is called once per frame
	void Update () {
	
	}
}
