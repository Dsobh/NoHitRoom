using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";

    [SerializeField, Tooltip("Velocidad a la que se mueve el jugador")]
    private float speed = 6.5f;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D playerRB;

    [SerializeField, Tooltip("Suavizado en el movimiento")]
    private float smoothTime = 0.04f;

    private Vector2 lastMovement = Vector2.zero;

    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Capturamos el input. GetAxisRaw devuelve valores 0 y 1, no intermedios
        moveDirection.x = Input.GetAxisRaw(HORIZONTAL);
        moveDirection.y = Input.GetAxisRaw(VERTICAL);
    }

    void FixedUpdate()
    {
        //Calculamos el último movimiento a través de un Lerp para hacer el suavizado
        lastMovement = Vector2.Lerp(lastMovement, moveDirection, smoothTime);
        this.transform.Translate(lastMovement * speed);

    }
}
