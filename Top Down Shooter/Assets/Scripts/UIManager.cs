using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private HealthBar playerHealthBar;
    [SerializeField] private GameObject enemyHealthBarPrefab;
    [SerializeField] private Transform enemyHealthBarContainer;

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
        healthBar.transform.SetParent(enemy.transform, false);
        healthBar.SetTarget(enemy.Health);
    }
}