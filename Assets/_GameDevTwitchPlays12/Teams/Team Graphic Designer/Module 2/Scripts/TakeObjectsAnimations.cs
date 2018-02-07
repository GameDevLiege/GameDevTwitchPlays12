using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeObjectsAnimations : MonoBehaviour
{
    float moveSpeed = 0f;

    void Update()
    {
        transform.position = new Vector3(0, moveSpeed, 0);
        moveSpeed += 0.1f;

        if (moveSpeed > 5)
            Destroy(GetComponent<SpriteRenderer>());
    }
}