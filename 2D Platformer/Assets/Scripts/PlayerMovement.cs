using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float move_speed = 10;
    [SerializeField] float sprint_speed = 15;

    [Header("Jump")]
    [SerializeField] float jump_force = 1000;

    SpriteRenderer renderer;

    //Animation
    Animator animator;
    const float animator_default_speed = 1;
    const float animator_increased_speed = 2;
    readonly string[] anim_params = { "Idle", "Walk", "Jump", "Light Attack"};

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleAttacking();
    }

    void HandleMovement()
    {
        //TODO: move using the newest button pressed as your direction instead of defaulting to right
        //could do an if holding left (can move right == false and vice versa).

        //move movement to physics based. Will feel smoother, can still get tight controls and wont 
        //"glitch" out when running into walls.

        if (Input.GetKey(KeyCode.D)) 
            MoveRight();
        else if (Input.GetKey(KeyCode.A))
            MoveLeft();
        else
            ToggleAnims(anim_params[0]);
    }

    void MoveRight()
    {
        renderer.flipX = false; 

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //sprint
            transform.Translate(sprint_speed * Time.deltaTime * Vector2.right);

            ToggleAnims(anim_params[1], animator_increased_speed);
        }
        else
        {
            //walk
            transform.Translate(move_speed * Time.deltaTime * Vector2.right);

            ToggleAnims(anim_params[1]);
        }
    }

    void MoveLeft()
    {
        renderer.flipX = true;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //sprint
            transform.Translate(sprint_speed * Time.deltaTime * Vector2.left);

            ToggleAnims(anim_params[1], animator_increased_speed);
        }
        else
        {
            //walk
            transform.Translate(move_speed * Time.deltaTime * Vector2.left);

            ToggleAnims(anim_params[1]);
        }
    }

    void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //jumping isnt working right. jump height doesn't seem to affect anything.
            //probably need to move to rigidi body based jumping and rigidbody based movement
            //need to check if on ground. probably do 2 raycasts
            transform.Translate(jump_force * Time.deltaTime * Vector2.up);

            //TODO: Play jump animation. Do after checking if jumping or else will be hard to test.
        }
    }

    void HandleAttacking()
    {
        //currently does nothing but animate
        //will probably want to create an IDamagle interface for all our breakable objects

        if (Input.GetKeyDown(KeyCode.Space))
            ToggleAnims(anim_params[3]);
    }

    void ToggleAnims(string anim_on, float animator_speed = animator_default_speed)
    {
        //used to move player walk anim faster so the transition from walk to run is smoother using the same step in the sprite sheet
        animator.speed = animator_speed;

        //trigger on passed in animation
        animator.SetTrigger(anim_on);

        //turn off all anim params except the one passed in
        foreach (string anim_param in anim_params)
        {
            if (anim_param != anim_on)
                animator.ResetTrigger(anim_param);
        }
    }
}