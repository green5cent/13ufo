using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Character : MonoBehaviour {

    public bool isOpen;
    Image characterClose;
    private static Character _instance;
    static public Character Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("CharacterType").GetComponent<Character>(); ;
            return _instance;
        }
    }
    Text[] text;

    // Use this for initialization
    void Start()
    {
        isOpen = false;
        characterClose = transform.FindChild("CharacterClose").GetComponent<Image>();
        EventTriggerListener.Get(characterClose.gameObject).onClick = OnCloseClick;
        text = new Text[7];

        text[0] = transform.FindChild("CharacterName").GetComponent<Text>();
        text[1] = transform.FindChild("CharacterHp").GetComponent<Text>();
        text[2] = transform.FindChild("CharacterDem").GetComponent<Text>();
        text[3] = transform.FindChild("CharacterDef").GetComponent<Text>();
        text[4] = transform.FindChild("CharacterExp").GetComponent<Text>();
        text[5] = transform.FindChild("CharacterCoin").GetComponent<Text>();
        text[6] = transform.FindChild("CharacterLevle").GetComponent<Text>();
        UpdateShowUI();

        PlayerInfo.OnInfoChangedEvent += Show;
    }

    void OnCloseClick(GameObject go)
    {
        if (isOpen)
            transform.DOLocalMove(new Vector3(-2000, 0, 0), 0.5f);
        isOpen = !isOpen;
    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        PlayerInfo.OnInfoChangedEvent -= Show;
    }

    void Show(InfoType type)
    {
            UpdateShowUI();
    }

    void UpdateShowUI()
    {
        PlayerInfo info = PlayerInfo.Instance;

        //Debug.Log (info.Name);
        text[0].text = PlayerInfo.Instance.Name;
        text[1].text = PlayerInfo.Instance.Hp.ToString();
        text[2].text = PlayerInfo.Instance.Dem.ToString();
        text[3].text = PlayerInfo.Instance.Def.ToString();
        text[4].text = PlayerInfo.Instance.Exp.ToString();
        text[5].text = PlayerInfo.Instance.Coin.ToString();
        text[6].text = PlayerInfo.Instance.Level.ToString();
    }
}
