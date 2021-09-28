using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    //Formato
    /*
    {Tiempo[>0],    Tipo[0-3],  Muro[0-27], posX[6.8-(-6.4)],   posy[(-4.2)-(2.9)], dirx[], diry[]}
    Tipo 0= muro
    Tipo 1= proyectil
    Tipo 2= tiempoExtra
    Tipo 3= obstaculo

    Dirección:
    x = 1 -> derecha
    x= 0 -> no se mueve
    x = -1 -> izquierda

    y = 1 -> arriba
    y = 0 -> no se mueve
    y = -1 -> abajo

    y = 1 & x = 1 -> diagonal hacia arriba derecha 
    
    **El muro tambien es el tiempo de vida de los obstaculos si tipo==3
    */
    //Patrón rejilla
    private float[,] patron1={
        {0,0,0,0,0,0,0},
        {0,0,2,0,0,0,0},
        {0,0,4,0,0,0,0},
        {0,0,6,0,0,0,0},
        {0,0,8,0,0,0,0},
        {0,0,10,0,0,0,0},
        {0,0,12,0,0,0,0},
        {0,0,14,0,0,0,0},
        {0,0,16,0,0,0,0},
        {0,0,18,0,0,0,0},
        {0,0,20,0,0,0,0},
        {0,0,22,0,0,0,0},
        {0,0,24,0,0,0,0},
        {0,0,26,0,0,1,0.5f},
        {2,2,5,3.1f,2.3f,0,0}
    };

    //Paredes intercaladas
    private float[,] patron2={
        {0,0,15,0,0,0,0},
        {0,0,17,0,0,0,0},
        {0,2,5,-5.3f,-0.7f,0,0},
        {2,0,20,0,0,0,0},
        {2,0,22,0,0,0,0},
        {2,0,24,0,0,0,0},
        {2,0,26,0,0,0,0},
        {2,2,5,3.1f,2.3f,0,0},
        {4,0,0,0,0,0,0},
        {4,0,2,0,0,0,0},
        {4,0,4,0,0,0,0},
        {4,2,5,-5.6f,0.9f,0,0},
        {6,0,5,0,0,0,0},
        {6,0,7,0,0,0,0},
        {6,0,9,0,0,0,0},
        {6,0,11,0,0,0,0},
        {6,0,13,0,0,0,0},
        {6,2,5,-1.5f,-0.7f,0,0},
        {7,-1,0,0,0,0,0}
    };

    //Disparos desde derecha a izquierda
    private float[,] patron3={
        {0,1,27,0,0,0,0},
        {1,1,26,0,0,0,0},
        {2,1,25,0,0,0,0},
        {2,2,5,3.7f,0.7f,0,0},
        {3,1,24,0,0,0,0},
        {4,1,23,0,0,0,0},
        {5,1,22,0,0,0,0},
        {6,1,21,0,0,0,0},
        {6,2,5,-5.3F,-2.2f,0,0},
        {7,1,20,0,0,0,0},
        {8,1,19,0,0,0,0},
        {9,-1,0,0,0,0,0},
        {9,2,5,3.1f,2.3f,0,0}
    };

    //Dispaos desde arriba que empiezan a los lados y acaban en el centro
    private float[,] patron4={
        {0,1,27,0,0,0,0},
        {0,1,19,0,0,0,0},
        {0,2,5,-5.9F,-3.6f,0,0},
        {1,1,26,0,0,0,0},
        {1,1,20,0,0,0,0},
        {2,1,25,0,0,0,0},
        {2,1,21,0,0,0,0},
        {2,2,5,3F,1.5f,0,0},
        {3,1,24,0,0,0,0},
        {3,1,22,0,0,0,0},
        {4,1,23,0,0,0,0},
        {4,2,5,0,-3f,0,0},
        {6,-1,0,0,0,0,0}
    };

    //Obstaculos + disparos
    private float[,] patron5={
        {0,3,6,5.1f,2.1f,-1,0},
        {0,3,6,-5.3f,-0.7f,1,0},
        {0,3,6,5.3f,-3.7f,-1,0},
        {1,1,1,0,0,0,0},
        {1,1,3,0,0,0,0},
        {2,1,1,0,0,0,0},
        {2,1,3,0,0,0,0},
        {2,2,5,-5.6F,-0.6f,0,0},
        {3,1,1,0,0,0,0},
        {3,1,3,0,0,0,0},
        {3,2,5,0.3F,-3.7f,0,0},
        {4,1,1,0,0,0,0},
        {4,1,3,0,0,0,0},
        {4,2,5,0F,2.4f,0,0},
        {5,1,1,0,0,0,0},
        {5,1,3,0,0,0,0},
        {6,1,1,0,0,0,0},
        {6,1,3,0,0,0,0},
        {8,-1,0,0,0,0,0},
        {8,2,5,0,-0.6f,0,0}
    };

private float[,] patron6={
        {0,0,0,0,0,0,0},
        {0,2,5,0,-0.6f,0,0},
        {0,0,2,0,0,0,0},
        {0,0,4,0,0,0,0},
        {2,1,25,0,0,0,0},
        {2,1,21,0,0,0,0},
        {3,0,17,0,0,0,0},
        {3,0,15,0,0,0,0}
        
    };

