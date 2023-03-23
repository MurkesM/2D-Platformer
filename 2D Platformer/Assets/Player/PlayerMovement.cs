using UnityEngine;

public class PlayerMovement : RigidBodyMovement2D
{
    [Header("Animation")]
    Animator animator;
    const float walk_animation_speed = 1;
    const float run_animation_speed = 2;
    const string anim_speed_param = "Move Speed";
    readonly string[] anim_params = { "Idle", "Walk", "Jump", "Light Attack"};

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.W))
            Jump();

        HandleAttacking();
    }

    protected override void HandleMovement()
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

    protected override void MoveRight()
    {
        base.MoveRight();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //sprint
            move_speed = run_speed;

            ToggleAnims(anim_params[1]);

            animator.SetFloat(anim_speed_param, run_animation_speed);
        }
        else
        {
            //walk
            move_speed = walk_speed;

            ToggleAnims(anim_params[1]);

            animator.SetFloat(anim_speed_param, walk_animation_speed);
        }
    }

    protected override void MoveLeft()
    {
        base.MoveLeft();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //sprint
            move_speed = run_speed;

            ToggleAnims(anim_params[1]);

            animator.SetFloat(anim_speed_param, run_animation_speed);
        }
        else
        {
            //walk
            move_speed = walk_speed;

            ToggleAnims(anim_params[1]);

            animator.SetFloat(anim_speed_param, walk_animation_speed);
        }
    }

    protected override void HandleIdle()
    {
        base.HandleIdle();

        ToggleAnims(anim_params[0]);
    }

    void HandleAttacking()
    {
        //currently does nothing but animate
        //will probably want to create an IDamagable interface for all our breakable objects
        //also will want to check if close enough to an object for a "hit" to occur

        if (Input.GetKeyDown(KeyCode.Space))
            ToggleAnims(anim_params[3]);
    }


    //might be good to move this to an anim util class
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