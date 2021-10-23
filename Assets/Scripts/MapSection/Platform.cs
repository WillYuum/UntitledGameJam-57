using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteComp;


    void Awake()
    {
        spriteComp = gameObject.GetComponent<SpriteRenderer>();
    }


    public void SetPosAfterPrevPlatform(Vector2 prevPlatformPos)
    {
        float newXPos = prevPlatformPos.x - spriteComp.sprite.rect.size.x;
        transform.position = new Vector2(newXPos, prevPlatformPos.y);
    }

    public Vector2 GetPlatformSize() => spriteComp.bounds.size;
}
