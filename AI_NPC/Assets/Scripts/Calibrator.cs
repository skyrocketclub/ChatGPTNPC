using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibrator : MonoBehaviour
{
    public Transform mainCamera;
    public Transform cameraOffset;

    private float targetHeight = 1.7406f;
    private float heightOffset = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Calibrate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Calibrate()
    {
        float currentHeight = mainCamera.position.y;
        heightOffset = currentHeight - targetHeight;
        Vector3 newPosition = cameraOffset.position;
        newPosition.y = cameraOffset.position.y - heightOffset;
        cameraOffset.position = newPosition;
    }
}
