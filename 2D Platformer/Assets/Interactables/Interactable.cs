using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool is_interactable = true;
    public float timeTillInteractable = 0;
    public abstract void OnTriggerEnter2D(Collider2D collision);
    public virtual void OnTriggerExit2D(Collider2D collision) { }
}