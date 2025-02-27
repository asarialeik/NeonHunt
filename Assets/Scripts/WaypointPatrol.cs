using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public bool onPatrol = true;

    int m_CurrentWaypointIndex;

    void Start ()
    {
        navMeshAgent.SetDestination (waypoints[0].position);
        navMeshAgent.speed = 2;
        GameManager.Instance.Register(gameObject);
    }

    void Update ()
    {
        if (onPatrol == true)
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            navMeshAgent.speed = 2;
        }
        else
        {
            navMeshAgent.speed = 0;
        }
    }

    public void DisabledEnemy()
    {
        GameManager.Instance.currentScore += 2;
        GameManager.Instance.Unregister();
        float prob = Random.Range(0f, 1f);
        if (prob <= EnergySystem.Instance.maxEnergyChance && prob > EnergySystem.Instance.maxEnergyChance + EnergySystem.Instance.instantRecoveryChance)
        {
            GameObject prefab = EnergySystem.Instance.GetPowerUp1FromPool();
            prefab.SetActive(true);
            prefab.transform.position = this.gameObject.transform.position;
        }
        else if (prob <= EnergySystem.Instance.maxEnergyChance + EnergySystem.Instance.instantRecoveryChance)
        {
            GameObject prefab = EnergySystem.Instance.GetPowerUp2FromPool();
            prefab.SetActive(true);
            prefab.transform.position = this.gameObject.transform.position;
        }
    }
}
