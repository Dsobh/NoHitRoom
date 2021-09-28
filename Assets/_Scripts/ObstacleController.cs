using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    [SerializeField, Tooltip("Velocidad de la plataforma")]
    private float platformSpeed = 10f;
    public float directionX, directionY;
    public float lifeTime = 3f;
    private float timeToEndLife;
    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(directionX, directionY, 0);
        timeToEndLife = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToEndLife >= lifeTime)
        {
            Destroy(this.gameObject);
        }else{
            timeToEndLife += Time.deltaTime;
        }
        this.transform.Translate(platformSpeed * Time.deltaTime * direction);
    }

    void OnEnable()
    {
        RotateObject.OnCollisionWithDestructor += HandleChildCollision;
    }

    void OnDisable()
    {
        RotateObject.OnCollisionWithDestructor -= HandleChildCollision;
    }

    void HandleChildCollision()
    {
        direction = direction * -1;
    }
}
