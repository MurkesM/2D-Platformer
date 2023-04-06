using UnityEngine;

public class Lava : Interactable2D
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            PlayerControls.KillPlayer();
        }
    }
}