 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public GameObject player;
    private WaypointPatrol waypointPatrol;
    bool m_IsPlayerInRange;

    private void Start()
    {
        waypointPatrol = GetComponentInParent<WaypointPatrol>();
    }

    void OnTriggerEnter (Collider other)
    {
        print(other);
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            waypointPatrol.onPatrol = false;
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update ()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.transform.position - this.transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.gameObject.CompareTag("Player"))
                {
                    print("Caught");
                }
                else
                {
                    waypointPatrol.onPatrol = true;
                }
            }
        }
    }
}