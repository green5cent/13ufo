using UnityEngine;
using System.Collections;

public class GameCommon : MonoBehaviour {

    private static GameCommon _instance;

    public static GameCommon Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindGameObjectWithTag(Tags.game).GetComponent<GameCommon>();

            return _instance;
        }
    }
    private RectTransform canvas;

   public  void SetPanelIndex(GameObject go)
    {
        int myIndex = go.transform.GetSiblingIndex();
        foreach (RectTransform item in canvas)
        {
            int itemIndex = item.GetSiblingIndex();

            if (myIndex < itemIndex)
            {
                myIndex = itemIndex;
            }
        }

        go.transform.SetSiblingIndex(myIndex + 1);
        // this.gameObject.SetActive(true);
        //  transform.DOPlayForward();
    }

    // Use this for initialization
    void  Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
