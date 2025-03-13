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

    /// <summary>
    /// 지정된 수치만큼 달성도를 올립니다. 목표수치에 도달하면 업적을 달성시킵니다.
    /// </summary>
    /// <param name="addCount"></param>
    public void UpdateAchiv(int addCount)
    {
        if (isClear) return;

        currnetCount += addCount;
        if (isClear)
            Debug.Log(achivName + "달성: " + Description);
    }

    /// <summary>
    /// 달성도를 0으로 합니다.
    /// </summary>
    public void ResetAchive()
    {
        currnetCount = 0;
    }
}
