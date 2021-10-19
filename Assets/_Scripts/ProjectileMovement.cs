using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ProjectileMovement : MonoBehaviour
{

    public float speed=2;
    private Rigidbody2D projectile;
    private Vector2 moveDirection=Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
        projectile = GetComponent<Rigidbody2D>();
        double rotation=Math.Round(gameObject.transform.rotation.w, 1);
        
        if(rotation==1)
                moveDirection = new Vector2(0,1);
        else if(rotation==0.7)
                moveDirection = new Vector2(1,0);
        else if(rotation==-0.7)
                moveDirection = new Vector2(-1,0);     
        else if(rotation==0)
                moveDirection = new Vector2(0,-1);
        projectile.velocity=moveDirection*speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        projectile.velocity=moveDirection*speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name.Contains("Destructor"))
            Destroy(this.gameObject);

        if(other.CompareTag("TimeZone"))
        {
                speed = speed/7;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("TimeZone"))
        {
                speed = speed*7;
        }
    }
}
