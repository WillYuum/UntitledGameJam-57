using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chaser
{
    public class Chaser : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 7.5f;

        [SerializeField] private FOW fow;

        private bool isTryingToCatchPlayer = false;

        void Awake()
        {
            fow.EnableFOW();
            fow.onCaughtTarget += InvokeSawPlayer;
        }

        void Update()
        {
            if (GameManager.instance.GameIsOn == false) return;

            if (isTryingToCatchPlayer)
            {
                HandleCatchPlayer();
            }
        }


        private void InvokeSawPlayer()
        {
            fow.onCaughtTarget -= InvokeSawPlayer;
            isTryingToCatchPlayer = true;
        }

        private void HandleCatchPlayer()
        {
            Transform playerTransform = Player.instance.transform;
            // transform.LookAt(playerTransform, Vector2.up);
            transform.up = playerTransform.position - transform.position;

            if (Vector2.Distance(transform.position, playerTransform.position) < 1f)
            {
                GameManager.instance.LoseGame();
            }
            else
            {
                transform.position = Vector2.Lerp(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            }
        }



    }
}
