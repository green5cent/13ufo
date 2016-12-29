using UnityEngine;
using System.Collections;

public class EnemysController : MonoBehaviour {

    private Transform player;
    private GameObject[] enemys;

    private float distance = 20;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tag.player).GetComponent<Transform>();
        enemys = GameObject.FindGameObjectsWithTag(Tag.enemys);
        //int i = 0;
        //while (i<transform.childCount)
        //{
        //    enemys[i] = transform.GetChild(i);
        //    i++;
        //}

	}
	
	// Update is called once per frame
	void Update () {

        foreach (GameObject obj in enemys)
        {
            if (obj != null)
            {
                if (Vector3.Distance(player.position, obj.transform.position) >= distance)
                    obj.SetActive(false);
                else
                {
                    if (!obj.activeSelf)
                        obj.SetActive(true);
                }
            }
        }
    }
}
