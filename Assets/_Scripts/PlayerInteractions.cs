using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    public delegate void PickTimeDelegate();
    public static event PickTimeDelegate OnPickExtraTime;

    public delegate void GameOverDelegate();
    public static event GameOverDelegate OnGameOver;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("ExtraTime"))
        {
            if(OnPickExtraTime != null)
            {
                gameObject.GetComponent<AudioSource>().Play();
                OnPickExtraTime();
            }
            
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("DeadZone"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);

            if(OnGameOver != null)
            {
                Destroy(this.gameObject);
                OnGameOver();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("DeadZone"))
        {
            if(OnGameOver != null)
            {
                Destroy(this.gameObject);
                OnGameOver();
            }
        }
    }
}
