using UnityEngine;

public static class AnimatorUtils
{
    public static bool SetTriggerIfNotInState(this Animator animator, string trigger, string state)
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!animatorStateInfo.IsName(state))
        {
            animator.SetTrigger(trigger);
            return true;
        }
        return false;
    }
}
