using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    private SpriteRenderer spr;
    public Sprite Sprite;
    public Rigidbody2D rb;
    public Action BallDestroyed;
    public PlayerController Owner;
    private GameManager gameManager;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        spr.sprite = Sprite;
    }

    public void LaunchBall(Vector2 _dir)
    {
        rb.velocity = _dir.normalized;
        rb.velocity *= speed;
    }

    private void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.collider.CompareTag("Paddel"))
        {
            float y = hitFactor(transform.position, _other.transform.position, _other.collider.bounds.size.y);

            Vector2 dir = new Vector2(0, 0);

            if (_other.transform.position.x < 0)
            {
                dir = new Vector2(1, y).normalized;

            }
            if (_other.transform.position.x > 0)
            {
                dir = new Vector2(-1, y).normalized;
            }
            rb.velocity = dir * speed;

        }

        Obstacle obstacle = _other.gameObject.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.GetDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Bounds"))
        {
            if (_other.transform.position.x < 0)
            {
                if (Owner.playerType == PlayerController.EPlayerInputType.player2)
                    gameManager.DamagePlayer1();

                Destroy(gameObject);
                BallDestroyed?.Invoke();

            }
            else if (_other.transform.position.x > 0)
            {
                if (Owner.playerType == PlayerController.EPlayerInputType.player1)
                    gameManager.DamagePlayer2();

                Destroy(gameObject);
                BallDestroyed?.Invoke();
            }
        }
    }

    private float hitFactor(Vector2 _ballPos, Vector2 _paddlePos, float _racketHeight)
    {
        return (_ballPos.y - _paddlePos.y) / _racketHeight;
    }
}
