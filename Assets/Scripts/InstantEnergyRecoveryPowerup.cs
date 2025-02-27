using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantEnergyRecoveryPowerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnergySystem.Instance.instantRecoveryActive = true;
            EnergySystem.Instance.instantRecoveryActive = false;
            this.gameObject.SetActive(false);
        }
    }
}
