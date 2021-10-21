using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    public string id_room;
    
    /*
    ** Walls contiene una lista de vectores3 que corresponde a las paredes:
    ** x -> posición x en la matriz
    ** y -> posición y en la matriz
    ** z -> tipo de pared:
    ******* VoidCell = -1,
    ******* Floor = 0,
    ******* UpWall = 1,
    ******* SideWall = 2,
    ******* DownWall = 3
    **
    */
    public Vector3[] walls;


}
