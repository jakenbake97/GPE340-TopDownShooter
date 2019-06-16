using UnityEngine;

public class LightFollowsMouse : MonoBehaviour
{
   [SerializeField,Tooltip("The height above the ground the light should follow the mouse at")] 
   private float lightHeight = 2f;

   public void LightMovesToPoint(Vector3 targetPoint) //Takes a target point from the mouse position in the
                                                      //input manager and moves the light this script is attached to,
                                                      //to that point + the lightHeight offset in the y axis
   {
      transform.position = new Vector3(targetPoint.x, targetPoint.y + lightHeight, targetPoint.z);
   }
}
