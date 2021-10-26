using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 1.5f;
    private float lifeTimeCounter;


    // Start is called before the first frame update
    void Start()
    {
        lifeTimeCounter = lifeTime;
    }

    public void RescaleRay(float newLenght)
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x, newLenght, 1);
    }

    // Update is called once per frame
    void Update()
    {
       if(lifeTimeCounter <= 0)
       {
           Destroy(this.gameObject);
       }
       lifeTimeCounter -= Time.deltaTime;
    }
}
