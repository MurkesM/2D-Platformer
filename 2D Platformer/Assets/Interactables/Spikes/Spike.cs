using UnityEngine;

public class Spike : Interactable2D
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            PlayerControls.KillPlayer();
        }
    }
}