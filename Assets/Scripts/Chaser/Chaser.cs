using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chaser
{
    public class Chaser : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 7.5f;

        [SerializeField] private FOW fow;

        void Awake()
        {
            // fow.EnableFOW();
        }
    }
}
