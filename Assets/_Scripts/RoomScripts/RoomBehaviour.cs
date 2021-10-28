using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField, Tooltip("Path del archivo JSON con los datos de la habitación")]
    private string JSON_path;

    public PatternCollection _patternCollection;

    [SerializeField, Tooltip("Prefab del rayo que lanza la pared")]
    private GameObject wallRay;

    [SerializeField, Tooltip("Máxima longitud que alcanzará el rayo")]
    private float maxLenghtOfRay = 40;

    private Cell[,] cellMatrix;

    // Start is called before the first frame update
    void Start()
    {
        ReadPatternJSON();
        cellMatrix = this.GetComponent<RoomData>().cells;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Eliminar, solo debug
        if (Input.GetKeyDown(KeyCode.K))
        {
            //SOLO DEBUG
            //SpawnRay(cellMatrix[0, 1]);
            ExecutePattern(_patternCollection.patterns[0].sequence[1].elements);
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

    public void ExecutePattern(Vector3[] patternSequence)
    {
        for(int i=0; i<patternSequence.Length; i++)
        {
            Debug.Log(patternSequence[i]);
            int attackType = (int)patternSequence[i].z;

            if(attackType == 0)
            {
                SpawnRay(cellMatrix[(int)patternSequence[i].y, (int)patternSequence[i].x]);
            }
        }
    }

    public float GetMaxLenghtOfRay()
    {
        return maxLenghtOfRay;
    }

    /// <summary>
    /// Carga el Json con los datos de la habitación en un objeto RoomCollection
    /// </summary>
    private void ReadPatternJSON()
    {
        using (StreamReader stream = new StreamReader(JSON_path))
        {
            string json = stream.ReadToEnd();
            _patternCollection = JsonUtility.FromJson<PatternCollection>(json);
        }
    }
}
