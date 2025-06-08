using UnityEngine;
using System;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject _bloodPrefab;

    public float maxHealth = 100f;
    public event Action OnDeath;

    private Teams spawner;
    private float currentHealth;
    private Vector3 spawnPoint;
    private AddKillUI _addKillUI;

    void Start()
    {
        currentHealth = maxHealth;
        _addKillUI = FindAnyObjectByType<AddKillUI>();
    }

    public void SetSpawner(Teams spawner)
    {
        this.spawner = spawner;
    }

    public void SetSpawnPoint(Vector3 point)
    {
        spawnPoint = point;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            _addKillUI.AddNewElement();
            Die();
        }
    }

    void Die()
    {
        OnDeath?.Invoke();
        Destroy(Instantiate(_bloodPrefab, transform.position, transform.rotation), 3);
        Destroy(gameObject);
    }
}