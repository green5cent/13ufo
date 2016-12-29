using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
public class EquipControl : MonoBehaviour {
    public bool isOpen;
    Image equipClose;
    private static EquipControl _instance;
    public ObjectInfo info;
    List<Transform> cells;
    static public EquipControl Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("Equip").GetComponent<EquipControl>(); ;
            return _instance;
        }
    }

    // Use this for initialization
    void Start () {
        isOpen = false;
        equipClose = transform.FindChild("EquipClose").GetComponent<Image>();
        EventTriggerListener.Get(equipClose.gameObject).onClick = OnCloseClick;
        
        info = new ObjectInfo();
        cells = new List<Transform>();
        for(int i =0;i<transform.FindChild("EquipImg").childCount ;i++)
        {
            cells.Add(transform.FindChild("EquipImg").GetChild(i));
        }
    }

    void OnCloseClick(GameObject go)
    {
        if (isOpen)
            transform.DOLocalMove(new Vector3(2000, 0, 0), 0.5f);
        isOpen = !isOpen;
    }

    public bool AddItem(int id)
    {
        GameObject obj;
        obj = (GameObject)Instantiate(Resources.Load<GameObject>("EquipItem"));
        info = ObjectInfoManager.Instance.GetObjectInfo(id);
        obj.GetComponent<Image>().sprite = Resources.Load<Sprite>(info.IconName);
      //  PlayerInfo.Instance.
        
        for(int i=0;i<cells.Count;i++)
        {
            EquipTypeSet tmp ;
            tmp = cells[i].GetComponent<EquipTypeSet>();
            if (info._ObjectType==ObjectType.Equip&& info._EquipType==tmp.equipType)
            {

                if(cells[i].childCount>0)
                {
                    if (id == cells[i].transform.GetChild(0).GetComponent<EquipItem>().GetId())
                        break;
                    
                    BagControl.Instance.AddItem(cells[i].transform.GetChild(0).GetComponent<EquipItem>().GetId());
                    Destroy(cells[i].GetChild(0).gameObject);
                    cells[i].transform.GetChild(0).GetComponent<EquipItem>().DesInfo();
                }

                obj.transform.SetParent(cells[i]);
                obj.transform.localEulerAngles = Vector3.zero;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<EquipItem>().SetId(id);
                return true;
            }
        }
        Destroy(obj);
        return false;
    }

	// Update is called once per frame
	void Update () {
	}
}
