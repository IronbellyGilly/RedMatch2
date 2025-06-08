using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private NavMeshAgent _navMeshAgent;


    private void Start()
    {
        StartCoroutine(UpdateWay());
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private IEnumerator UpdateWay()
    {
        while (true)
        {
            ChangeWay();
            yield return new WaitForSeconds(Random.Range(1,3));
        }
    }

    private void ChangeWay()
    {
        _navMeshAgent.SetDestination(new Vector3(Random.Range(-50,50), 1, Random.Range(-50, 50)));
    }
}