using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private HealthBar playerHealthBar;
    [SerializeField] private GameObject enemyHealthBarPrefab;
    [SerializeField] private Transform enemyHealthBarContainer;
    [SerializeField] private Image weaponDisplay;
    [SerializeField] private Text livesText;
    [SerializeField] private Text enemiesLeftText;
    public Camera uICamera;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterPlayer(Player player)
    {
        playerHealthBar.SetTarget(player.Health);
    }

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

    public void ButtonResume()
    {
        GameManager.Resume();
    }

    public void ButtonQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ButtonRetry()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void ButtonStart()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void ButtonMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}