private float[,] patron7={
        {0,1,0,0,0,0,0},
        {0,1,1,0,0,0,0},
        {0,1,2,0,0,0,0},
        {0,1,3,0,0,0,0},
        {1,1,0,0,0,0,0},
        {1,1,1,0,0,0,0},
        {1,1,4,0,0,0,0},
        {1,1,3,0,0,0,0},
        {2,1,0,0,0,0,0},
        {2,1,4,0,0,0,0},
        {2,1,2,0,0,0,0},
        {2,1,3,0,0,0,0},
        {2,2,5,2,0,0,0},
        {2,2,5,-2,0,0,0},
        {2,1,25,2,0,0,0},
        {2,1,22,-2,0,0,0},
        {3,1,0,0,0,0,0},
        {3,1,1,0,0,0,0},
        {3,1,2,0,0,0,0},
        {3,1,4,0,0,0,0},
        {4,1,4,0,0,0,0},
        {4,1,1,0,0,0,0},
        {4,1,2,0,0,0,0},
        {4,1,3,0,0,0,0},
    };

    private float[,] patron8={
        {0,1,0,0,0,0,0},
        {0,1,1,0,0,0,0},
        {0,1,2,0,0,0,0},
        {0,1,3,0,0,0,0},
        {1,1,0,0,0,0,0},
        {1,1,1,0,0,0,0},
        {1,1,2,0,0,0,0},
        {1,1,4,0,0,0,0},
        {2,2,5,2,0,0,0},
        {2,2,5,-2,0,0,0},
        {2,1,25,2,0,0,0},
        {2,1,22,-2,0,0,0},
        {2,1,0,0,0,0,0},
        {2,1,1,0,0,0,0},
        {2,1,4,0,0,0,0},
        {2,1,3,0,0,0,0},
        {3,1,0,0,0,0,0},
        {3,1,4,0,0,0,0},
        {3,1,2,0,0,0,0},
        {3,1,3,0,0,0,0},
        {4,1,4,0,0,0,0},
        {4,1,1,0,0,0,0},
        {4,1,2,0,0,0,0},
        {4,1,3,0,0,0,0},
    };

        private float[,] patron9={
        {0,1,0,0,0,0,0},
        {0,1,2,0,0,0,0},
        {0,1,4,0,0,0,0},
        {0,1,6,0,0,0,0},
        {0,1,8,0,0,0,0},
        {0,1,10,0,0,0,0},
        {0,1,12,0,0,0,0},
        {0,1,14,0,0,0,0},
        {0,1,16,0,0,0,0},
        {0,1,18,0,0,0,0},
        {0,1,20,0,0,0,0},
        {0,1,22,0,0,0,0},
        {0,1,24,0,0,0,0},
        {0,1,26,0,0,1,0.5f},
        {0,2,5,0,-0.6f,0,0}
    };
    
    private List<float[,]> patrones=new List<float[,]>();


    public List<Transform> wallPortions = new List<Transform>();

    public GameObject extraTime;

    public GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        patrones.Add(patron1);
        patrones.Add(patron2);
        patrones.Add(patron3);
        patrones.Add(patron4);
        patrones.Add(patron5);
        patrones.Add(patron6);
        patrones.Add(patron7);
        patrones.Add(patron8);
        patrones.Add(patron9);


        //Recorremos todos los hijos y los añadimos a una lista
        foreach(Transform child in transform)
        {
            foreach(Transform gChild in child.transform)
            {
                wallPortions.Add(gChild);
            }
        }

        StartCoroutine(loadPatron());
    }

    private IEnumerator loadPatron(){
        
        //Seleccion del patron a utilizar
        int index=Random.Range(0 ,patrones.Count);
        float[,] patron=patrones[index];

        //Ejecucion del patron
        //Se va ejecutando por unidades de tiempo
        int tiempo=0;
        int i=0;
        
        //Mientras el tiempo sea menor o igual que el tiempo de la ultima tarea
        while(tiempo<=patron[patron.GetLength(0)-1,0]){
            //Ejecuto todas las tareas con tiempo igual la variable tiempo
            while(i<patron.GetLength(0) && tiempo==patron[i,0]){
                //Ejecucion en base al tipo
                switch((int)patron[i,1]){
                    case 0:
                        TriggerWallAttack((int)patron[i,2]);
                    break;
                    case 1:
                        TriggerProjectile((int)patron[i,2]);
                    break;
                    case 2:
                        PlaceTime(patron[i,3],patron[i,4],patron[i,2]);
                    break;
                    case 3:
                        PlaceObstacle(patron[i,3],patron[i,4],patron[i,5],patron[i,6],patron[i,2]);
                    break;
                }
                i++;
            }
            yield return new WaitForSeconds(1);
            tiempo++;
        }

        //Llamada recursiva tras un tiempo
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(loadPatron());
    }

    private void TriggerWallAttack(int index){
        WallController _wallController = wallPortions[index].GetComponent<WallController>();
        _wallController.TriggerAttack(); 
    }

    private void TriggerProjectile(int index){
         WallController _wallController = wallPortions[index].GetComponent<WallController>();
        _wallController.generateProjectile(); 
    }

    private void PlaceTime(float posX, float posY,float lifetime){
        GameObject exT=Instantiate(extraTime,new Vector3(posX,posY,0),new Quaternion(0,0,0,0));
        TimeController tc=exT.GetComponent<TimeController>();
        tc.lifeTime=lifetime;
    }
    private void PlaceObstacle(float posX, float posY, float dirX,float dirY,float lifetime){
        GameObject obs=Instantiate(obstacle,new Vector3(posX,posY,0),new Quaternion(0,0,0,0));
        ObstacleController oc=obs.GetComponent<ObstacleController>();
        oc.directionX=dirX;
        oc.directionY=dirY;
        oc.lifeTime=lifetime;
    }
}
