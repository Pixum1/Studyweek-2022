using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    //[SerializeField] float speedMultiplier;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        StartingForce();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity.magnitude);
       
    }
    private void FixedUpdate()
    {
        
    }
    private void StartingForce()
    {
        float x = 0.75f;
        float y = 0.75f;

        rb.velocity = new Vector2(x , y );
        rb.velocity *= speed;

    }
    private void OnCollisionEnter2D(Collision2D _other)
    {
        if(_other.collider.CompareTag("Paddel"))
        {
            float y = hitFactor(transform.position, _other.transform.position, _other.collider.bounds.size.y);
           
            Vector2 dir = new Vector2(0, 0);

            if(_other.transform.position.x < 0) 
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
    private float hitFactor(Vector2 _ballPos, Vector2 _paddlePos ,float _racketHeight)
    {
        return (_ballPos.y - _paddlePos.y) / _racketHeight;


    }
}
