using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achivement", menuName = "AchiveData")]
public class AchivData : ScriptableObject
{
    [SerializeField] string achivName; //업적의 이름입니다.
    [SerializeField] string description; //업적의 목표등을 설명하는 부분입니다.
    public Sprite icon; //업적의 아이콘입니다.
    [SerializeField] int goalCount; //업적의 목표수치입니다.
    [SerializeField] int currnetCount; //업적의 달성률입니다.

    [SerializeField] int level; //업적을 달성한 횟수입니다.
    [SerializeField] int addGoalPerLv; //업적을 달성할 때 증가할 목표치입니다.

    public bool isClear => CurrentCount >= GoalCount;
    public string Name => string.Format(achivName, level + 1);
    public string Description => string.Format(description, GoalCount);
    public int GoalCount => goalCount + level * addGoalPerLv;
    public int CurrentCount => currnetCount;

    /// <summary>
    /// 지정된 수치만큼 달성도를 올립니다. 목표수치에 도달하면 업적을 달성시킵니다.
    /// </summary>
    /// <param name="addCount"></param>
    public void UpdateAchiv(int addCount)
    {
        if (isClear) return;

        currnetCount += addCount;
        if (isClear)
        {
            Debug.Log(Name + "달성: " + Description);
            //UIManager.instance.AchievementClear(Name); //나중에 활성화해줄것
            level++;
        }
    }

    /// <summary>
    /// 달성도를 0으로 합니다.
    /// </summary>
    public void ResetAchive()
    {
        level = 0;
        currnetCount = 0;
    }
}
