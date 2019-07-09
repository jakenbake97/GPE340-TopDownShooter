using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField, Tooltip("A reference to the health bar for the player's health")]
    private HealthBar playerHealthBar;

    [SerializeField, Tooltip("The prefab for enemy health bars")]
    private GameObject enemyHealthBarPrefab;

    [SerializeField, Tooltip("Transform that the instantiated health bars should be stored under")]
    private Transform enemyHealthBarContainer;

    [SerializeField, Tooltip("The image object to be updated with weapon icons")]
    private Image weaponDisplay;

    [SerializeField, Tooltip("The text object to display the number of lives the player has")]
    private Text livesText;

    [SerializeField, Tooltip("The text object to display the number of enemies left")]
    private Text enemiesLeftText;

    public Camera uICamera;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Called when the player is spawned, this sets the health bar target to the player
    /// </summary>
    public void RegisterPlayer(Player player)
    {
        playerHealthBar.SetTarget(player.Health);
    }

    /// <summary>
    /// Called when an enemy is spawned, this sets the health bar target to the enemy
    /// </summary>
    public void RegisterEnemy(Enemy enemy)
    {
        var temp = Instantiate(enemyHealthBarPrefab);
        HealthBar healthBar = temp.GetComponent<HealthBar>();
        healthBar.transform.SetParent(enemyHealthBarContainer, false);
        healthBar.SetTarget(enemy.Health);
    }

    private void Update()
    {
        if (enemiesLeftText)
        {
            enemiesLeftText.text = $"Enemies Left: {GameManager.Instance.enemiesLeft}";
        }

        if (!GameManager.Player) return;
        if (GameManager.Player.currentWeapon)
        {
            weaponDisplay.overrideSprite = GameManager.Player.currentWeapon.Icon;
        }

        livesText.text = $"Lives: {GameManager.Lives}";
    }

    /// <summary>
    /// On click event that resumes the game
    /// </summary>
    public void ButtonResume()
    {
        GameManager.Resume();
    }

    /// <summary>
    /// on click event that quits the game
    /// </summary>
    public void ButtonQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    /// <summary>
    /// on click event that refreshes the current scene
    /// </summary>
    public void ButtonRetry()
    {
        SceneManager.LoadScene("Main Scene");
    }

    /// <summary>
    /// on click event that loads the main game scene
    /// </summary>
    public void ButtonStart()
    {
        SceneManager.LoadScene("Main Scene");
    }

    /// <summary>
    /// on click event that loads the menu scene
    /// </summary>
    public void ButtonMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}