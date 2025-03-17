using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapEffect : MonoBehaviour
{
    public GameObject[] mapEffects;

    // Update is called once per frame
    void Update()
    {
        ResourceName currentSeason = MapManager.Instance.mapControllerTest.treeObjects.resourceName;
        SetActiveEffect(currentSeason);
    }

    private void SetActiveEffect(ResourceName season)
    {
        // 모든 이펙트 비활성화
        foreach (GameObject effect in mapEffects)
        {
            effect.SetActive(false);
        }

        // 특정 계절 이펙트만 활성화하고 파티클 실행
        switch (season)
        {
            case ResourceName.Spring:
                ActivateEffect(mapEffects[0]);
                break;
            case ResourceName.Summer:
                ActivateEffect(mapEffects[1]);
                break;
            case ResourceName.Fall:
                ActivateEffect(mapEffects[2]);
                break;
            case ResourceName.Winter:
                ActivateEffect(mapEffects[3]);
                break;
        }
    }

    private void ActivateEffect(GameObject effect)
    {
        effect.SetActive(true);
        ParticleSystem[] particles = effect.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particles)
        {
            ps.Play();
        }
    }

}
