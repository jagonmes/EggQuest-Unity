using TMPro;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class FramerateLimiter : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 60;
    private double currentScreenFramerate;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            SetFramerate();
        }
    }

    private void Update()
    {
        
        if (Application.isPlaying)
        {
            if(currentScreenFramerate != Screen.currentResolution.refreshRateRatio.value)
                SetFramerate();
        }
        
    }

    private void SetFramerate()
    {
        if (Application.isMobilePlatform)
        {
            Application.targetFrameRate = targetFrameRate;
            QualitySettings.vSyncCount = 0;
        }else if((int)Screen.currentResolution.refreshRateRatio.value == targetFrameRate)
            QualitySettings.vSyncCount = 1;
        else if(Screen.currentResolution.refreshRateRatio.value % 1 == 0)
        {
            float framerateRatio =(float) (Screen.currentResolution.refreshRateRatio.value / targetFrameRate);
            if(framerateRatio % 1.0 == 0 && framerateRatio <= 4)
            {
                QualitySettings.vSyncCount = (int)framerateRatio;
                Application.targetFrameRate = -1;
            }
            else
            { 
                Application.targetFrameRate = targetFrameRate;
                QualitySettings.vSyncCount = 0;
            }
        }
        else
        {
            Application.targetFrameRate = targetFrameRate;
            QualitySettings.vSyncCount = 0;
        }
        currentScreenFramerate = Screen.currentResolution.refreshRateRatio.value;
    }


}
