using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    static AchivementManager _instance;
    public static AchivementManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("AchivementManager").AddComponent<AchivementManager>();
            return _instance;
        }
    }

    [SerializeField] AchivData[] achivs;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        achivs = Resources.LoadAll<AchivData>("Achivements");

#if UNITY_EDITOR
        foreach (var achiv in achivs)
            achiv.ResetAchive();
#endif
    }

    /// <summary>
    /// ���� ������ �޼����� �����մϴ�. ������ Ÿ�ֿ̹� �ش� �Լ��� ȣ�����ָ� �˴ϴ�.
    /// </summary>
    /// <param name="idx">������ ������ ��ȣ�Դϴ�.</param>
    /// <param name="addCount">������ ��ġ�Դϴ�. �⺻���� 1�Դϴ�.</param>
    public void SignAchivement(int idx, int addCount = 1)
    {
        achivs[idx].UpdateAchiv(addCount);
    }
}
