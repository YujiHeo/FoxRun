using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
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
}
