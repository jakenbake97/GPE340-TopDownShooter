using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Level Settings")] [SerializeField, Tooltip("The prefab for the player")]
    private GameObject playerPrefab;

    [SerializeField, Tooltip("The spawn point for the player")]
    private Transform playerSpawnPoint;

    [SerializeField, Tooltip("The amount of time it takes before the player is re-spawned")]
    private float playerRespawnDelay = 3f;

    public static Player Player { get; private set; }
    private int debugNumber = 0;

    private void Awake()
    {
        Instance = this;
        SpawnPlayer();
    }


    private void SpawnPlayer()
    {
        var temp = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        temp.name = $"Player{debugNumber}";
        Player = temp.GetComponent<Player>();

        Player.Health.onDie.AddListener(HandlePlayerDeath);
        debugNumber++;
    }

    private void HandlePlayerDeath()
    {
        Player.Health.onDie.RemoveListener(HandlePlayerDeath);
        Invoke(nameof(SpawnPlayer), playerRespawnDelay);
    }
}