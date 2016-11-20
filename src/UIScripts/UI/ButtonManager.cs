using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour {

    private Transform Bag;
    private Transform Store;

    Image[] img = new Image[5];

    private RectTransform canvas;
    Vector3 pos;

    // Use this for initialization
    void Start () {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();

        Bag = GameObject.Find("Bag").transform;
        Store = GameObject.Find("Store").transform;


        img[0] = GameObject.Find("Bag").GetComponent<Image>();
        img[1] = GameObject.Find("Store").GetComponent<Image>();
        img[2] = GameObject.Find("CharacterType").GetComponent<Image>();
        img[3] = GameObject.Find("Equip").GetComponent<Image>();
        img[4] = GameObject.Find("Task").GetComponent<Image>();

        for(int i=0;i<img.Length;i++)
        {
            EventTriggerListener.Get(img[i].gameObject).onClick = OnImgClick;
            EventTriggerListener.Get(img[i].gameObject).onDrag = OnImgDrag;
            EventTriggerListener.Get(img[i].gameObject).onBeginDrag = OnImgBeginDrag;
        }

    }

    void OnImgBeginDrag(GameObject go )
    {
        pos = go.transform.position - Input.mousePosition;
    }
	
    void OnImgDrag(GameObject go)
    {

        go.transform.position = Input.mousePosition + pos;
        GameCommon.Instance.SetPanelIndex(go);
    }

    void OnImgClick(GameObject go)
    {
        GameCommon.Instance.SetPanelIndex(go);
    }



	// Update is called once per frame
	void Update () {
	
	}
}
