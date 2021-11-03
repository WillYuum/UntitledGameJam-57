using System.Collections;
using DG.Tweening;
using UnityEngine;
using Utils.GenericSingletons;

public class Player : MonoBehaviourSingleton<Player>
{
    private bool canMove = true;
    private bool isHiding = false;



    [Header("Control props")]
    [SerializeField] private float dashForce = 2.0f;
    [SerializeField] private DelayController dashDelayer;
    [SerializeField] private float moveSpeed = 3.0f;

    private Rigidbody2D rb;


    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool isPressedMoveKey = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
            if (isPressedMoveKey)
            {
                if (isFinishedDelay)
                {
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

                    rb.velocity = new Vector2(horizontal, vertical) * dashForce;
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
            rb.velocity = Vector2.zero;
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
