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

    private AiVisual selectedConfig;

    private void Start()
    {
        selectedConfig = visualConfig.aiVisuals.Random();
        Instantiate(selectedConfig.VisualPrefab, ground);
        StartCoroutine(MoveAround());
    }

    private IEnumerator MoveAround()
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
