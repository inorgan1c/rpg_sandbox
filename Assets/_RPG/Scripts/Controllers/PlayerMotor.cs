using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target = null;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
        agent.isStopped = false;
    }

    public void FollowTarget(Interactable newTarget)
    {
        target = newTarget.interactionTransform;
        agent.stoppingDistance = newTarget.radius * 0.8f;
        agent.updateRotation = false;
    }

    public void StopFollowingTarget()
    {
        target = null;
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
