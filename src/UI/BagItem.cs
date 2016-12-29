using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class BagItem : MonoBehaviour{
    
    int id;
    Image img;
    GameObject obj;
    void Start()
    {
        img = transform.GetComponent<Image>();
        obj = null;
        EventTriggerListener.Get(img.gameObject).onRightClick = OnImgClick;
        EventTriggerListener.Get(img.gameObject).onEnter = OnImgEnter;
        EventTriggerListener.Get(img.gameObject).onExit = OnImgExit;
    }

    void OnImgEnter(GameObject go)
    {
        if (obj == null)
        {
            obj = ObjInfoControl.Instance.SetObj(id,transform);
        }
    }

    void OnImgExit(GameObject go)
    {
        if(obj!=null)
        {
            Destroy(obj);
        }
    }

    void OnImgClick(GameObject go)
    {
        if(obj!=null)
            Destroy(obj);
        ObjectInfo info = ObjectInfoManager.Instance.GetObjectInfo(id);

        if (info._ObjectType==ObjectType.Equip&& EquipControl.Instance.AddItem(id))
        {
            ReduceNNum(go);
        }

        if (info._ObjectType == ObjectType.Drug)
        {
            PlayerInfo.Instance.AddHp(info.Hp);
            ReduceNNum(go);
        }
        
    }

    void ReduceNNum(GameObject go)
    {
        string str = transform.GetChild(0).GetComponent<Text>().text;
        int num = int.Parse(str);
        num--;
        transform.GetChild(0).GetComponent<Text>().text = num.ToString();
        if (num == 0)
            Destroy(go);
    }

    public void SetBagItem(int id)
    {
        ObjectInfo info = new ObjectInfo();
        this.id = id;
        info = ObjectInfoManager.Instance.GetObjectInfo(id);
        transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(info.IconName);
        transform.GetChild(0).GetComponent<Text>().text = "";

    }


}
