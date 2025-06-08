using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _Xrange;
    [SerializeField] private float _Yrange;
    [SerializeField] private GameObject _enemyPrefab;

    private void Start()
    {
        StartCoroutine(SpawnerCoroutine());
    }

    private IEnumerator SpawnerCoroutine()
    {
        while (true)
        {
            EnemySpawn();
            yield return new WaitForSeconds(3);
        }
    }

    private void EnemySpawn()
    {
        Instantiate(_enemyPrefab, new Vector3(Random.Range(-_Xrange, _Xrange), 1, Random.Range(-_Yrange, _Yrange)), transform.rotation);
    }
}