using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Pedestrian : MonoBehaviour
{
    public NavMeshAgent agent;
    public float wanderMinRadius = 2;
    public float wanderMaxRadius = 5;
    public float minimumMovementTimeout = 2f;
    public float maximumMovementTimeout = 5f;
    public AiVisualConfig visualConfig;
    public Transform ground;

    protected GameObject visual;
    protected float velocityThreshold = 0.25f;
    protected AiVisual selectedConfig;
    protected Animator animator;

    private void Start()
    {
        selectedConfig = visualConfig.aiVisuals.Random();
        visual = Instantiate(selectedConfig.VisualPrefab, ground);
        animator = visual.GetComponentInChildren<Animator>(true);
        StartCoroutine(MoveAround());
    }

    private void Update()
    {
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            var animationChanged = HandleAnimation(animator);
            if (animationChanged)
            {
                // Some animations move the transform, causing game objects to drift or sink.
                visual.transform.localPosition = Vector3.zero;
            }
        }
    }

    protected virtual bool HandleAnimation(Animator animator)
    {
        if (agent.velocity.magnitude < velocityThreshold)
        {
            return animator.SetTriggerIfNotInState(selectedConfig.idleTrigger, selectedConfig.idleState);
        }
        else
        {
            return animator.SetTriggerIfNotInState(selectedConfig.walkTrigger, selectedConfig.walkState);
        }
    }

    protected IEnumerator MoveAround()
    {
        while (true)
        {
            var destination = GetDestination();
            agent.SetDestination(destination);
            yield return new WaitForSeconds(GetMovementTimeout());
        }
    }

    protected virtual float GetMovementTimeout()
    {
        return UnityEngine.Random.Range(minimumMovementTimeout, maximumMovementTimeout);
    }

    protected virtual Vector3 GetDestination()
    {
        Quaternion quaternion = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
        Vector3 offset = quaternion * transform.forward * UnityEngine.Random.Range(wanderMinRadius, wanderMaxRadius);
        return transform.position + offset;
    }
}
