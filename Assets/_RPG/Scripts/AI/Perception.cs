using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour
{
    public float proximityRadius = 3f;
    public float sightRadius = 10f;
    public float FieldOfView = 45f;
    public Transform eye;
    public Vector3 lastSighting;
    public bool isTargetInSightLine;
    public Transform target;

    private void Start()
    {
        if (!target)
        {
            target = PlayerManager.instance.player;
        }

        if (!eye)
        {
            eye = transform;
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < proximityRadius)
        {
            lastSighting = target.position;
            isTargetInSightLine = true;

        } else if (distance < sightRadius)
        {
            UpdateSight(target);
        } else
        {
            isTargetInSightLine = false;
        }
    }

    private void UpdateSight(Transform target)
    {
        if (HasClearSightLineToTarget(target) && TargetInFOV(target))
        {
            isTargetInSightLine = true;
            lastSighting = target.position;
        } else
        {
            isTargetInSightLine = false;
        }
    }

    private bool HasClearSightLineToTarget(Transform target)
    {
        RaycastHit Info;
        Vector3 DirToTarget = (target.position - eye.position).normalized;

        if (Physics.Raycast(eye.position, DirToTarget, out Info, sightRadius))
        {
            if (Info.transform.CompareTag(target.tag))
            {
                return true;
            }
        }

        return false;
    }

    private bool TargetInFOV(Transform target)
    {
        Vector3 dirToTarget = target.position - eye.position;
        float angle = Vector3.Angle(eye.forward, dirToTarget);

        return (angle <= FieldOfView);
    }


    public static Quaternion FaceTarget(Transform thisTransform, Vector3 targetPosition, bool slerp=true)
    {
        Vector3 direction = (targetPosition - thisTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        if (slerp)
        {
            return Quaternion.Slerp(thisTransform.rotation, lookRotation, Time.deltaTime * 5f);
        } else
        {
            return lookRotation;
        }
    }

    public static Quaternion EscapeTarget(Transform thisTransform, Vector3 targetPosition, bool slerp=true)
    {
        Vector3 direction = -1*(targetPosition - thisTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        
        if (slerp)
        {
            return Quaternion.Slerp(thisTransform.rotation, lookRotation, Time.deltaTime * 5f);
        } else
        {
            return lookRotation;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, proximityRadius);
        Gizmos.color = Color.blue;
        Gizmos.matrix = Matrix4x4.TRS(eye.position, transform.rotation, Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, FieldOfView, sightRadius, 0, 1);
    }
}
