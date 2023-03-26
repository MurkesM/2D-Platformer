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
    [SerializeField] protected Transform feet;
    [SerializeField] protected LayerMask floor_layer;
    [SerializeField] protected float jump_force = 50;
    protected bool can_jump = true;
    float overlap_circle_radius = .05f;


    protected SpriteRenderer sprite_renderer;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        //movement
        rb.velocity = move_speed * move_direction;
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
        //Is there a way to do this simpler with a rigidbody setting?
        move_speed = 0;
    }

    protected virtual void Jump()
    {
        if (!CheckJumpState())
            return;

        can_jump = false;

        rb.AddForce(jump_force * Vector2.up);
        
        //TODO: Play jump animation. Do after checking if jumping or else will be hard to test.
        //jump needs to be more smooth using add force.
         

    }

    bool CheckJumpState()
    {
        //maybe switch to OverlapCircleNonAloc? Need to check the tradeoff between memory and cpu most likely.

        if (Physics2D.OverlapCircle(feet.position, overlap_circle_radius, floor_layer)) 
            can_jump = true;

        return can_jump;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawSphere(feet.position, overlap_circle_radius);
    }
}