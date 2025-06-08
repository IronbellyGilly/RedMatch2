using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class FollowAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<GameObject> opponents = new List<GameObject>();

    private GameObject currentTarget;

    private void Update()
    {
        FindClosestOpponent();
        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.transform.position);
        }
    }

    private void FindClosestOpponent()
    {
        float minDistance = Mathf.Infinity;
        GameObject closestOpponent = null;

        foreach (var opponent in opponents)
        {
            if (opponent == null) continue; // пропускаем уничтоженные объекты
            float distance = Vector3.Distance(transform.position, opponent.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestOpponent = opponent;
            }
        }

        currentTarget = closestOpponent;
    }

    public void AddOpponent(GameObject newOpponent)
    {
        if (!opponents.Contains(newOpponent))
        {
            opponents.Add(newOpponent);
        }
    }
}