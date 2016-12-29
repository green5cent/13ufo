using UnityEngine;
using System.Collections;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    //private static PlayerController _instance;

    //public static PlayerController Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //            _instance = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerController>();

    //        return _instance;
    //    }
    //}

    private Animator animator;
    private Rigidbody2D playerController;
    public Transform myTransform;
    private Camera maincamera;

    private Vector2 PreMove;
    float h;
    float v;

    public float health;   //血
    private Transform OverPlane;


    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        maincamera = Camera.main;
        animator = this.GetComponent<Animator>();
        playerController = this.GetComponent<Rigidbody2D>();
        myTransform = this.transform;
        PreMove = myTransform.position;
        OverPlane = GameObject.Find("Over").transform;
        OverPlane.gameObject.SetActive(false);
    }


    void Pause()
    {
        Time.timeScale = 0;
        OverPlane.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
      //  PlayerInfo.Instance.Hp
        if (health<=0)
        {
            AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateinfo.IsName("WalkLeft") || stateinfo.IsName("IdleLeft")|| stateinfo.IsName("ShootLeft"))
            {
                animator.Play("DeadLeft");
            }
            if (stateinfo.IsName("WalkRight") || stateinfo.IsName("IdleRight")||stateinfo.IsName("ShootRight"))
            {
                animator.Play("DeadRight");
            }
            if (stateinfo.IsName("WalkDown") || stateinfo.IsName("IdleDown")|| stateinfo.IsName("ShootDown"))
            {
                animator.Play("DeadDown");
            }
            if (stateinfo.IsName("WalkUp") || stateinfo.IsName("IdleUp") || stateinfo.IsName("ShootUp"))
            {
                animator.Play("DeadUp");
            }
            //OverPlane.DOLocalMove(new Vector3(0, 0, 0), 1f);
          //  Time.timeScale = 0;
            Invoke("Pause",1f);
            
            

        }


        

        if (Input.GetKeyDown(KeyCode.J))
        {
            
            GameObject buttle = ButtleManager._Instance.GetGameObjectInDic(ButtleName.g_ButtleName);

            AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);
            buttle.transform.localScale = new Vector3(0.05f, 0.05f, 1);
            if (stateinfo.IsName("WalkLeft") || stateinfo.IsName("IdleLeft") || stateinfo.IsName("ShootLeft"))
            {
               // animator.Play("ShootLeft");
                buttle.transform.position = myTransform.position + new Vector3(-0.4f, -0.2f, 0);
                buttle.GetComponent<Buttle>().CurrentDirection = -Vector3.right;
                buttle.transform.rotation = Quaternion.Euler( new Vector3(0,0,0));
            }
            if (stateinfo.IsName("WalkRight") || stateinfo.IsName("IdleRight") || stateinfo.IsName("ShootRight"))
            {
                //animator.Play("ShootRight");
                buttle.transform.position = myTransform.position + new Vector3(0.4f, -0.2f, 0);
                buttle.GetComponent<Buttle>().CurrentDirection =Vector3.right;
                buttle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }
            if (stateinfo.IsName("WalkDown") || stateinfo.IsName("IdleDown") || stateinfo.IsName("ShootDown"))
            {
               // animator.Play("ShootDown");
                buttle.transform.position = myTransform.position + new Vector3(0f, -0.6f, 0);
                buttle.GetComponent<Buttle>().CurrentDirection = -Vector3.up;
                buttle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
            if (stateinfo.IsName("WalkUp") || stateinfo.IsName("IdleUp") || stateinfo.IsName("ShootUp"))
            {
              //  animator.Play("ShootUp");
                buttle.transform.position = myTransform.position + new Vector3(0f, 0.6f, 0);
                buttle.GetComponent<Buttle>().CurrentDirection = Vector3.up;
                buttle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
            }
            
            
        }
    }



    void PlayerMove()
    {


        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (h == 0 && v == 0)
        {
            AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateinfo.IsName("WalkLeft"))
                animator.Play("IdleLeft");
            if (stateinfo.IsName("WalkRight"))
                animator.Play("IdleRight");
            if (stateinfo.IsName("WalkDown"))
                animator.Play("IdleDown");
            if (stateinfo.IsName("WalkUp"))
                animator.Play("IdleUp");
        }

        if ((Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1))
        {

            playerController.MovePosition(new Vector3(myTransform.position.x + h * Time.deltaTime * 4, myTransform.position.y + v * Time.deltaTime * 4));

            if (h < 0 && Input.GetKey(KeyCode.A)) //left
                animator.Play("WalkLeft");
            if (h > 0 && Input.GetKey(KeyCode.D))//right
                animator.Play("WalkRight");
            if (v < 0 && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))//down
                animator.Play("WalkDown");
            if (v > 0 && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))//up
                animator.Play("WalkUp");


            if (PreMove != playerController.position)
                MoveCamera(h, v);

            PreMove = playerController.position;
            //   maincamera.transform.position += new Vector3( h * Time.deltaTime * 8,  v * Time.deltaTime * 8,-10);
        }

    }

    void MoveCamera(float h, float v)
    {
        Vector3 a = maincamera.transform.position;
        Vector3 b = new Vector3(playerController.position.x, playerController.position.y, -10);
        maincamera.transform.position = Vector3.Lerp(a, b, 1.0f);
    }


}
