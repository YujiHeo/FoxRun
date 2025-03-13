using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achivement", menuName = "AchiveData")]
public class AchivData : ScriptableObject
{
    [SerializeField] string achivName; //������ �̸��Դϴ�.
    [SerializeField] string description; //������ ��ǥ���� �����ϴ� �κ��Դϴ�.
    public Sprite icon; //������ �������Դϴ�.
    [SerializeField] int goalCount; //������ ��ǥ��ġ�Դϴ�.
    [SerializeField] int currnetCount; //������ �޼����Դϴ�.

    [SerializeField] int level; //������ �޼��� Ƚ���Դϴ�.
    [SerializeField] int addGoalPerLv; //������ �޼��� �� ������ ��ǥġ�Դϴ�.

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
