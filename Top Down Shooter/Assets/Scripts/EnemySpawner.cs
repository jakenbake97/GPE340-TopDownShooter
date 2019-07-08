using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyUnits;
    [SerializeField] private int enemyCount;
    [SerializeField] private int maxActiveEnemies;
    [SerializeField] private float spawnDelay;
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
    }
}