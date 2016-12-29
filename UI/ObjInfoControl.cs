using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjInfoControl : MonoBehaviour {

    private static ObjInfoControl _instance;
    static public ObjInfoControl Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("Canvas").GetComponent<ObjInfoControl>(); ;
            return _instance;
        }
    }

    // Use this for initialization
    void Start () {
	
	}

    public GameObject SetObj(int id,Transform target)
    {
        GameObject obj;
        ObjectInfo info = ObjectInfoManager.Instance.GetObjectInfo(id);
        obj = (GameObject)Instantiate(Resources.Load<GameObject>("ObjInfo"));
        obj.transform.SetParent(GameObject.Find("Canvas").transform);
        obj.transform.localEulerAngles = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.FindChild("ObjImg").GetComponent<Image>().sprite = Resources.Load<Sprite>(info.IconName);
        obj.transform.FindChild("ObjName").GetComponent<Text>().text = info.Name;
        obj.transform.FindChild("ObJDes").GetComponent<Text>().text = info.des;

        float width = obj.GetComponent<RectTransform>().rect.size.x;

        obj.transform.position = target.position + new Vector3(-width/2, 0, 0);
        return obj;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
