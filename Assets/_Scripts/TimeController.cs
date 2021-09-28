using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    public float lifeTime = 3f;

    private float timeToEndLife;
    // Start is called before the first frame update
    void Start()
    {
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
    }
}
