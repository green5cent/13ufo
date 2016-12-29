using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour
{
    public enum AIState
    {
        Idle = 0,
        Patrol,
        Pursuit,
        Attack,
        Escape
    }

    PlayerController playerController;

    Vector3 prePosition;

    AIState state;
    Transform player;
    Transform myTransform;
    Animator animator;
    float viewDistance = 5f;
    float arrivalDistance = 0.7f;
    float escapeDistance = 10;
    float healthCount = 100;
    public float health = 100;  //血

    float maxSpeed = 5;
 //   Vector3 velocity = Vector3.zero;
    Vector3 acceleration = Vector3.zero;

    Vector3 desiredVelocity;
    float wanderRadius = 5;
    float wanderDistance = 1;
    float wanderJitter = 1;
    Vector3 wanderTarget;
    Vector3 wanderTargetLocal = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        state = AIState.Patrol;
        player = GameObject.Find("Player").GetComponent<Transform>();
        animator = transform.GetComponent<Animator>();
        wanderTarget = new Vector3(wanderRadius * 0.707f, 0, wanderRadius * 0.707f);
        myTransform = this.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);
        //if(myTransform.childCount>0)
        //{
        //    for(int i=0;i<myTransform.childCount;i++)
        //    {
                
        //        Animator ani = myTransform.GetChild(i).GetComponent<Animator>();
        //        AnimatorStateInfo stateinfo = ani.GetCurrentAnimatorStateInfo(0);
        //        Debug.Log(stateinfo.IsName("Hunt"));
        //        if (stateinfo.IsName("Hunt"))
        //        {
        //            Destroy(ani.gameObject);
                    
        //        }

        //    }
        //}
        myTransform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        switch (state)
        {
            case AIState.Idle:
                OnIdle();
                break;
            case AIState.Patrol:
                OnPartrol();
                break;
            case AIState.Pursuit:
                OnPursuit();
                break;
            case AIState.Attack:
                OnAttack();
                break;
            case AIState.Escape:
                OnEscape();
                break;
        }
    }

    void OnIdle()
    {
        //if saw player
        if (Vector3.Distance(player.position, myTransform.position) <= viewDistance)
            state = AIState.Attack;

        //if health  < 10%
        if (health <= healthCount * 0.1f)
            state = AIState.Escape;

    }

    void OnPartrol()
    {
        //if saw player
        if (Vector3.Distance(player.position, myTransform.position) <= viewDistance)
        {
            state = AIState.Pursuit;
        }

        //if health  < 10%
        if (health <= healthCount * 0.1f)
        {
            state = AIState.Escape;
        }
     
        Move(Wander());
    }


    void Move(Vector3 a)
    {
        a = new Vector3(a.x, a.y, 0);

        acceleration = a*0.003f;
     //   velocity += acceleration;

        if (a.y <= 0.5f && a.y >= -0.5f && a.x > 0)
            animator.Play("MSWalkRight");
        if (a.y <= 0.5f && a.y >= -0.5f && a.x < 0)
            animator.Play("MSWalkLeft");
        if (a.x <= 0.5 && a.x >= -0.5f && a.y > 0)
            animator.Play("MSWalkUp");
        if (a.x <= 0.5 && a.x >= -0.5f && a.y < 0)
            animator.Play("MSWalkDown");

      //  myTransform.position += velocity * Time.deltaTime;
        myTransform.position += a * Time.deltaTime*0.5f;
    }

   

    Vector3 Wander()
    {
        wanderTarget += new Vector3(Random.Range(-1f, 1f) * wanderJitter, Random.Range(-1f, 1f) * wanderJitter, 0);

        wanderTarget = wanderRadius * wanderTarget.normalized;

       // wanderTargetLocal = velocity.normalized * wanderDistance + wanderTarget + myTransform.position;

        desiredVelocity = (wanderTargetLocal - myTransform.position).normalized * maxSpeed;
       return (desiredVelocity );


    }

    void OnPursuit()
    {
        //Vector3 toTarget = player.position - transform.position;
        //float relativeDirection = Vector3.Dot(transform.forward, player.forward);

        //if ((Vector3.Dot(toTarget, transform.forward) > 0) && (relativeDirection < -0.95f))
        //{
        //    desiredVelocity = (player.position - transform.position).normalized * maxSpeed;
        //    return (desiredVelocity - velocity);
        //}

        //float lookaheadTime = toTarget.magnitude / (maxSpeed + player.GetComponent<Rigidbody2D>().velocity.magnitude);

        //desiredVelocity = (player.position + player.GetComponent<Rigidbody2D>().velocity * lookaheadTime
        //    - transform.position).normalized * maxSpeed;

        //return (desiredVelocity - velocity);
        //Vector2 Target = player.position - transform.position;


        //抵达目标位置
        if (Vector3.Distance(player.position, myTransform.position) <= arrivalDistance)
        {
            state = AIState.Attack;
        }

        //if not saw player
        if (Vector3.Distance(player.position, myTransform.position) >= viewDistance)
        {
            state = AIState.Patrol;
        }

        desiredVelocity = player.position - myTransform.position;
      //  velocity = Vector3.zero;
        Move( desiredVelocity );
    }

    Vector3 Evade()
    {
       
        Vector3 toTarget = player.position - transform.position;

        float lookaheadTime = toTarget.magnitude / (maxSpeed + player.GetComponent<CharacterController>().velocity.magnitude);

        desiredVelocity = (transform.position - (player.position + player.GetComponent<CharacterController>().velocity * lookaheadTime)).normalized * maxSpeed;
        
        return (desiredVelocity );

    }

    void OnAttack()
    {
        AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateinfo.IsName("MSWalkLeft"))
            animator.Play("MSAttackLeft");
        if (stateinfo.IsName("MSWalkRight"))
            animator.Play("MSAttackRight");
        if (stateinfo.IsName("MSWalkDown"))
            animator.Play("MSAttackDown");
        if (stateinfo.IsName("MSWalkUp"))
            animator.Play("MSAttackUp");

        player.GetComponent<PlayerController>().health -= 1;
        PlayerInfo.Instance.ReduceHp(10);
        //if(playerController.transform.childCount(0)!=null)
        //    playerController.transform.GetChild(0).
        //GameObject obj = Instantiate(Resources.Load<GameObject>("Hunt"));
        //obj.transform.SetParent(playerController.transform);
        //obj.transform.position = playerController.transform.position;


        if (Vector3.Distance(player.position, myTransform.position)>= arrivalDistance*1.2f )
            state = AIState.Pursuit;

        //if not saw player
        if (Vector3.Distance(player.position, myTransform.position) >= viewDistance)
        {
            state = AIState.Patrol;
        }
        //if health  < 10%
        if (health <= healthCount * 0.1f)
            state = AIState.Escape;
    }

    void OnEscape()
    {
        Move(Evade());

        //离开player一定范围
        if (Vector3.Distance(player.position, myTransform.position) <= escapeDistance)
        {
            state = AIState.Patrol;
        }
    }

    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("1111111111");
    //}
}
