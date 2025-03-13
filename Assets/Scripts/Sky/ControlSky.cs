using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSky : MonoBehaviour
{
    public Material dayMat;
    public Material nightMat;
    public GameObject dayLight;
    public GameObject nightLight;


    public Color dayFog;
    public Color nightFog;

    public void SetDaySky()
    {
        RenderSettings.skybox = dayMat;
        RenderSettings.fogColor = dayFog;
    }

    public void SetNightSky()
    {
        RenderSettings.skybox = nightMat;
        RenderSettings.fogColor = nightFog;
    }
}
