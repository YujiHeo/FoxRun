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

    [SerializeField] int level;
    [SerializeField] int addGoalPerLv;

    public bool isClear => CurrentCount >= GoalCount;
    public string Name => string.Format(achivName, level + 1);
    public string Description => string.Format(description, GoalCount);
    public int GoalCount => goalCount + level * addGoalPerLv;
    public int CurrentCount => currnetCount;

    /// <summary>
    /// ������ ��ġ��ŭ �޼����� �ø��ϴ�. ��ǥ��ġ�� �����ϸ� ������ �޼���ŵ�ϴ�.
    /// </summary>
    /// <param name="addCount"></param>
    public void UpdateAchiv(int addCount)
    {
        if (isClear) return;

        currnetCount += addCount;
        if (isClear)
        {
            Debug.Log(Name + "�޼�: " + Description);
            level++;
        }
    }

    /// <summary>
    /// �޼����� 0���� �մϴ�.
    /// </summary>
    public void ResetAchive()
    {
        level = 0;
        currnetCount = 0;
    }
}
