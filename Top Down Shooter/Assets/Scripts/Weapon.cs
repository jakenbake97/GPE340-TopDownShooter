using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float fireRate;

    public enum WeaponAnimationType
    {
        hipFire = 0,
        RifleGrip = 1,
        HandgunGrip = 2
    }

    public WeaponAnimationType animationType = WeaponAnimationType.hipFire;
}