using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField, Tooltip("Prefab del rayo que lanza la pared")]
    private GameObject wallRay;

    [SerializeField, Tooltip("Máxima longitud que alcanzará el rayo")]
    private float maxLenghtOfRay = 40;

    private Cell[,] cellMatrix;

    // Start is called before the first frame update
    void Start()
    {
        cellMatrix = this.GetComponent<RoomData>().cells;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Eliminar, solo debug
        if (Input.GetKeyDown(KeyCode.K))
        {
            //SOLO DEBUG
            SpawnRay(cellMatrix[0, 1]);
        }
    }

    //TODO: Pulir rotación
    //la rotación del rayo se contrala desde aquí a partir del tipo de celda. Esto se prodría corregir si cada pared tiene una rotación ya determinada, entonces desde aquí
    //podemos establecer la rotación
    //Eliminariamos tambien el código duplicado
    public void SpawnRay(Cell cell)
    {
        int cellType = cell.getCellType();
        if (cellType > 0)
        {
            int sizeCell = this.GetComponent<RoomData>().GetSizeCell();
            Vector2 cellCordinates = cell.getCoordinates();
            Vector3 spawnPosition = new Vector3(cellCordinates.x, cellCordinates.y, 0);

            //Hacemos un ajuste para que el rayo se dispare en mitad de la celda y no desde el vértice
            if (cellType == 2)
            {
                spawnPosition += new Vector3(0, sizeCell / 2, 0);
            }
            else
            {
                spawnPosition += new Vector3(sizeCell / 2, 0, 0);
            }

            //Hacemos Spawn
            GameObject newInstance = Instantiate(wallRay, spawnPosition, Quaternion.identity);
            newInstance.GetComponent<RayController>().RescaleRay(maxLenghtOfRay); //Alargamos el rayo

            //Rotamos según la pared desde la que se dispare
            if (cellType == 2)
            {
                newInstance.transform.Rotate(new Vector3(0, 0, -90));
            }
            else if (cellType == 1)
            {
                newInstance.transform.Rotate(new Vector3(0, 0, 180));
            }
        }
    }

    public float GetMaxLenghtOfRay()
    {
        return maxLenghtOfRay;
    }
}
