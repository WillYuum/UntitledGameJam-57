using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chaser
{
    public class FOW : MonoBehaviour
    {
        public float viewRadius;
        [Range(0, 360)]
        public float viewAngle;

        public LayerMask targetMask;
        public LayerMask obstacleMask;

        [HideInInspector]
        public List<Transform> visibleTargets = new List<Transform>();

        public void EnableFOW()
        {
            StartCoroutine(nameof(FindTargetsWithDelay), .2f);
        }

        public void DisableFOW()
        {
            StopCoroutine(nameof(FindTargetsWithDelay));
        }


        IEnumerator FindTargetsWithDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                FindVisibleTargets();
            }
        }

        private void FindVisibleTargets()
        {
            visibleTargets.Clear();
            Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask.value);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector2 dirToTarget = (target.position - transform.position).normalized;
                if (Vector2.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    Debug.Log(" TEST" + target.gameObject.name);
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        visibleTargets.Add(target);
                    }
                }
            }
        }


        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
        }
    }
}
