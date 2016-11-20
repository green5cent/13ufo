using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private static GameController _instance;

    public static GameController Instance
    {
        get{ 
            if (_instance == null)
                _instance = GameObject.FindGameObjectWithTag(Tags.game).GetComponent<GameController>();
        
        return _instance;
        }
    }

    private Transform player;

    public Transform Player
    {
        get
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Transform>();

            return player;
        }
        
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
