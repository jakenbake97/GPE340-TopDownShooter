using UnityEngine;

public class LaserSight : MonoBehaviour
{
    private Weapon weaponRef;
    private LineRenderer line;

    private void Start()
    {
        weaponRef = GetComponent<Weapon>();
        line = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// if this weapon is equipped then we can enable the lineRenderer to draw the laser sight and set its start an
    /// endpoints. it starts at the weapon's barrel and ends wherever the raycast collides with something
    /// </summary>
    private void Update()
    {
        if (weaponRef.Equipped)
        {
            line.enabled = true;
            line.SetPosition(0, weaponRef.barrel.position);
            RaycastHit hit;
            if (Physics.Raycast(weaponRef.barrel.position, transform.forward, out hit, Mathf.Infinity))
            {
                line.SetPosition(1, hit.point);
            }
        }
    }
}