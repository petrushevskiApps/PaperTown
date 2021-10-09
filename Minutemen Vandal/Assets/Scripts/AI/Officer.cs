using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Officer : Pedestrian
{
    [SerializeField]
    private Transform player;

    public void ChasePlayer(Transform player)
    {
        this.player = player;
        visual.transform.LookAt(player);
        animator.SetTriggerIfNotInState(selectedConfig.runTrigger, selectedConfig.runState);
        StopAllCoroutines();
        agent.SetDestination(player.position);
    }

    public void StopChasingPlayer()
    {
        player = null;
        agent.SetDestination(transform.position);
        visual.transform.localRotation = Quaternion.identity;
        animator.SetTriggerIfNotInState(selectedConfig.idleTrigger, selectedConfig.idleState);
        StartCoroutine(MoveAround());
    }

    protected override float GetMovementTimeout()
    {
        if (player == null)
        {
            return base.GetMovementTimeout();
        }
        else
        {
            return 0;
        }
    }

    protected override Vector3 GetDestination()
    {
        if (player == null)
        {
            return base.GetDestination();
        }
        else
        {
            return player.position;
        }
    }

    protected override bool HandleAnimation(Animator animator)
    {
        if (agent.velocity.magnitude > velocityThreshold)
        {
            if (player == null)
            {
                visual.transform.localRotation = Quaternion.identity;
                return animator.SetTriggerIfNotInState(selectedConfig.walkTrigger, selectedConfig.walkState);
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName(selectedConfig.walkState))
                {
                    return false;
                }
                visual.transform.LookAt(player);
                return animator.SetTriggerIfNotInState(selectedConfig.runTrigger, selectedConfig.runState);
            }
        }
        else
        {
            visual.transform.localRotation = Quaternion.identity;
            return animator.SetTriggerIfNotInState(selectedConfig.idleTrigger, selectedConfig.idleState);
        }
    }
}
