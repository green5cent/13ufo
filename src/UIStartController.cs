using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStartController : MonoBehaviour {

    private Image uiStart;
    private Image uiContinue;
    private Image uiSetting;
    private Image uiExit;

    // Use this for initialization
    void Start ()
    {
        uiStart = GameObject.Find("Start").GetComponent<Image>();
        uiContinue = GameObject.Find("Continue").GetComponent<Image>();
        //uiSetting = GameObject.Find("Setting").GetComponent<Image>();
        uiExit = GameObject.Find("Exit").GetComponent<Image>();

        EventTriggerListener.Get(uiStart.gameObject).onClick = OnClick;
        EventTriggerListener.Get(uiContinue.gameObject).onClick = OnClick;
       // EventTriggerListener.Get(uiSetting.gameObject).onClick = OnClick;
        EventTriggerListener.Get(uiExit.gameObject).onClick = OnClick;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick(GameObject obj)
    {
        switch(obj.name)
        {
            case "Start":
                SceneManager.LoadScene("main");
                break;
            case "Continue":
                SceneManager.LoadScene("main");
                break;
            case "Setting":

                break;
            case "Exit":
                Application.Quit();
                break;
        }
    }
}
