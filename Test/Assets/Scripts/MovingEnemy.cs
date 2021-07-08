using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public Vector2 targetPosition;
    private Main Main;
    void FixedUpdate()
    {
        transform.Translate(targetPosition*Time.deltaTime*10f);
        Destroy(this.gameObject, 2);
    }
}
