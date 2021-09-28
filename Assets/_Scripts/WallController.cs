using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    //Inicia el ataque básico
    private bool attack;
    //Propertie para el ataque básico
    public bool ATTACK
    {
        get{return this.attack;}
        set{this.attack = value;}
    }

    private Vector3 baseSize; //Escala inicial de la pared
    private Vector3 maxScale; //Límite de extensión de la pared
    [Header("Valores para las Animaciones de Extensión")]
    [SerializeField, Tooltip("Cuanto se va a extender la pared")]
    private float extension;
    [SerializeField, Tooltip("Velocidad a la que crece")]
    private float scaleSpeed = 0.01f;
    [SerializeField, Tooltip("Cuanto crece en cada unidad de tiempo")]
    private float scaleStep = 0.5f;
    [SerializeField, Tooltip("Tiempo entre parpadeos")]
    private float TimeBetweenBlink = 0.2f;
    private int numberOfBlinks = 5;
    [SerializeField, Tooltip("Duration of the attack")]
    private float attackDuration = 1.5f;

    [Header("Valores para las animaciones de disparo")]
    [SerializeField, Tooltip("Tiempo que tarda en actualizar el color")]
    private float colorFadeTime = 0.1f;
    [SerializeField, Tooltip("Cantidad de color que variara por unidad de tiempo")]
    private float colorLerpDecrement = 0.3f;
    private SpriteRenderer _spriteRenderer;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        baseSize = this.transform.localScale;
        maxScale = new Vector3(baseSize.x, extension, baseSize.z);
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Función que lanza el ataque básico (extensión de la pared)
    /// </summary>
    public void TriggerAttack()
    {
        StartCoroutine(BlinkWall());
        attack = true;
    }

    /// <summary>
    /// Cortutina que extiende la pared hasta el lado opuesto
    /// </summary>
    /// <returns></returns>
    private IEnumerator ScaleWall()
    {
        while(this.transform.localScale.y <= extension)
        {
            float newY = this.transform.localScale.y + scaleStep;
            this.transform.localScale = new Vector3(this.transform.localScale.x, newY, this.transform.localScale.z);
            yield return new WaitForSeconds(scaleSpeed); 
        }
        
        //Esperamos antes de reducir el ataque de nuevo
        yield return new WaitForSeconds(attackDuration);
        
        while(this.transform.localScale.y >= baseSize.y)
        {
            float newY = this.transform.localScale.y - scaleStep;
            this.transform.localScale = new Vector3(this.transform.localScale.x, newY, this.transform.localScale.z);
            yield return new WaitForSeconds(scaleSpeed); 
        }
        _spriteRenderer.color = Color.white;
        attack = false;
    }

    /// <summary>
    /// Corutina que controla el parpadeo de la porción de pared y luego llama a ScaleWall
    /// </summary>
    /// <returns></returns>
    private IEnumerator BlinkWall()
    {
        for(int i=0; i<numberOfBlinks; i++)
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(TimeBetweenBlink);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(TimeBetweenBlink);
        }
        _spriteRenderer.color = Color.red;
        StartCoroutine(ScaleWall());
    }

    private IEnumerator ChargeProyectil()
    {
        for(float i = 1f; i >= 0; i-= colorLerpDecrement)
        {
            Color c = _spriteRenderer.color;
            c.g = i;
            c.b = i;
            _spriteRenderer.color = c;
            yield return new WaitForSeconds(colorFadeTime);
        }
        Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
        _spriteRenderer.color = Color.white;
    }

    public void generateProjectile(){
        StartCoroutine(ChargeProyectil());
    }
}
