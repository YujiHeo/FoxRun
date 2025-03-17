using System;
using Unity.VisualScripting;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;
    public Material dayMat;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;
    public Material nightMat;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    [Header("Spoot Lighting")]
    public Light spoot;

    public Color winterFog;
    private float skyRotation;
    private EnviromentSetting enviromentSetting;

    private void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
        enviromentSetting = GetComponentInParent<EnviromentSetting>();
    }

    private void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        skyRotation = time * 360.0f;
        RenderSettings.skybox.SetFloat("_Rotation", skyRotation);

        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);

        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4.0f;
        lightSource.color = colorGradiant.Evaluate(time);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject;
        if (lightSource.intensity == 0 && go.activeInHierarchy)
        {
            go.SetActive(false);

        }
        else if (lightSource.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
            DayORNight(lightSource);
            if (enviromentSetting.setWinter && lightSource == sun)
            {
                RenderSettings.fogColor = winterFog;
                return;
            }

            RenderSettings.fogColor = lightSource.color;
        }

    }

    void DayORNight(Light lightSource)
    {
        if(lightSource == sun)
        {
            spoot.gameObject.SetActive(false);
            RenderSettings.skybox = dayMat;
        }
        else
        {
            spoot.gameObject.SetActive(true);
            RenderSettings.skybox = nightMat;
        }
    }
}