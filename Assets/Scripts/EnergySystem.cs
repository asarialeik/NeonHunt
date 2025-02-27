using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public static EnergySystem Instance { get; private set; }

    private float maxEnergy = 1;
    private float currentEnergy;
    //Amount needed to shoot
    private float shootingEnergy = 0.33f;
    private float rechargingTime = 10f;
    //Amount recovered each second
    private float energyRecovery = 0.03f;
    private float recoveryMinTime = 1f;
    [SerializeField]
    public bool canShoot;
    private bool hasShot;
    private float _TimeSinceLastShot;
    //Instant recovery powerup chance
    public float instantRecoveryChance = 0.1f;
    //Max energy powerup chance
    public float maxEnergyChance = 0.05f;
    private float maxEnergyTime = 30f;
    public bool maxEnergyActive;
    public bool instantRecoveryActive;
    [SerializeField]
    private GameObject maxEnergyPrefab;
    [SerializeField]
    private GameObject instantRecoveryPrefab;
    private List<GameObject> maxEnergyPowerupPool;
    private List<GameObject> instantEnergyRecoveryPowerupPool;
    private int maxEnergyPoolSize = 5;
    private int instantEnergyRecoveryPoolSize = 5;
    [SerializeField]
    private TMP_Text energyText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        maxEnergyPowerupPool = new List<GameObject>();
        for (int i = 0; i < maxEnergyPoolSize; i++)
        {
            GameObject prefab = Instantiate(maxEnergyPrefab);
            prefab.SetActive(false);
            prefab.name = "Powerup_Type1";
            maxEnergyPowerupPool.Add(prefab);
        }

        instantEnergyRecoveryPowerupPool = new List<GameObject>();
        for (int i = 0; i < instantEnergyRecoveryPoolSize; i++)
        {
            GameObject prefab = Instantiate(instantRecoveryPrefab);
            prefab.SetActive(false);
            prefab.name = "Powerup_Type2";
            instantEnergyRecoveryPowerupPool.Add(prefab);
        }
    }

    void Start()
    {
        currentEnergy = 1;
    }

    public void CanShoot()
    {
        if (currentEnergy >= shootingEnergy)
        {
            canShoot = true;
            hasShot = true;
        }
    }

    void Update()
    {
        if (currentEnergy >= maxEnergy)
        {
            currentEnergy = maxEnergy;
        }

        if (energyText != null)
        {
            energyText.text = ("Energy: " + (currentEnergy * 100).ToString("000") + "/" + (maxEnergy*100).ToString("000"));
        }

        if (maxEnergyActive == false && hasShot == false)
        {
            _TimeSinceLastShot += Time.deltaTime;
        }
        else if (hasShot == true)
        {
            _TimeSinceLastShot = 0f;
        }

        if (maxEnergyActive)
        {
            currentEnergy = maxEnergy;
        }
        hasShot = false;
        if (_TimeSinceLastShot >= rechargingTime)
        {
            StartCoroutine(EnergyRecharge());
        }
    }

    public void JustShot()
    {
        if (!maxEnergyActive)
        {
            currentEnergy -= shootingEnergy;
        }
    }

    IEnumerator EnergyRecharge()
    {
        yield return new WaitForSeconds(recoveryMinTime);
        currentEnergy += energyRecovery;
    }

    public IEnumerator MaxEnergyPowerupReset()
    {
        yield return new WaitForSeconds(maxEnergyTime);
        maxEnergyActive = false;
    }

    public GameObject GetPowerUp1FromPool()
    {
        foreach (GameObject prefab in maxEnergyPowerupPool)
        {
            if (!prefab.activeInHierarchy)
            {
                return prefab;
            }
        }
        return null;
    }

    public GameObject GetPowerUp2FromPool()
    {
        foreach (GameObject prefab in instantEnergyRecoveryPowerupPool)
        {
            if (!prefab.activeInHierarchy)
            {
                return prefab;
            }
        }
        return null;
    }
}
