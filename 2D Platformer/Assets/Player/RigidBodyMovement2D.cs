using System;
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
    [SerializeField] protected float jump_force = 50;
    [SerializeField] protected Transform feet;
    [SerializeField] protected LayerMask floor_layer;
    protected bool can_jump = true;
    protected bool is_jumping = false;
    float overlap_circle_radius = .05f;

    public Action<bool> JumpStateUpdated;

    protected SpriteRenderer sprite_renderer;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        CheckJumpState();
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
        //TODO: Is there a way to do this simpler with a rigidbody setting?
        move_speed = 0;
    }

    protected virtual void Jump()
    {
        //return if false
        if (!can_jump)
            return;

        rb.AddForce(jump_force * Vector2.up);
    }

    /// <summary>
    /// Returns true if the user can jump.
    /// </summary>
    /// <returns></returns>
    protected bool CheckJumpState()
    {
        //TODO: Find a way to not call this every frame or find a different way to check jump state if you have to keep in update loop.
        //TODO: maybe see how useing a collider would work. 
        //TODO: maybe switch to OverlapCircleNonAloc? Need to check the tradeoff between memory and cpu most likely.

        if (Physics2D.OverlapCircle(feet.position, overlap_circle_radius, floor_layer))
            SetJumpState(true);
        else
            SetJumpState(false);

        return can_jump;
    }

    /// <summary>
    /// Set's whether the user can jump and whether the user is currently jumping.
    /// </summary>
    /// <param name="can_jump"></param>
    protected void SetJumpState(bool can_jump)
    {
        this.can_jump = can_jump;
        is_jumping = !can_jump;

        //send event
        JumpStateUpdated?.Invoke(is_jumping);
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawSphere(feet.position, overlap_circle_radius);
    }
#endif
}