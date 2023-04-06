using UnityEngine;

public class PlayerControls : RigidBodyMovement2D
{
    //Animation
    Animator animator;
    const float walk_animation_speed = 1;
    const float run_animation_speed = 2;
    const string anim_speed_param = "Move Speed";
    const string anim_light_attack_state = "Light Attack anim";
    readonly string[] anim_params = { "Idle", "Walk", "Jump", "Light Attack"};

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();

        JumpStateUpdated += SetJumpVisuals;
    }

    void OnDestroy()
    {
        JumpStateUpdated -= SetJumpVisuals;
    }

    protected override void Update()
    {
        base.Update();

        HandleMovement();

        if (Input.GetKeyDown(KeyCode.W))
            Jump();

        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
    }

    protected override void Idle()
    {
        base.Idle();

        AnimationUtil.ToggleAnims(animator, anim_params[0], anim_params);
    }

    void HandleMovement()
    {
        //TODO: move using the newest button pressed as your direction instead of defaulting to right
        //TODO: could do an if holding left (can move right == false and vice versa).

        if (Input.GetKey(KeyCode.D)) 
            MoveRight();
        else if (Input.GetKey(KeyCode.A))
            MoveLeft();
        else if (!is_jumping)
            Idle();
    }

    protected override void MoveRight()
    {
        base.MoveRight();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //sprint
            move_speed = run_speed;

            //play run anim if not currently jumping
            if (!is_jumping)
            {
                AnimationUtil.ToggleAnims(animator, anim_params[1], anim_params);
                animator.SetFloat(anim_speed_param, run_animation_speed);
            }
        }
        else
        {
            //walk
            move_speed = walk_speed;

            //play walk anim if not currently jumping
            if (!is_jumping)
            {
                AnimationUtil.ToggleAnims(animator, anim_params[1], anim_params);
                animator.SetFloat(anim_speed_param, walk_animation_speed);
            }
        }
    }

    protected override void MoveLeft()
    {
        base.MoveLeft();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //sprint
            move_speed = run_speed;

            //play run anim if not currently jumping
            if (!is_jumping)
            {
                AnimationUtil.ToggleAnims(animator, anim_params[1], anim_params);
                animator.SetFloat(anim_speed_param, run_animation_speed);
            }
        }
        else
        {
            //walk
            move_speed = walk_speed;

            //play walk anim if not currently jumping
            if (!is_jumping)
            {
                AnimationUtil.ToggleAnims(animator, anim_params[1], anim_params);
                animator.SetFloat(anim_speed_param, walk_animation_speed);
            }
        }
    }

    void SetJumpVisuals(bool is_jumping)
    {
        if (!is_jumping)
            return;

        AnimationUtil.ToggleAnims(animator, anim_params[2], anim_params);
    }

    void Attack()
    {
        //TODO: currently does nothing but animate
        //TODO: will probably want to create an IDamagable interface for all our breakable objects or use the interactables class
        //TODO: also will want to check if close enough to an object for a "hit" to occur (use overlap circle)

        animator.Play(anim_light_attack_state, 0, 0f);
    }

    public static void KillPlayer()
    {
        print("Kill Player");
    }
}