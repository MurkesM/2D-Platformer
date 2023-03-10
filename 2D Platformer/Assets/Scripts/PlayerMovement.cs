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

    Animator animator;
    const string IDLE_ANIM = "Idle";
    const string WALK_ANIM = "Walk";
    const string RUN_ANIM = "Run";
    const string JUMP_ANIM = "Jump";

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
        //TODO: animations aren't setup correctly. Calling ResetTrigger fixes it but it doesn't seem like the correct route.

        if (Input.GetKey(KeyCode.D)) 
            MoveRight();
        else if (Input.GetKey(KeyCode.A))
            MoveLeft();
        else 
            animator.SetTrigger(IDLE_ANIM);
    }

    void MoveRight()
    {
        renderer.flipX = false; 

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(sprint_speed * Time.deltaTime * Vector2.right);

            animator.SetTrigger(RUN_ANIM);
            animator.ResetTrigger(WALK_ANIM);
        }
            
        else
        {
            transform.Translate(move_speed * Time.deltaTime * Vector2.right);

            animator.SetTrigger(WALK_ANIM);
            animator.ResetTrigger(RUN_ANIM);
        }
    }

    void MoveLeft()
    {
        renderer.flipX = true;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(sprint_speed * Time.deltaTime * Vector2.left);

            animator.SetTrigger(RUN_ANIM);
            animator.ResetTrigger(WALK_ANIM);
        }
            
        else
        {
            transform.Translate(move_speed * Time.deltaTime * Vector2.left);

            animator.SetTrigger(WALK_ANIM);
            animator.ResetTrigger(RUN_ANIM);
        }
            
    }

    void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //jumping isnt working right. jump height doesn't seem to affect anything.
            //probably need to move to rigidi body based jumping and rigidbody based movement
            //need to check if on ground
            transform.Translate(jump_speed * Time.deltaTime * jump_height_vector);

            //TODO: Play jump animation
        }
    }
}