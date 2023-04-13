using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer breakable_roots_renderer;
    [SerializeField] Collider2D breakable_roots_collider;

    bool is_breakable = true;

    public void Break()
    {
        if (!is_breakable)
            return;

        is_breakable = false;

        breakable_roots_renderer.enabled = false;
        breakable_roots_collider.enabled = false;
    }
}