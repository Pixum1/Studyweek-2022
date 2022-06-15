using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float m_Speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity += Vector2.left * m_Speed;
    }

    private void Update()
    {
        transform.LookAt(rb.velocity);
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        Obstacle obstacle = _collision.gameObject.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.GetDamage();
        }
    }
}
