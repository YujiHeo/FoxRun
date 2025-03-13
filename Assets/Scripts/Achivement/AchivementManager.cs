using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    [SerializeField] AchivData[] achivs;

    void Awake()
    {
        achivs = Resources.LoadAll<AchivData>("Achivements");
    }
}
