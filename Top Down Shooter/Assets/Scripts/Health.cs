using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //variable max and initial health
    //ability to keep track of health and have other scripts view that value
    //ability to take damage from things
    //ability to heal
    //notify other objects of state changes when taking damage or when destroyed
    // Start is called before the first frame update
    
    [SerializeField, Tooltip("The maximum health allowed")]
    private float maxHealth = 200f;

    [SerializeField, Tooltip("The starting health value")]
    private float initialHealth = 200f;

    private float healthValue;

//    public float HealthValue
//    {
//        get;
//        private set { healthValue = value; }
//    }
//
//    public float HealthPercent
//    {
//        get;
//        private set
//        {
//            
//        }
//    }
  
}
