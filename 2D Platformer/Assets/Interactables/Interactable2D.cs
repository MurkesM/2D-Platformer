using UnityEngine;

public abstract class Interactable2D : MonoBehaviour
{
    public bool is_interactable = true;
    public float timeTillInteractable = 0;
    protected abstract void OnTriggerEnter2D(Collider2D collision);
    protected virtual void OnTriggerExit2D(Collider2D collision) { }
}