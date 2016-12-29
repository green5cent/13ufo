using UnityEngine;
using System.Collections;


public class ButtleName
{
    public static string g_ButtleName = "g_Buttle";
}


public class Buttle : MonoBehaviour {

    //子弹的运行速度
    private float m_buttleSpeed = 5.0f;
    private float t;
    private float duration;

    private Vector3 currentDirection;

    void Start()
    {
        t = 0;
        duration = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //子弹跑起来
        GameButtleRun();
        t += Time.deltaTime / duration;
        if(t>1)
        {
            gameObject.SetActive(false);
            t = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
            return;
        if (other.name == "enemy(Clone)")
        {
            other.transform.GetComponent<AIController>().health -= 25;
            GameObject obj = Instantiate(Resources.Load<GameObject>("Hunt"));
            obj.transform.SetParent(other.transform);
            obj.transform.position = other.transform.position;
        }
        
       //if ()
        {
            gameObject.SetActive(false);
        }
    }

   public Vector3 CurrentDirection
    {
        set { currentDirection = value; }
    }

    void GameButtleRun()
    {
        transform.position += currentDirection * m_buttleSpeed * Time.deltaTime;
    }
}
