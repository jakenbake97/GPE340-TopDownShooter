using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnStationary : MonoBehaviour
{
    [SerializeField] private GameObject[] respawnObject;
    [SerializeField] private float delayTimer;

    private IEnumerator Respawn()
    {
        int index = Random.Range(0, respawnObject.Length);
        yield return new WaitForSeconds(delayTimer);
        var spawned = Instantiate(respawnObject[index], transform.position, Quaternion.identity);
        spawned.transform.parent = transform;
    }

    public void EventRespawn()
    {
        StartCoroutine(Respawn());
    }
}