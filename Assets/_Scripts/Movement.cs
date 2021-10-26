using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoHitRoom;

public class Movement : MonoBehaviour
{

    //Variables del movimiento
    private Rigidbody2D playerRB;
    private Vector3 moveDirection = Vector2.zero; //Dirección del player según el input
    [SerializeField, Tooltip("Velocidad a la que se mueve el jugador")]
    private float speed = 1.13f;
    [SerializeField, Tooltip("Suavizado en el movimiento")]
    private float smoothTime = 0.3f;
    private Vector2 lastMovement = Vector2.zero; //Almacenamos la posición antes del movimiento
    
    //Variables del Dash
    private bool isDashButtonPreshed = false;

    [SerializeField, Tooltip("Cantidad de desplazamiento con el Dash")]
    private float dashAmount = 2.4f;
    [SerializeField]
    private float dashCouldDown = 15f;
    private float dashCouldDownCounter = 15f;
    [SerializeField]
    private GameObject timeZone;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !timeZone.activeInHierarchy)
        {
            timeZone.transform.position = this.gameObject.transform.position;
            timeZone.SetActive(true);
        }
        //Capturamos el input. GetAxisRaw devuelve valores 0 y 1, no intermedios
        moveDirection.x = Input.GetAxisRaw(Constants.HORIZONTAL);
        moveDirection.y = Input.GetAxisRaw(Constants.VERTICAL);

        //Calculamos el último movimiento a través de un Lerp para hacer el suavizado
        lastMovement = Vector3.Lerp(lastMovement, moveDirection, smoothTime);
        this.transform.Translate(lastMovement * speed);

        //Capturamos el dash
        //TODO: Comprobar si podemos capturar la entrada de key con el input system usando Jump
        if(Input.GetKeyDown(KeyCode.Space) && dashCouldDownCounter >= dashCouldDown)
        {
            isDashButtonPreshed = true;
            dashCouldDownCounter = 0;
        }
        dashCouldDownCounter += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if(isDashButtonPreshed)
        {
            //Movemos hacia una dirección específica una cantidad de movimiento concreta (es básicamente un teletransporte sin usar físicas)
            this.transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection*dashAmount, dashAmount);
            isDashButtonPreshed = false;
        }
    }
}
