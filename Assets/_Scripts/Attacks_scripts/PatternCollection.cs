using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternCollection
{
    public Patterns[] patterns;

    public Patterns GetPatternFromRoom(string id) 
    {
        for(int i=0; i <patterns.Length; i++)
        {
            if(patterns[i].id_room.Equals(id))
            {
                return patterns[i];
            }
        }
        Debug.LogError("Clase: PatternCollection. Método: GetPatternFromRoom --> No existe un patrón con id de habitación: " + id);
        return null;
    }
}
