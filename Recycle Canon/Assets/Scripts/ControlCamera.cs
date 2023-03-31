using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControlCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    public CinemachineCameraOffset cameraOffset;

    [SerializeField] private float orthoSyzeDefault;
    [SerializeField] private float orthoSyzeType1;
    [SerializeField] private float orthoSyzeType2;
    [SerializeField] private float orthoSyzeType3;
    //[SerializeField] private float cameraOffsetValue;

    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        Screen.orientation = ScreenOrientation.Portrait;

        if (Camera.main.aspect >= 0.5625f) 
        {
            cameraOffset.enabled = false;
            virtualCamera.m_Lens.OrthographicSize = orthoSyzeDefault;          
            Debug.Log("Res: " + Camera.main.scaledPixelWidth + "x" + Camera.main.scaledPixelHeight + " | AspectRatio: " + Camera.main.aspect);
        }
        else if (Camera.main.aspect >= 0.5f) 
        { 
            cameraOffset.enabled = true;
            cameraOffset.m_Offset.y = 2;
            virtualCamera.m_Lens.OrthographicSize = orthoSyzeType1;        
            Debug.Log("Res: " + Camera.main.scaledPixelWidth + "x" + Camera.main.scaledPixelHeight + " | AspectRatio: " + Camera.main.aspect);
        }
        else if (Camera.main.aspect >= 0.666f) 
        {
            cameraOffset.enabled = false;
            virtualCamera.m_Lens.OrthographicSize = orthoSyzeType2;
            Debug.Log("Res: " + Camera.main.scaledPixelWidth + "x" + Camera.main.scaledPixelHeight + " | AspectRatio: " + Camera.main.aspect);
        }
        else if (Camera.main.aspect <= 0.75f) 
        {  
            cameraOffset.enabled = true;
            cameraOffset.m_Offset.y = 3;
            virtualCamera.m_Lens.OrthographicSize = orthoSyzeType3;
            Debug.Log("Res: " + Camera.main.scaledPixelWidth + "x" + Camera.main.scaledPixelHeight + " | AspectRatio: " + Camera.main.aspect);
        }
        else 
        {
            cameraOffset.enabled = false;
            virtualCamera.m_Lens.OrthographicSize = orthoSyzeType3;
            Debug.Log("Res: " + Camera.main.scaledPixelWidth + "x" + Camera.main.scaledPixelHeight + " | AspectRatio: " + Camera.main.aspect);
        }
    }
}
