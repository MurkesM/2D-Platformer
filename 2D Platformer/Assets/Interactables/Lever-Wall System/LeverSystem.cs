using UnityEngine;

public class LeverSystem : Interactable2D
{
    [Header("Lever")]
    [SerializeField] SpriteRenderer lever_renderer;
    [SerializeField] Sprite lever_interacted_sprite;

    [Header("Wall")]
    [SerializeField] Animator wall_animator;
    const string move_param = "Move";

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (!lever_interacted_sprite && !wall_animator)
                return;

            lever_renderer.sprite = lever_interacted_sprite;

            wall_animator.SetBool(move_param, true);

            isInteractable = false;
        }
    }
}