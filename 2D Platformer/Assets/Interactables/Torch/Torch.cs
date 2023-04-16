using System;
using System.Collections;
using UnityEngine;

public class Torch : Interactable2D
{
    const string anim_param = "Glow";
    [SerializeField] Animator animator;

    bool is_glowing = false;
    public bool IsGlowing { get { return is_glowing; } }

    public Action<bool> GlowStateUpdated;

    Coroutine glow_Routine = null;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (animator != null && isInteractable)
                glow_Routine = StartCoroutine(GlowRoutine());
        }
    }

    IEnumerator GlowRoutine()
    {
        //play anim for set amount of time and remove the ability to trigger the anim again
        animator.SetBool(anim_param, true);
        isInteractable = false;

        //set state and sent events
        is_glowing = true;
        GlowStateUpdated(is_glowing);

        yield return new WaitForSeconds(3);

        //stop anim
        animator.SetBool(anim_param, false);

        //set state and sent events
        is_glowing = false;
        GlowStateUpdated(is_glowing);

        //stop coroutine
        StopCoroutine(glow_Routine);
    }
}