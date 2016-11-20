using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShopItem : MonoBehaviour {

    // Use this for initialization
    Image img;
    public int id;
    void Start () {
        img = transform.GetComponent<Image>();

        EventTriggerListener.Get(img.gameObject).onRightClick = OnImgClick;
	}

    void OnImgClick(GameObject go)
    {
        ObjectInfo info = ObjectInfoManager.Instance.GetObjectInfo(id);
        if (PlayerInfo.Instance.Coin >= info.Buy)
        {
            BagControl.Instance.AddItem(id);
            PlayerInfo.Instance.CoinJ(info.Buy);
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}
