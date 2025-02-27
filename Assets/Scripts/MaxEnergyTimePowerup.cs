using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxEnergyTimePowerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnergySystem.Instance.maxEnergyActive = true;
            StartCoroutine(EnergySystem.Instance.MaxEnergyPowerupReset());
            this.gameObject.SetActive(false);
        }
    }
}
