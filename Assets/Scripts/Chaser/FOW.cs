using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chaser
{
    public class FOW : MonoBehaviour
    {
        [SerializeField] public float viewRadius;
        [Range(0, 360)]
        [SerializeField] public float viewAngle;

        [SerializeField] private FowVisual fowVision;
        public event Action onCaughtTarget;


        [Header("Filters")]
        public LayerMask targetMask;
        public LayerMask obstacleMask;



        [HideInInspector]
        public List<Transform> visibleTargets = new List<Transform>();

        public void EnableFOW()
        {
            gameObject.SetActive(true);
            StartCoroutine(nameof(FindTargetsWithDelay), .2f);
        }

        public void DisableFOW()
        {
            gameObject.SetActive(false);
            StopCoroutine(nameof(FindTargetsWithDelay));
        }

        void Update()
        {
            fowVision.SetOrigin(transform.position);
            fowVision.SetAimDirection(transform.parent.up);
        }


        IEnumerator FindTargetsWithDelay(float delay)
        {
            while (GameManager.instance.GameIsOn)
            {
                yield return new WaitForSeconds(delay);
                FindVisibleTargets();
            }
        }

        private void FindVisibleTargets()
        {
            visibleTargets.Clear();
            Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask.value);

            bool caughtTarget = false;

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector2 dirToTarget = (target.position - transform.position).normalized;
                if (Vector2.Angle(transform.up, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector2.Distance(transform.position, target.position);

                    if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        visibleTargets.Add(target);
                        caughtTarget = true;
                        break;
                    }
                }
            }

            if (caughtTarget)
            {
                onCaughtTarget?.Invoke();
            }
        }


        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal = false)
        {
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
        }
    }
}
