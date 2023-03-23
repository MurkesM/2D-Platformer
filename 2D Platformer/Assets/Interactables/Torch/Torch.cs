using System.Collections;
using UnityEngine;

public class Torch : Interactable
{
    const string anim_param = "Glow";
    [SerializeField] Animator animator;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (animator != null && is_interactable)
                StartCoroutine(GlowRoutine());
        }
    }

    IEnumerator GlowRoutine()
    {
        //play anim for set amount of time and remove the ability to trigger the anim again
        animator.SetBool(anim_param, true);
        is_interactable = false;
        yield return new WaitForSeconds(3);

        //stop anim and and coroutine
        animator.SetBool(anim_param, false);
        StopCoroutine(GlowRoutine());
    }
}