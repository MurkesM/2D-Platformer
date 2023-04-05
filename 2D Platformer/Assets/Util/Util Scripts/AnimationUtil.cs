using UnityEngine;

public static class AnimationUtil
{
    /// <summary>
    /// Sets the specified animator paramater to true and set's the specified list of params to false.
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="anim_to_turn_on"></param>
    /// <param name="anims_to_turn_off"></param>
    static public void ToggleAnims(Animator animator, string anim_to_turn_on, string[] anims_to_turn_off)
    {
        animator.SetBool(anim_to_turn_on, true);

        foreach (string anim_param in anims_to_turn_off)
        {
            if (anim_param != anim_to_turn_on)
                animator.SetBool(anim_param, false);
        }
    }
}
