using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapEffect : MonoBehaviour
{
    public ParticleSystem[] mapEffects;
    private ResourceName currentSeason;

    // Update is called once per frame
    void Update()
    {
        currentSeason = MapManager.Instance.mapControllerTest.treeObjects.resourceName;
        SetActiveEffect(currentSeason);

        
    }

    private void SetActiveEffect(ResourceName season)
    {

        if (season == ResourceName.Spring)
        {
            mapEffects[0].Play();
        }
        else
        {
            mapEffects[0].Stop();
        }

    }


}
