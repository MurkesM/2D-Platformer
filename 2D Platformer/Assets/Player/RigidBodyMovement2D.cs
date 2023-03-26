using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class RigidBodyMovement2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float walk_speed = 10;
    [SerializeField] protected float run_speed = 15;

    protected Vector2 move_direction;
    protected float move_speed;

    [Header("Jump")]
    [SerializeField] protected LayerMask floor_layer;
    [SerializeField] protected float jump_force = 50;
    protected bool can_jump = true;

    protected SpriteRenderer sprite_renderer;
    
    protected Rigidbody2D rb;

    [SerializeField] protected bool debug;

    protected virtual void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        //movement
        rb.velocity = move_speed * move_direction;

        can_jump = CheckJumpState();
    }

    protected virtual void MoveRight()
    {
        sprite_renderer.flipX = false;

        move_direction = Vector2.right;
    }

    protected virtual void MoveLeft()
    {
        sprite_renderer.flipX = true;

        move_direction = Vector2.left;
    }

    protected virtual void Idle()
    {
        //set move speed to 0 so that the user stop's immediatly when not moving instead of sliding
        move_speed = 0;
    }

    protected virtual void Jump()
    {
        if (!can_jump)
            return;

        can_jump = false;

        rb.AddForce(jump_force * Vector2.up);
        
        //TODO: Play jump animation. Do after checking if jumping or else will be hard to test.
        //need to check if on ground. probably do 2 raycasts
        //jump needs to be more smooth using add force.
    }

    bool CheckJumpState()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .175f, floor_layer);

        if (hit)
            can_jump = true;

#if UNITY_EDITOR
        OnDrawGizmos();
#endif

        return can_jump;
    }

    void OnDrawGizmos()
    {
        if (!debug)
            return;

        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector2.down) * .175f;
        Gizmos.DrawRay(transform.position, direction);
    }
}