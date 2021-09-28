using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100;

    public delegate void CollisionReachedDelegate();
    public static event CollisionReachedDelegate OnCollisionWithDestructor;

    void Start()
    {   

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("Destructor"))
        {
            if(OnCollisionWithDestructor != null)
            {
                OnCollisionWithDestructor();
            }
        }
    }
}
