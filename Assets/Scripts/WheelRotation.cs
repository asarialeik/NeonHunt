using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    float rotationSpeed = 45;
    Vector3 currentEulerAngles;
    float x;
    float y;
    float z;
    public void OnMovement(float speed)
    {
        x = 0;
        if (speed < 7)
        {
            y = 22.5f - y;
        }
        else
        {
            y = 45 - y;
        }
        z = 0;
        currentEulerAngles += new Vector3(x, y, z) * Time.deltaTime * rotationSpeed;
        transform.localEulerAngles = currentEulerAngles;
    }
}
