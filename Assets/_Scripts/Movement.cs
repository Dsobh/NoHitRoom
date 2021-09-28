using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 1;
    private Vector2 moveDirection=Vector2.zero;
    private Rigidbody2D player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        moveDirection = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        if(Input.GetButton("Jump"))
            moveDirection*=speed*2;
        else
            moveDirection*=speed;
        player.velocity=moveDirection;
    }
}
