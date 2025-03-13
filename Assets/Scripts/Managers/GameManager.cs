using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    public int score;
    public int feverTimeScore = 1;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            return _instance;
        }
    }

    Player _player;
    public Player player { get => _player; set => _player = value; }

    public int Score { get; private set; } //아이템 획득시 점수


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
            Destroy(gameObject);
    }

    public void AddScore(int score)
    {
        Score += score * feverTimeScore; //추후 다양한 변수를 곱해서 점수 관련 아이템 효과 기능구현 가능
    }
}
