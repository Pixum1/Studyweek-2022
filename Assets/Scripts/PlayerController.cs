using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private int startingHealth;
    public int Health;

    [HideInInspector]
    public BallBehaviour ball;
    [SerializeField]
    private BallBehaviour m_BallPrefab;
    [SerializeField]
    private Transform[] m_Bounds;

    private bool ballDocked;

    private float ballRespawnTimer;
    [SerializeField]
    private float m_BallRespawnTime = 10f;

    [SerializeField]
    private Sprite m_BallSprite;

    public enum EPlayerInputType
    {
        player1,
        player2
    };

    public EPlayerInputType playerType;

    private float ballDockPosX;

    private string vertAxis;

    private void Start()
    {
        Health = startingHealth;

        ballRespawnTimer = m_BallRespawnTime;

        SpawnBall();

        switch (playerType)
        {
            case EPlayerInputType.player1:
                vertAxis = "Vertical";
                ballDockPosX = this.transform.position.x + this.transform.localScale.x + .1f;
                break;
            case EPlayerInputType.player2:
                vertAxis = "Vertical2";
                ballDockPosX = this.transform.position.x - this.transform.localScale.x - .1f;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        float vertPos = Input.GetAxisRaw(vertAxis);

        if (ball != null)
        {
            ballRespawnTimer = m_BallRespawnTime;
            if (ballDocked)
                ball.transform.position = new Vector3(ballDockPosX, this.transform.position.y, ball.transform.position.z);

            switch (playerType)
            {
                case EPlayerInputType.player1:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ballDocked = false;
                        ball.LaunchBall(Vector2.right);
                        ball = null;
                    }
                    break;
                case EPlayerInputType.player2:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        ballDocked = false;
                        ball.LaunchBall(Vector2.left);
                        ball = null;
                    }
                    break;
                default:
                    break;
            }
        }

        if (ball == null)
        {
            ballRespawnTimer -= Time.deltaTime;
            if (ballRespawnTimer <= 0)
            {
                SpawnBall();
                ballRespawnTimer = m_BallRespawnTime;
            }
        }

        if (vertPos != 0)
            transform.position += Vector3.up * (vertPos * m_Speed * Time.deltaTime);

        KeepInBounds();
    }

    private void KeepInBounds()
    {
        if (transform.position.y > m_Bounds[0].position.y - transform.localScale.y / 2f)
        {
            transform.position = new Vector3(transform.position.x, m_Bounds[0].position.y - transform.localScale.y / 2f);

        }
        else if (transform.position.y < m_Bounds[1].position.y + transform.localScale.y / 2f)
        {
            transform.position = new Vector3(transform.position.x, m_Bounds[1].position.y + transform.localScale.y / 2f);
        }
    }

    private void SpawnBall()
    {
        if (ball == null)
        {
            BallBehaviour b = Instantiate(m_BallPrefab);
            b.Owner = this;
            b.BallDestroyed += SpawnBall;
            ball = b;
            ballDocked = true;
            ball.Sprite = m_BallSprite;

            switch (playerType)
            {
                case EPlayerInputType.player1:
                    b.transform.position = this.transform.position;
                    b.transform.position += Vector3.right * (this.transform.localScale.x + .1f);
                    break;
                case EPlayerInputType.player2:
                    b.transform.position = this.transform.position;
                    b.transform.position += Vector3.left * (this.transform.localScale.x + .1f);
                    break;
                default:
                    break;
            }
        }
    }
}
