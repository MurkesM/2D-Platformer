using UnityEngine;

public abstract class Interactable2D : MonoBehaviour
{
    public const string playerLayer = "Player";

    public bool isInteractable = true;
    public float timeTillInteractable = 0;
    protected abstract void OnTriggerEnter2D(Collider2D collision);
    protected virtual void OnTriggerExit2D(Collider2D collision) { }
}