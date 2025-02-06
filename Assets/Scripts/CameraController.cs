using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public CinemachineFreeLook cinemachineCamera;
    public float recenterDelay = 2f;
    private float timeSinceLastInput = 0f;
    private bool isRecentering = false;

    private Vector2 cameraInput;

    void Start()
    {
        if (!cinemachineCamera) cinemachineCamera = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        if (cameraInput.sqrMagnitude > 0.01f)
        {
            timeSinceLastInput = 0f;
            isRecentering = false;
        }
        else
        {
            timeSinceLastInput += Time.deltaTime;
        }

        if (timeSinceLastInput >= recenterDelay && !isRecentering)
        {
            StartCoroutine(RecenterCamera());
        }
    }

    IEnumerator RecenterCamera()
    {
        isRecentering = true;
        cinemachineCamera.m_RecenterToTargetHeading.m_enabled = true;
        yield return new WaitForSeconds(1f);
        cinemachineCamera.m_RecenterToTargetHeading.m_enabled = false;
        isRecentering = false;
    }

    public void OnCameraMove(InputAction.CallbackContext context)
    {
        cameraInput = context.ReadValue<Vector2>();

        cinemachineCamera.m_XAxis.Value += cameraInput.x * Time.deltaTime * 200f;
        cinemachineCamera.m_YAxis.Value += cameraInput.y * Time.deltaTime * 2f;
    }
}