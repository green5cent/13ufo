using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ShopControl : MonoBehaviour
{

    BagControl bag;
    Image img;
    Text coinText;
    void Start()
    {
        bag = GameObject.Find("Bag").GetComponent<BagControl>();
        img = transform.FindChild("StoreClose").GetComponent<Image>();
        EventTriggerListener.Get(img.gameObject).onClick = OnClick;
        coinText = transform.FindChild("Coin").GetComponent<Text>();
        PlayerInfo.OnInfoChangedEvent += UpdateCoin;
        UpdateCoin(InfoType.Coin);
        for (int i=101;i<=112;i++)
        {
            Transform obj;
            obj = Instantiate(Resources.Load<Transform>("ShopItem"));
            obj.SetParent(GameObject.Find("ShopingGrild").transform);
            obj.localScale = Vector3.one;
            obj.localPosition = Vector3.zero;
            ObjectInfo info = new ObjectInfo();
            info = ObjectInfoManager.Instance.GetObjectInfo(i);
            obj.GetComponent<ShopItem>().id = info.Id;
            obj.FindChild("GoodImg").GetComponent<Image>().sprite = Resources.Load<Sprite>(info.IconName) as Sprite;
            obj.FindChild("GoodText").GetComponent<Text>().text = info.des;
            obj.FindChild("GoodCoinText").GetComponent<Text>().text = info.Buy.ToString();
        }
    }

    void UpdateCoin(InfoType info)
    {
        if (info == InfoType.Coin)
            coinText.text = PlayerInfo.Instance.Coin.ToString();
    }

    void OnClick(GameObject go)
    {
        transform.DOLocalMove(new Vector3(2000, 0, 0), 0.5f);
    }
}


