using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class OverController : MonoBehaviour {

    private Image again;
    private Image leave;

	// Use this for initialization
	void Start () {
        again = transform.FindChild("Again").GetComponent<Image>();
        leave = transform.FindChild("Leave").GetComponent<Image>();

        EventTriggerListener.Get(again.gameObject).onClick = OnClick;
        EventTriggerListener.Get(leave.gameObject).onClick = OnClick;
    }

    void OnClick(GameObject obj)
    {
       
        switch (obj.name)
        {
            case "Again":
                SceneManager.LoadScene("main");
                Time.timeScale = 1;
              //  transform.DOLocalMove(new Vector3(2000, 0, 0), 0.5f);
                break;
            case "Leave":
                Debug.Log("*********************");
                SceneManager.LoadScene("start");
                break;
        }
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
