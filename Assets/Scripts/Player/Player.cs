using UnityEngine;
using Utils.GenericSingletons;

public class Player : MonoBehaviourSingleton<Player>
{

    [SerializeField] private float moveSpeed = 3.0f;
    void Update()
    {
        HandlePlayerMovement();
    }



    private void HandlePlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.position += new Vector3(horizontal, vertical, 0);
    }
}
