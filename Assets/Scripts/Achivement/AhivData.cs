using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achivement", menuName = "AchiveData")]
public class AchivData : ScriptableObject
{
    [SerializeField] string achivName;
    [SerializeField] string description;
    public Sprite icon;
    [SerializeField] int goalCount;
    [SerializeField] int currnetCount;

    public bool isClear => currnetCount >= goalCount;
    public string Description => string.Format(description, goalCount);

    public void UpdateAchiv(int addCount)
    {
        if (isClear) return;

        currnetCount += addCount;
        if (isClear)
            Debug.Log(achivName + "´Þ¼º: " + Description);
    }

    public void ResetAchive()
    {
        currnetCount = 0;
    }
}
