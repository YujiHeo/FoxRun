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

    public AchivData[] achivs;

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
    }

    /// <summary>
    /// 지정 업적의 달성도를 갱신합니다. 갱신할 타이밍에 해당 함수를 호출해주면 됩니다.
    /// </summary>
    /// <param name="idx">갱신할 업적의 번호입니다.</param>
    /// <param name="addCount">갱신할 수치입니다. 기본값은 1입니다.</param>
    public void SignAchivement(int idx, int addCount = 1)
    {
        achivs[idx].UpdateAchiv(addCount);
    }

#if UNITY_EDITOR
    public void OnGUI()
    {
        if (GUILayout.Button("업적\n전체\n초기화", GUILayout.Width(100), GUILayout.Height(100)))
        {
            foreach (var achiv in achivs)
                achiv.ResetAchive();
        }
    }
#endif
}
