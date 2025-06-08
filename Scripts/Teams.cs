using UnityEngine;
using System.Collections.Generic;

public class Teams : MonoBehaviour
{
    [Header("������� ������ � ���������")]
    public GameObject enemyPrefab;
    public GameObject allyPrefab;

    [Header("�����-������ ������")]
    public Transform[] enemySpawnPoints;

    [Header("�����-������ ���������")]
    public Transform[] allySpawnPoints;

    public FollowAI followAI; // ������ �� ��� FollowAI

    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> allies = new List<GameObject>();

    void Start()
    {
        // ������� 8 ������
        for (int i = 0; i < 8; i++)
        {
            SpawnEnemy(i);
        }

        // ������� 8 ���������
        for (int i = 0; i < 8; i++)
        {
            SpawnAlly(i);
        }
    }

    void SpawnEnemy(int index)
    {
        if (enemySpawnPoints.Length == 0 || enemyPrefab == null)
            return;

        Transform spawnPoint = enemySpawnPoints[index % enemySpawnPoints.Length];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // ��������� � ������
        enemies.Add(enemy);

        // �������� � FollowAI
        if (followAI != null)
        {
            followAI.AddOpponent(enemy);
        }

        EnemyHealth ec = enemy.GetComponent<EnemyHealth>();
        if (ec != null)
        {
            ec.SetSpawner(this);
            ec.SetSpawnPoint(spawnPoint.position);
            ec.OnDeath += () => RespawnEnemy(enemy, spawnPoint.position);
        }
    }

    void SpawnAlly(int index)
    {
        if (allySpawnPoints.Length == 0 || allyPrefab == null)
            return;

        Transform spawnPoint = allySpawnPoints[index % allySpawnPoints.Length];
        GameObject ally = Instantiate(allyPrefab, spawnPoint.position, spawnPoint.rotation);

        allies.Add(ally);
    }

    void RespawnEnemy(GameObject enemy, Vector3 spawnPosition)
    {
        enemies.Remove(enemy);
        Destroy(enemy);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        enemies.Add(newEnemy);

        if (followAI != null)
        {
            followAI.AddOpponent(newEnemy);
        }

        EnemyHealth ec = newEnemy.GetComponent<EnemyHealth>();
        if (ec != null)
        {
            ec.SetSpawner(this);
            ec.SetSpawnPoint(spawnPosition);
            ec.OnDeath += () => RespawnEnemy(newEnemy, spawnPosition);
        }
    }
}