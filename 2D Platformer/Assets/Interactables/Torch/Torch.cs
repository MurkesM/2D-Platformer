using System.Collections;
using UnityEngine;

public class Torch : Interactable2D
{
    const string anim_param = "Glow";
    [SerializeField] Animator animator;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (animator != null && isInteractable)
                StartCoroutine(GlowRoutine());
        }
    }

    IEnumerator GlowRoutine()
    {
        //play anim for set amount of time and remove the ability to trigger the anim again
        animator.SetBool(anim_param, true);
        isInteractable = false;
        yield return new WaitForSeconds(3);

        //stop anim and and coroutine
        animator.SetBool(anim_param, false);
        StopCoroutine(GlowRoutine());
    }
}
