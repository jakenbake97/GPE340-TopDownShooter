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

    [SerializeField] private int initialLives = 3;

    public int Lives { get; private set; }

    public static Player Player { get; private set; }

    public static bool Paused = false;
    private static float originalTimeScale;

    private void Awake()
    {
        Instance = this;
        Lives = initialLives;
        SpawnPlayer();
    }


    private void SpawnPlayer()
    {
        var temp = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        Player = temp.GetComponent<Player>();

        Player.Health.onDie.AddListener(HandlePlayerDeath);
    }

    private void HandlePlayerDeath()
    {
        Player.Health.onDie.RemoveListener(HandlePlayerDeath);
        if (Lives > 0)
        {
            Invoke(nameof(SpawnPlayer), playerRespawnDelay);
            Lives--;
        }
        else
        {
            // Game Over!   
        }
    }

    public static void Pause()
    {
        Paused = true;
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    public static void UnPause()
    {
        Paused = false;
        Time.timeScale = originalTimeScale;
    }
}