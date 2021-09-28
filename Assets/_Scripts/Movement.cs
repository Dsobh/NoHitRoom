using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField, Tooltip("Velocidad a la que se mueve el jugador")]
    private float speed = 6.5f;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D playerRB;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    void FixedUpdate() {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");
        
        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            playerRB.velocity = Vector2.zero;
            playerRB.angularVelocity = 0;
        }else{
            playerRB.velocity = moveDirection * speed;
        }
    }
}
