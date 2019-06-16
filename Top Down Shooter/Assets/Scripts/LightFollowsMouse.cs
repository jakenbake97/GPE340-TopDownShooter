using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollowsMouse : MonoBehaviour
{
   [SerializeField,Tooltip("The height above the ground the light should follow the mouse at")] 
   private float lightHeight = 2f;

   public void LightMovesToPoint(Vector3 targetPoint)
   {
      transform.position = new Vector3(targetPoint.x, targetPoint.y + lightHeight, targetPoint.z);
   }
}
