using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeZoneController : MonoBehaviour
{
    private float lifeTime = 4f;
    private float lifeTimeCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        lifeTimeCounter = 0;
    }

    void OnEnable()
    {
        lifeTimeCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTimeCounter >= lifeTime)
        {
            this.gameObject.SetActive(false);
        }else
        {
            lifeTimeCounter += Time.deltaTime;
        }

    }
}
