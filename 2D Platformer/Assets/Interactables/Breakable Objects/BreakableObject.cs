using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer breakable_roots_renderer;
    [SerializeField] Collider2D breakable_roots_collider;

    const string player_tag = "Player";

    bool is_breakable = true;

    public static List<BreakableObject> breakableObjectsInRange = new();

    public static void BreakObjectsInRange()
    {
        foreach (BreakableObject breakable_object in breakableObjectsInRange)
            breakable_object.Break();

        breakableObjectsInRange.Clear();
    }

    public void Break()
    {
        if (!is_breakable)
            return;

        is_breakable = false;

        breakable_roots_renderer.enabled = false;
        breakable_roots_collider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(player_tag))
            breakableObjectsInRange.Add(this);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(player_tag))
            breakableObjectsInRange.Remove(this);
    }
}