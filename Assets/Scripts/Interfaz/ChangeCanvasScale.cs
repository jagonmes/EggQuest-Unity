using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ChangeCanvasScale : MonoBehaviour
{
    [SerializeField] private CanvasScaler canvasScaler;
    private int screenWidth;
    private int screenHeight;
    public bool isMobile = false;
    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        changeCanvasScale();
    }

    void Update()
    {
        if (Screen.width != screenWidth || Screen.height != screenHeight)
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;
            changeCanvasScale();
        }
    }

    private void changeCanvasScale()
    {
        if (Application.isMobilePlatform || isMobile)
        {
            canvasScaler.matchWidthOrHeight = 0;
        }
        else
        {
            if(Mathf.Approximately(Screen.width / (float)Screen.height, 4f/3f))
                canvasScaler.matchWidthOrHeight = 0.915f;
            else if((Screen.height <= 1080 &&  Screen.height % 720f == 0) || Screen.height % 1440f == 0)
                canvasScaler.matchWidthOrHeight = 1;
            else
                canvasScaler.matchWidthOrHeight = 0.82f;
        }
    }

}
