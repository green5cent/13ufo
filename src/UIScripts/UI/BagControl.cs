using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class BagControl : MonoBehaviour {

    public bool isOpen;
    public List<CELL> cells;
    GameObject parentObj;
    Image bagClose;
    private static BagControl _instance;
    static public BagControl Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("Bag").GetComponent<BagControl>(); ;
            return _instance;
        }
    }

    void Start () {
        isOpen = false;
        bagClose = transform.FindChild("BagClose").GetComponent<Image>();
        EventTriggerListener.Get(bagClose.gameObject).onClick = OnCloseClick;
        cells = new List<CELL>();
        for (int i = 0; ; i++)
        {
            if (transform.Find("knapsack/uicell" + i) == null)
                break;
            cells.Add(new CELL(transform.Find("knapsack/uicell" + i).gameObject, 0));
        }
    }

    void OnCloseClick(GameObject go)
    {
        if(isOpen)
            transform.DOLocalMove(new Vector3(-2000, 0, 0), 0.5f);
        isOpen = !isOpen;
    }

    void OnImageBeginDrag(GameObject go)
    {
        parentObj = go.transform.parent.gameObject;
        go.transform.GetComponent<CanvasGroup>().blocksRaycasts = false; //使goDrag不受到射线检测
        go.GetComponent<Transform>().SetParent(parentObj.transform.parent.parent, true);
        go.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        go.transform.localEulerAngles = Vector3.zero;
    }

    void OnImageDrag(GameObject go)
    {
        go.transform.position = Input.mousePosition;
    }

    void OnImageEndDrag(GameObject go, PointerEventData eventData)
    {
        //   Vector3 pos = GetMousePos(go);

        if (eventData.pointerCurrentRaycast.gameObject != null)  //射线检测要放置的位置上的gameobject
        {
            for (int i = 0; i < cells.Count; i++)
            {
                GameObject obj = eventData.pointerCurrentRaycast.gameObject;
                if (cells[i].obj == obj)
                {
                    go.GetComponent<Transform>().SetParent(obj.GetComponent<Transform>(), true);
                    go.transform.position = obj.transform.position;
                    go.transform.localScale = Vector3.one;
                    go.transform.localEulerAngles = Vector3.zero;
                    go.transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    return;
                }
            }
        }
        go.transform.position = parentObj.transform.position;
        go.GetComponent<Transform>().SetParent(parentObj.transform, true);
        go.transform.localScale = new Vector3(1f, 1f, 1f);
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //把鼠标位置屏幕坐标转为世界坐标


    public void AddItem(int id)
    {
        GameObject obj;
        obj = (GameObject)Instantiate(Resources.Load<GameObject>("uiitem"));
        obj.GetComponent<BagItem>().SetBagItem(id);


        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].obj.transform.childCount == 0)
                cells[i] = new CELL(cells[i].obj, 0);
            if (cells[i].id == 0)
            {
                obj.transform.SetParent(cells[i].obj.transform, true);
                cells[i] = new CELL(cells[i].obj, id);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.transform.position = cells[i].obj.transform.position;
                obj.transform.localEulerAngles = Vector3.zero;
                obj.transform.GetChild(0).GetComponent<Text>().text = "1";
                EventTriggerListener.Get(obj).onBeginDrag = OnImageBeginDrag;
                EventTriggerListener.Get(obj).onDrag = OnImageDrag;
                EventTriggerListener.Get(obj).onEndDrag = OnImageEndDrag;
                return;
            }
            else
            {
                if (cells[i].id==id)
                {
                    int tmp = int.Parse(cells[i].obj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    tmp++;
                    cells[i].obj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = tmp.ToString();
                    Destroy(obj);
                    return;
                }
            }
        }
    }

}

public struct CELL
{
    public GameObject obj;
    public int id;

    public CELL(GameObject obj, int id)
    {
        this.obj = obj;
        this.id = id;
    }
}