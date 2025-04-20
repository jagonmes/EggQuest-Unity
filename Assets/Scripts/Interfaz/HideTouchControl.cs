using System;
using UnityEngine;

public class HideTouchControl : MonoBehaviour
{
    [SerializeField] private GameObject touchControl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!Application.isEditor && !Application.isMobilePlatform)
        {
            touchControl.SetActive(false);
        }
    }
}
