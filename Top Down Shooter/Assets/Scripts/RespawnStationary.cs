using System.Collections;
using UnityEngine;

public class RespawnStationary : MonoBehaviour
{
    [SerializeField, Tooltip("Array of objects this should randomly pull from to spawn")]
    private GameObject[] respawnObject;

    [SerializeField, Tooltip("How long it should take for an item to respawn")]
    private float delayTimer;

    /// <summary>
    /// coroutine that picks a random item from the array to spawn and then instantiates it after waiting for a
    /// certain amount of time
    /// </summary>
    private IEnumerator Respawn()
    {
        int index = Random.Range(0, respawnObject.Length);
        yield return new WaitForSeconds(delayTimer);
        var spawned = Instantiate(respawnObject[index], transform.position, Quaternion.identity);
        spawned.transform.parent = transform;
    }

    /// <summary>
    /// starts the coroutine when called by events
    /// </summary>
    public void EventRespawn()
    {
        StartCoroutine(Respawn());
    }
}