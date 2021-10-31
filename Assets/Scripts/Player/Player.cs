using System.Collections;
using DG.Tweening;
using UnityEngine;
using Utils.GenericSingletons;

public class Player : MonoBehaviourSingleton<Player>
{
    private bool canMove = true;
    private bool isHiding = false;

    [SerializeField] private float dashForce = 2.0f;
    [SerializeField] private DelayController dashDelayer;

    [SerializeField] private float moveSpeed = 3.0f;
    void Update()
    {
        HandlePlayerMovement();

    }

    private void HandlePlayerMovement()
    {
        if (canMove == false) return;

        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.position += new Vector3(horizontal, vertical, 0);

        dashDelayer.IncrementTimer(out bool isFinishedDelay);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            bool isPressedMoveKey = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
            if (isPressedMoveKey)
            {
                print("Clicked dashed");
                // bool isFinishedDelay = false;
                if (isFinishedDelay)
                {
                    print("Dashed!!");

                    if (vertical < 0)
                    {
                        vertical = -1;
                    }
                    else if (vertical > 0)
                    {
                        vertical = 1;
                    }

                    if (horizontal < 0)
                    {
                        horizontal = -1;
                    }
                    else if (horizontal > 0)
                    {
                        horizontal = 1;
                    }

                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal, vertical) * dashForce;
                    dashDelayer.ResetTimer();
                    StartCoroutine(StopDashEffect(.3f));
                }
            }
        }
    }

    private IEnumerator StopDashEffect(float delay)
    {
        while (GameManager.instance.GameIsOn)
        {
            yield return new WaitForSeconds(delay);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void MoveToNextPlatform()
    {
        DisableMovement();

        float newXPos = MapController.instance.latestSpawnedPlatform.GetPlatform().GetEnterPos().x;
        float duration = Mathf.Abs(newXPos - transform.position.x) / moveSpeed;

        transform
        .DOMoveX(newXPos, duration)
        .SetEase(Ease.InOutSine)
        .OnComplete(EnableMovement);
    }


    private void EnableMovement()
    {
        canMove = true;
    }
    private void DisableMovement()
    {
        canMove = false;
    }


    private void HandleDash()
    {

    }


    public void EnterHideState()
    {
        isHiding = true;
    }

    public void ExitHideState()
    {
        isHiding = false;
    }
}

[System.Serializable]
public class DelayController
{
    private float currentTimer = 0;
    [SerializeField] private float delay = 1.0f;

    public void IncrementTimer(out bool isFinishedDelay)
    {
        isFinishedDelay = false;
        if (currentTimer >= delay)
        {
            isFinishedDelay = true;
        }
        else
        {
            currentTimer += Time.deltaTime;

            if (currentTimer >= delay)
            {
                isFinishedDelay = true;
            }
        }
    }

    public void ResetTimer()
    {
        currentTimer = 0;
    }
}
