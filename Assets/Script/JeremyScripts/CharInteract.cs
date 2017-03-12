using UnityEngine;
using System.Collections;
public class CharInteract : MonoBehaviour {
    [SerializeField]private float sensitivity = 0.2f;
    private enum state{STANDING, MOVING, SLIDING, JUMPING, FALLING, SHOOTING, PRAYING}
    private state _state;
    public bool jump = false;
    public bool facing = true;
    public float move_force = 365f;
    public float max_speed = 5f;
    public float jump_force = 1000f;
    public float power = 120;
    public bool grounded = false;
    public Transform groundCheck;
    public Rigidbody2D rigidbody2d;
    private float horizontal,arrow_horizontal,arrow_vertical,angle,x,y;
    private GameObject arrow_gameobject;
    private ArrowInteractions arrow_interactions;
    private bool can_pray, check;
    private Vector2 arrow_out;
    private Vector3[] trailrederer_helper;
    void Start()
    {
        can_pray = true;
        check = false;
        rigidbody2d = GetComponent<Rigidbody2D>();
        arrow_gameobject = GameObject.Find("arrow");
        if (arrow_gameobject == null)
        {
            Debug.Log("No arrow found!");
        }
        arrow_interactions = arrow_gameobject.GetComponent<ArrowInteractions>();
        trailrederer_helper = new Vector3[20]; 
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Ground")
        {
            grounded = true;
        }
    }
    void Update()
    {
        switch (_state)
        {
            case state.STANDING:
                //animation code
                OnGround();
                Jump();
                FirePray();
                break;
            case state.MOVING:
                //animation code
                OnGround();
                Jump();
                break;
            case state.SLIDING:
                //animation code
                OnGround();
                Jump();
                break;
            case state.JUMPING:
                //animation code
            case state.FALLING:
                //animation code
                Falling();
                break;
            case state.SHOOTING:
                Firing();
                break;
            case state.PRAYING:
                Prayer();
                break;
        }
    }
    void FixedUpdate()
    {
        if (Mathf.Abs(horizontal * rigidbody2d.velocity.x) < max_speed)
        {
            rigidbody2d.AddForce(Vector2.right * horizontal * move_force);
        }
        if (Mathf.Abs(rigidbody2d.velocity.x) > max_speed)
        {
            rigidbody2d.velocity = new Vector2(Mathf.Sign(rigidbody2d.velocity.x) * max_speed, rigidbody2d.velocity.y);
        }
        if ((horizontal > 0 && !facing) || (horizontal > 0 && facing))
        {
            Flip();
        }
        if (jump)
        {
            rigidbody2d.AddForce(new Vector2(0f, jump_force));
            jump = false;
        }
    }
    void OnGround()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal >= sensitivity)
        {
            _state = state.MOVING;
        }
        else if (horizontal <= -sensitivity)
        {
            _state = state.MOVING;
        }
        else if (rigidbody2d.velocity.x != 0)
        {
            _state = state.SLIDING;
        }
        else
        {
            _state = state.STANDING;
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                jump = true;
                grounded = false;
                _state = state.JUMPING;
            }
           
        }
    }
    void Falling()
    {
        if (grounded)
        {
            _state = state.STANDING;
        }
        else if (rigidbody2d.velocity.y < 0)
        {
            _state = state.FALLING;
        }
    }
    void Flip()
    {
        facing = !facing;
    }
    void Firing()
    {

        if (Input.GetButtonDown("FireArrow"))
        {

            _state = state.STANDING;
        }
    }
    void Prayer()
    {
        if (can_pray == true)
        {
            //animation code
            arrow_interactions.Summon(transform);
            can_pray = false;
            _state = state.STANDING;
        }
    }
    void FirePray()
    {
        if (Input.GetButtonDown("Grab"))
        {
            //arrow_interactions.Grab(transform);
        }
        else if (Input.GetButtonDown("FireArrow"))
        {
            Debug.Log("FirePlay");
            _state = state.SHOOTING;
        }
        else if (Input.GetButtonDown("Pray"))
        {
            _state = state.PRAYING;
        }
    }
}