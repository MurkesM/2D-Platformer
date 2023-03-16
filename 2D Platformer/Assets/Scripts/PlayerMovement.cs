using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walk_speed = 10;
    [SerializeField] float sprint_speed = 15;

    Vector2 move_direction;
    float move_speed;

    [Header("Jump")]
    [SerializeField] float jump_force = 50;

    SpriteRenderer sprite_renderer;
    Rigidbody2D rb;

    [Header("Animation")]
    Animator animator;
    const float walk_animation_speed = 1;
    const float run_animation_speed = 2;
    readonly string[] anim_params = { "Idle", "Walk", "Jump", "Light Attack"};

    void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleAttacking();
    }

    void FixedUpdate()
    {
        rb.velocity = move_speed * move_direction;
    }

    void HandleMovement()
    {
        //TODO: move using the newest button pressed as your direction instead of defaulting to right
        //could do an if holding left (can move right == false and vice versa).

        if (Input.GetKey(KeyCode.D)) 
            MoveRight();
        else if (Input.GetKey(KeyCode.A))
            MoveLeft();
        else
            HandleIdle();
    }

    void MoveRight()
    {
        sprite_renderer.flipX = false;

        move_direction = Vector2.right;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //sprint
            move_speed = sprint_speed;

            ToggleAnims(anim_params[1]);
        }
        else
        {
            //walk
            move_speed = walk_speed;

            ToggleAnims(anim_params[1]);
        }
    }

    void MoveLeft()
    {
        sprite_renderer.flipX = true;

        move_direction = Vector2.left;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //sprint
            move_speed = sprint_speed;

            ToggleAnims(anim_params[1]);
        }
        else
        {
            //walk
            move_speed = walk_speed;

            ToggleAnims(anim_params[1]);
        }
    }

    void HandleIdle()
    {
        move_speed = 0;

        ToggleAnims(anim_params[0]);
    }

    void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(jump_force * Vector2.up);

            //TODO: Play jump animation. Do after checking if jumping or else will be hard to test.
            //need to check if on ground. probably do 2 raycasts
            //jump needs to be more smooth using add force.
        }
    }

    void HandleAttacking()
    {
        //currently does nothing but animate
        //will probably want to create an IDamagable interface for all our breakable objects

        if (Input.GetKeyDown(KeyCode.Space))
            ToggleAnims(anim_params[3]);
    }


    //would be good to moves this to an anim util class
    void ToggleAnims(string anim_on)
    {
        //turn on passed in animation
        animator.SetBool(anim_on, true);

        //turn off all anim params except the one passed in
        foreach (string anim_param in anim_params)
        {
            if (anim_param != anim_on)
                animator.SetBool(anim_param, false);
        }
    }
}