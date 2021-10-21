using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    enum CellType
    {
        VoidCell = -1,
        Floor = 0,
        UpWall = 1,
        SideWall = 2,
        DownWall = 3
    }

    private Vector2 coordinates; //Coordenadas de la casilla/celda
    private CellType _cellType; //Tipo de casilla/celda

    //Constructor
    public Cell(Vector2 pos)
    {
        coordinates = pos;
        _cellType = CellType.VoidCell;
    }

    #region Getters & Setters
    public Vector2 getCoordinates()
    {
        return coordinates;
    }

    public int getCellType()
    {
        return (int)_cellType;
    }

    public void setCoordinates(Vector2 pos)
    {
        this.coordinates = pos;
    }

    public void setCellType(int type)
    {
        switch (type)
        {
            case -1:
                _cellType = CellType.VoidCell;
                break;
            case 0:
                _cellType = CellType.Floor;
                break;
            case 1:
                _cellType = CellType.UpWall;
                break;
            case 2:
                _cellType = CellType.SideWall;
                break;
            case 3:
                _cellType = CellType.DownWall;
                break;
            default:
                Debug.LogError("Clase: Cell. Método: setCellType() --> El valor (" + type + ") no es válido");
                break;
        }
    }
    #endregion
}
