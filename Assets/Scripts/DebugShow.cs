using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.Windows;

public class DebugShow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpsText;
    float fpsNumber;
    private StarterAssetsInputs _input;
    private bool _developerModeActive = false;

    private void Start()
    {
        fpsText.gameObject.SetActive(false);
        Application.targetFrameRate = 60;
        _input = FindObjectOfType<StarterAssetsInputs>();
    }
    void Update()
    {
        if (_input.developerMode)
        {
            _developerModeActive = true;
        }
        else
        {
            _developerModeActive = false;
        }

        if (_developerModeActive)
        {
            fpsText.gameObject.SetActive(true);
            fpsNumber = 1f / Time.deltaTime;
            fpsText.text = "FPS: " + Mathf.RoundToInt(fpsNumber);
        }
        else
        {
            fpsText.gameObject.SetActive(false);
        }
    }
}