using UnityEngine;

public abstract class Interactable2D : MonoBehaviour
{
    public const string playerTag = "Player";

    public bool isInteractable = true;
    public float timeTillInteractable = 0;
    protected abstract void OnTriggerEnter2D(Collider2D other);
}