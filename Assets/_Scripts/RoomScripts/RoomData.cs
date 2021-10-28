using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoomData : MonoBehaviour
{

    [SerializeField, Tooltip("Path del archivo JSON con los datos de la habitación")]
    private string JSON_path;
    private RoomCollection _roomsCollection;

    public Cell[,] cells;
    
    #region Variables Matriz de celdas
    [Header("Datos matriz")]
    [SerializeField, Tooltip("Coordenada X de origen de la matriz/habitación")]
    public float coordinateX = 0;
    [SerializeField, Tooltip("Coordenada Y de origen de la matriz/habitación")]
    public float coordinateY = 0;

    [SerializeField, Tooltip("Tamaño de la celda o de la pared. Cuantas unidades ocupará.")]
    public int sizeCell;
    
    [SerializeField]
    private int rows;
    
    [SerializeField]
    public int columns;
    #endregion

    #region Variables de habitación
    [Header("Variables habitación")]
    [SerializeField, Tooltip("Identificador de la habitación en el JSON")]
    private string id_Room;
    [SerializeField]
    private GameObject UpWall;
    [SerializeField]
    private GameObject SideWall;
    [SerializeField]
    private GameObject DownWall;
    [SerializeField]
    private GameObject floor;
    #endregion

    [Header("Opciones para debug")]
    //SOLO DEBUG
    public GameObject wall;
    public Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        cells = new Cell[rows, columns];
        ReadRoomJSON();
        FillMatrix();
        //PrintMatrix();
        SpawnRoom();
        
    }

    /// <summary>
    /// Carga el Json con los datos de la habitación en un objeto RoomCollection
    /// </summary>
    private void ReadRoomJSON()
    {
        using (StreamReader stream = new StreamReader(JSON_path))
        {
            string json = stream.ReadToEnd();
            _roomsCollection = JsonUtility.FromJson<RoomCollection>(json);
        }
    }

    /// <summary>
    /// Crea las celdas correspondientes con sus coordenadas(en función del tamaño de las celdas especificado) y sus tipos.
    /// </summary>
    private void FillMatrix()
    {
        float initialX = coordinateX; //Almacenamos el valor de la coordenada inicial para poder resetearlo durante el loop
        for(int i=0; i<rows; i++)
        {
            for(int j=0; j<columns; j++)
            {
                //Creamos y asignamos la celda
                Vector2 pos = new Vector2(coordinateX, coordinateY);
                cells[i,j] = new Cell(pos);
                coordinateX += sizeCell;
            }
            coordinateX = initialX;
            coordinateY -= sizeCell; //Restamos porque la matriz va hacia abajo
        }

        //Seteamos los tipos de la celda con los datos leidos del Json
        Room newRoom = _roomsCollection.GetRoomFromID(id_Room);

        for(int i=0; i<newRoom.walls.Length; i++)
        {
            Vector3 wallData = newRoom.walls[i];
            cells[(int)wallData.x, (int)wallData.y].setCellType((int)wallData.z);
        }

    }

    /// <summary>
    /// Crea/Spawnea la habitación con todas sus paredes
    /// </summary>
    private void SpawnRoom()
    {
        GameObject prefabCell;
        Vector2 position;

        for(int i=0; i<rows; i++)
        {
            for(int j=0; j<columns; j++)
            {
                position = cells[i,j].getCoordinates();
                prefabCell = ObtainCellPrefab(cells[i,j].getCellType());
                if(prefabCell != null)
                {
                    GameObject newInstance = Instantiate(prefabCell, position, prefabCell.transform.rotation, this.transform);
                    Vector3 newScale = Vector3.Scale(newInstance.transform.localScale, new Vector3(sizeCell, sizeCell,1)); //Podemos hacer esto para escalarlo
                    newInstance.transform.localScale = newScale;
                }
            }
        }
    }

    /// <summary>
    /// Determina que prefab hay que utilizar
    /// </summary>
    /// <param name="cellType">Int que indica el tipo de celda de la que se quiere obtener el prefab</param>
    /// <returns>GameObject del prefab solicitado</returns>
    private GameObject ObtainCellPrefab(int cellType)
    {
        switch (cellType)
        {
            case 0: 
                return floor;
            case 1: 
                return UpWall;
            case 2: 
                return SideWall;
            case 3: 
                return DownWall;
            default:
                return null;
        }
    }

    //SOLO DEBUG
    private void PrintMatrix()
    {
        for(int i=0; i<rows; i++)
        {
            for(int j=0; j<columns; j++)
            {
                Debug.Log(cells[i,j].getCellType());
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

    public int GetSizeCell()
    {
        return sizeCell;
    }
}
