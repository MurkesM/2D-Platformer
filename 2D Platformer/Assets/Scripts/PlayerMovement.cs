using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float move_speed = 10;
    [SerializeField] float sprint_speed = 15;

    [Header("Jump")]
    [SerializeField] float jump_speed = 1000;
    [SerializeField] float jump_height = 2;
    Vector3 jump_height_vector;

    SpriteRenderer renderer;

    //Animation
    Animator animator;
    const float animator_default_speed = 1;
    const float animator_increased_speed = 2;
    readonly string[] anim_params = { "Idle", "Walk", "Jump" };

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        jump_height_vector = new Vector3(0, jump_height, 0);
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
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
            ToggleAnims(anim_params[0]);
    }

    void MoveRight()
    {
        renderer.flipX = false; 

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //sprint
            transform.Translate(sprint_speed * Time.deltaTime * Vector2.right);

            animator.speed = animator_increased_speed;
            ToggleAnims(anim_params[1]);
        }
        else
        {
            //walk
            transform.Translate(move_speed * Time.deltaTime * Vector2.right);

            animator.speed = animator_default_speed;
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

            animator.speed = animator_increased_speed;
            ToggleAnims(anim_params[1]);
        }
        else
        {
            //walk
            transform.Translate(move_speed * Time.deltaTime * Vector2.left);

            animator.speed = animator_default_speed;
            ToggleAnims(anim_params[1]);
        }
            
    }

    void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //jumping isnt working right. jump height doesn't seem to affect anything.
            //probably need to move to rigidi body based jumping and rigidbody based movement
            //need to check if on ground. probably do 2 raycasts
            transform.Translate(jump_speed * Time.deltaTime * jump_height_vector);

            //TODO: Play jump animation. Do after checking if jumping or else will be hard to test.
        }
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