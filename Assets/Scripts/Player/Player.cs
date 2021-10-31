using DG.Tweening;
using UnityEngine;
using Utils.GenericSingletons;

public class Player : MonoBehaviourSingleton<Player>
{
    private bool canMove = true;
    private bool isHiding = false;

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
