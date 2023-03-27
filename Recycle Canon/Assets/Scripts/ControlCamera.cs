using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControlCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    public CinemachineCameraOffset cameraOffset;

    public float orthoSyzeDefault;
    public float orthoSyzeType1;
    public float orthoSyzeType2;
    public float orthoSyzeType3;

    Vector2 resolution0 = new Vector2(1280, 720);
    Vector2 resolution1 = new Vector2(1920, 1080);
    Vector2 resolution2 = new Vector2(2560, 1080);

    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        Screen.orientation = ScreenOrientation.Portrait;

        if (Camera.main.aspect >= 0.5625f) // altura da tela é maior que a largura (9:16)
        {
            //Debug.Log("9:16");
            cameraOffset.enabled = false;
            virtualCamera.m_Lens.OrthographicSize = orthoSyzeDefault;
        }
        else if (Camera.main.aspect >= 0.5f) // altura da tela é maior que a largura (10:16)
        {
            cameraOffset.enabled = true;
            cameraOffset.m_Offset.y = 2;
            virtualCamera.m_Lens.OrthographicSize = orthoSyzeType1;
        }
        else if (Camera.main.aspect >= 0.666f) // altura da tela é maior que a largura (2:3)
        {
            //Debug.Log("2:3");
            cameraOffset.enabled = false;
            virtualCamera.m_Lens.OrthographicSize = orthoSyzeType2;
        }
        else if (Camera.main.aspect <= 0.75f) // altura da tela é maior que a largura (3:4)
        {
            //Debug.Log("9:18");
            cameraOffset.enabled = true;
            cameraOffset.m_Offset.y = 3;
            virtualCamera.m_Lens.OrthographicSize = orthoSyzeType3;
        }
        else // altura da tela é menor que a largura (1:2)
        {
            //Debug.Log("1:2");
            cameraOffset.enabled = false;
            virtualCamera.m_Lens.OrthographicSize = orthoSyzeType3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Screen.height == 1280 && Screen.width == 720 ||Screen.height == 1920 && Screen.width == 1080||Screen.height == 2560 && Screen.width == 1080)  
        //{virtualCamera.m_Lens.OrthographicSize = orthoSyzeDefault;}

        //if (Screen.height == 1280 && Screen.width == 720 || Screen.height == 1920 && Screen.width == 1080 || Screen.height == 2560 && Screen.width == 1080)
        //{ virtualCamera.m_Lens.OrthographicSize = orthoSyzeDefault; }
    }
}
