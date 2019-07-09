using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Tooltip("array of enemy prefabs to spawn from")]
    private GameObject[] enemyUnits;

    public int enemyCount;

    [SerializeField, Tooltip("The maximum number of enemies that can be alive at one time")]
    private int maxActiveEnemies;

    [SerializeField, Tooltip("The amount of time in seconds to wait before spawning the next enemy")]
    private float spawnDelay;

    private int currentActiveEnemies;
    private int numberSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnDelay);
    }

    /// <summary>
    /// Instantiates a random enemy at a random spawn point
    /// </summary>
    private void SpawnEnemy()
    {
        if (currentActiveEnemies >= maxActiveEnemies) return;

        int randomUnit = Random.Range(0, enemyUnits.Length);
        int randomSpawn = Random.Range(0, transform.childCount);
        var enemyObject = Instantiate(enemyUnits[randomUnit], transform.GetChild(randomSpawn).position,
            Quaternion.identity);
        Enemy enemyScript = enemyObject.GetComponent<Enemy>();
        currentActiveEnemies++;
        numberSpawned++;
        enemyScript.Health.onDie.AddListener(HandleEnemyDeath);
    }

    private void Update()
    {
        if (GameManager.Paused) return;
        if (numberSpawned >= enemyCount)
        {
            CancelInvoke(nameof(SpawnEnemy));
        }
    }

    /// <summary>
    /// removes the dead enemy from the currently active count
    /// </summary>
    private void HandleEnemyDeath()
    {
        currentActiveEnemies--;
        GameManager.Instance.enemiesLeft--;
    }
}