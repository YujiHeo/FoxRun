using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
        SceneLoad("HMJ_Test");
    }

    private void SceneLoad(string sceneName)
    {
        if(Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(sceneName);
            SoundManager.Instance.PlayBGM(sceneName);
        }   
    } 
}
