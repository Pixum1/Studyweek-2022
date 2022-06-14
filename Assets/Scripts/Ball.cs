using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D _collision)
    {
        Obstacle obstacle = _collision.gameObject.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.GetDamage();
        }
    }
}
