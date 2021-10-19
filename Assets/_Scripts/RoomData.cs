using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    private Vector2[,] roomCoordinates;
    
    [Header("Datos matriz")]
    [SerializeField, Tooltip("Coordenada X de origen de la matriz/habitación")]
    public float coordinateX = 0;
    [SerializeField, Tooltip("Coordenada Y de origen de la matriz/habitación")]
    public float coordinateY = 0;

    [SerializeField, Tooltip("Tamaño de la celda o de la pared. Cuantas unidades ocupará.")]
    private int sizeCell;
    
    [SerializeField]
    private int rows;
    
    [SerializeField]
    public int columns;

    [Header("Opciones para debug")]
    //SOLO DEBUG
    public GameObject wall;
    public Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        roomCoordinates = new Vector2[rows, columns];
        FillMatrix();
        PrintMatrix();
    }

    //Llena la matriz con las coordenadas de los vértices en función del tamaño de la celda.
    private void FillMatrix()
    {
        float initialX = coordinateX; //Almacenamos el valor de la coordenada inicial para poder resetearlo durante el loop
        for(int i=0; i<rows; i++)
        {
            for(int j=0; j<columns; j++)
            {
                roomCoordinates[i,j] = new Vector2(coordinateX, coordinateY);
                coordinateX += sizeCell;
            }
            coordinateX = initialX;
            coordinateY -= sizeCell; //Restamos porque la matriz va hacia abajo
        }
    }

    //SOLO DEBUG
    private void PrintMatrix()
    {
        for(int i=0; i<rows; i++)
        {
            for(int j=0; j<columns; j++)
            {
                Debug.Log(roomCoordinates[i,j]);
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            wall.transform.position = newPosition - new Vector3(1,0,0);
        }
    }
}
