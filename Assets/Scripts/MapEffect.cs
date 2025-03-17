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
        // ��� ����Ʈ ��Ȱ��ȭ
        foreach (GameObject effect in mapEffects)
        {
            effect.SetActive(false);
        }

        // Ư�� ���� ����Ʈ�� Ȱ��ȭ�ϰ� ��ƼŬ ����
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
