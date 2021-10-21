using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomCollection 
{
    public Room[] rooms;

    public Room GetRoomFromID(string id) {
        {
            for(int i=0; i<rooms.Length; i++)
            {
                if(rooms[i].id_room.Equals(id))
                {
                    return rooms[i];
                }
            }
            Debug.LogError("Clase: RoomCollection. Método: GetRoomFromID --> No existe una habitación con id: " + id);
            return null;
        }
    }
}
