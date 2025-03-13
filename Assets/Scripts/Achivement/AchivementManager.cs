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
            achiv.currnetCount = 0;
#endif
    }

    public void SignAchivement(int idx, int addCount)
    {
        achivs[idx].UpdateAchiv(addCount);
    }
}
