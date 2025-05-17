using UnityEngine;
using Cinemachine;
using System;

public class CameraZoom : MonoBehaviour
{
   [SerializeField] private float defalutDistance =6f;
   [SerializeField] private float minDistance=1f;
   [SerializeField] private float maxDistance=6f;

   [SerializeField] private float smoothing=4f;
   [SerializeField] private float zoomSensitivity=1f;

   private CinemachineFramingTransposer framingTransposer;
   private CinemachineInputProvider inputProvider;

   private float currentDistance;
    void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider = GetComponent<CinemachineInputProvider>();

        currentDistance = defalutDistance;
    }

    void Update()
    {
        Zoom();
    }

    void Zoom(){
        float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;
        currentDistance = Math.Clamp(currentDistance + zoomValue, minDistance, maxDistance);

        float distance = framingTransposer.m_CameraDistance;
        if(distance == currentDistance){
            return;
        }


        float lerpedValue = Mathf.Lerp(distance, currentDistance, smoothing * Time.deltaTime);

        framingTransposer.m_CameraDistance = lerpedValue;
    }
}
