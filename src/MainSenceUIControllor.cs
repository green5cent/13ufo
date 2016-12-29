using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class MainSenceUIControllor : MonoBehaviour
{
    private static MainSenceUIControllor _instance;

    public static MainSenceUIControllor Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag(Tags.UIContronllor).GetComponent<MainSenceUIControllor>();
            }
            return _instance;
        }
    }

    /*playerInfo*/
    Transform Name;
    Transform hp;
    Transform experience;
    Transform grade;

    PlayerInfo playerInfo;
    /*playerInfo*/


    Image knapsackBtn;
    Image equipBtn;
    Image storeBtn;
    Image playerBtn;

    Transform Bag;
    Transform Equip;
    Transform Store;
    Transform CharacterType;

    void Awake()
    {
        Name = transform.Find("playerInfo/Name");
        hp = transform.Find("playerInfo/hp");
        experience = transform.Find("playerInfo/experience");
        grade = transform.Find("playerInfo/grade");



        PlayerInfo.OnInfoChangedEvent += InfoChangeEvent;



        knapsackBtn = transform.Find("BottonUI/knapsackBtn").GetComponent<Image>();
        equipBtn=transform.Find("BottonUI/equipBtn").GetComponent<Image>();
        storeBtn = transform.Find("BottonUI/storeBtn").GetComponent<Image>();
        //playerBtn=transform.Find("BottonUI/playerBtn").GetComponent<Image>();
        playerBtn = GameObject.Find("playerBtn").GetComponent<Image>();
        EventTriggerListener.Get(knapsackBtn.gameObject).onClick = OnImgClick;
        EventTriggerListener.Get(equipBtn.gameObject).onClick = OnImgClick;
        EventTriggerListener.Get(storeBtn.gameObject).onClick = OnImgClick;
        EventTriggerListener.Get(playerBtn.gameObject).onClick = OnImgClick;

        Bag = GameObject.Find("Bag").transform;
        Equip=GameObject.Find("Equip").transform;
        Store = GameObject.Find("Store").transform;
        CharacterType= GameObject.Find("CharacterType").transform;
    }

    #region 玩家属性UI
    void InfoChangeEvent(InfoType type)
    {
        playerInfo = PlayerInfo.Instance;
        switch (type)
        {
            case InfoType.All:
                ChangeText(Name, playerInfo.Name);

                ChangeSlider(hp, playerInfo.Hp / playerInfo.TotalHp);
                ChangeText(hp.Find("Text"), playerInfo.Hp.ToString() + "/" + playerInfo.TotalHp.ToString());

                ChangeSlider(experience, playerInfo.Exp / playerInfo.TotalExp);
                ChangeText(experience.Find("Text"), playerInfo.Exp.ToString() + "/" + playerInfo.TotalExp.ToString());

                ChangeText(grade.Find("Image/Text"), playerInfo.Level.ToString());
                break;

            case InfoType.playerName:
                ChangeText(Name, playerInfo.Name);
                break;

            case InfoType.Hp:
                ChangeSlider(hp, playerInfo.Hp / playerInfo.TotalHp);
                ChangeText(hp.Find("Text"), playerInfo.Hp.ToString() + "/" + playerInfo.TotalHp.ToString());
                break;

            case InfoType.Exp:
                ChangeSlider(experience, playerInfo.Exp / playerInfo.TotalExp);
                ChangeText(experience.Find("Text"), playerInfo.Exp.ToString() + "/" + playerInfo.TotalExp.ToString());
                break;

            case InfoType.Level:
                ChangeText(grade.Find("Image/Text"), playerInfo.Level.ToString());
                break;

        }

    }

    void ChangeText(Transform go, string text)
    {
        go.GetComponent<Text>().text = text;
    }

    void ChangeSlider(Transform go, float value)
    {
        go.GetComponent<Slider>().value = value;
    }
    #endregion

    #region 按钮
    void OnImgClick(GameObject go)
    {
        switch(go.name)
        {
            case "knapsackBtn":
                if (!BagControl.Instance.isOpen)
                    Bag.DOLocalMove(new Vector3(0,0,0), 0.5f);
                else
                    Bag.DOLocalMove(new Vector3(-2000, 0, 0), 0.5f);
                BagControl.Instance.isOpen = !BagControl.Instance.isOpen;
                GameCommon.Instance.SetPanelIndex(Bag.gameObject);
                break;
            case "equipBtn":
                if (!EquipControl.Instance.isOpen)
                    Equip.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
                else
                    Equip.DOLocalMove(new Vector3(2000, 0, 0), 0.5f);
                EquipControl.Instance.isOpen = !EquipControl.Instance.isOpen;
                GameCommon.Instance.SetPanelIndex(Equip.gameObject);
                break;
            case "storeBtn":
                if (!ShopControl.Instance.isOpen)
                    Store.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
                else
                    Store.DOLocalMove(new Vector3(2000, 0, 0), 0.5f);
                ShopControl.Instance.isOpen = !ShopControl.Instance.isOpen;
                GameCommon.Instance.SetPanelIndex(Store.gameObject);
                break;
            case "playerBtn":
                if (!Character.Instance.isOpen)
                    CharacterType.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
                else
                    CharacterType.DOLocalMove(new Vector3(-2000, 0, 0), 0.5f);
                Character.Instance.isOpen = !Character.Instance.isOpen;
                GameCommon.Instance.SetPanelIndex(CharacterType.gameObject);
                break;
        }
    }
    #endregion

}
