using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Level Settings")] [SerializeField, Tooltip("The prefab for the player")]
    private GameObject playerPrefab;

    [SerializeField, Tooltip("The spawn point for the player")]
    private Transform playerSpawnPoint;

    [SerializeField, Tooltip("The amount of time it takes before the player is re-spawned")]
    private float playerRespawnDelay = 3f;

    [SerializeField, Tooltip("The number of lives the player starts with")]
    private int initialLives = 3;

    [SerializeField] private AudioClip gameOverSound;

    [SerializeField] private AudioMixerGroup fXMixer;

    [SerializeField, Tooltip("Reference to the spawner in the scene")]
    private EnemySpawner spawner;

    [HideInInspector] public int enemiesLeft;

    [SerializeField, Tooltip("invoked when the game is paused")]
    private UnityEvent onPause;

    [SerializeField, Tooltip("Invoked when the game resumes from pause")]
    private UnityEvent onResume;

    [SerializeField, Tooltip("Invoked when the player dies and is out of lives")]
    private UnityEvent onLose;

    [SerializeField, Tooltip("Invoked when there are no enemies left")]
    private UnityEvent onWin;

    public static int Lives { get; private set; }

    public static Player Player { get; private set; }

    public static bool Paused = false;
    private static float originalTimeScale = 1f;

    private void Awake()
    {
        Instance = this;
        Lives = initialLives;
        SpawnPlayer();
        enemiesLeft = spawner.enemyCount;
        Resume();
    }

    private void Update()
    {
        HandleWin();
    }

    /// <summary>
    /// Determines if the player has won. if so, invokes the onWin event
    /// </summary>
    private void HandleWin()
    {
        if (enemiesLeft > 0) return;
        Instance.onWin.Invoke();
    }


    /// <summary>
    /// Instantiates the player and adds a listener for their death
    /// </summary>
    private void SpawnPlayer()
    {
        var temp = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        Player = temp.GetComponent<Player>();

        Player.Health.onDie.AddListener(HandlePlayerDeath);
    }

    /// <summary>
    /// Called from the onDie event on player.Health. This removes the listener and subtracts lives determining
    /// if the game should switch to a game over state of respawn the player
    /// </summary>
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
            Instance.onLose.Invoke();
            var newSource = gameObject.AddComponent<AudioSource>();
            newSource.outputAudioMixerGroup = fXMixer;
            newSource.PlayOneShot(gameOverSound);
        }
    }

    /// <summary>
    /// Called from inputManager when the pause key is pressed. Sets bool pause to true, stops timescale,
    /// and invokes the onPause event
    /// </summary>
    public static void Pause()
    {
        Paused = true;
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        Instance.onPause.Invoke();
    }

    /// <summary>
    /// Called from either a ui button click or the press of the pause key when the game is in a pause state.
    /// Sets bool pause to false, reverts timescale, and invokes onResume function
    /// </summary>
    public static void Resume()
    {
        Paused = false;
        Time.timeScale = originalTimeScale;
        Instance.onResume.Invoke();
    }
}