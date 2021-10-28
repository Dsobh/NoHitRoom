using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Elements
{
   public Vector3[] elements;

   public Vector3 GetAtIndex(int index)
   {
       return elements[index];
   }
}
