using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed;
    Rigidbody2D Rb;


   
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }



    private void PlayerInput() 
    {
        Input.GetKey(KeyCode.W);
        
    
    }
}
