using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achivement", menuName = "AchiveData")]
public class AchivData : ScriptableObject
{
    public string achivName;
    public string description;
    public Sprite icon;
    public int goalCount;
    public int currnetCount;
    bool isClear => currnetCount >= goalCount;

    public void UpdateAchiv(int addCount)
    {
        if (isClear) return;

        currnetCount += addCount;
        if (isClear)
            Debug.Log(achivName + "´Þ¼º");
    }
}
