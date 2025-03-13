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
    /// ������ ��ġ��ŭ �޼����� �ø��ϴ�. ��ǥ��ġ�� �����ϸ� ������ �޼���ŵ�ϴ�.
    /// </summary>
    /// <param name="addCount"></param>
    public void UpdateAchiv(int addCount)
    {
        if (isClear) return;

        currnetCount += addCount;
        if (isClear)
            Debug.Log(achivName + "�޼�: " + Description);
    }

    /// <summary>
    /// �޼����� 0���� �մϴ�.
    /// </summary>
    public void ResetAchive()
    {
        currnetCount = 0;
    }
}
