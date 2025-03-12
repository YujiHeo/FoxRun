using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using System.Collections;

public enum UIState
{
    Title, //0
    Lobby, //1
    Game, //2
}

public enum CharacterState
{
    Min = -1,

    Character1, //0
    Character2, //1

    Max //enum CharacterState의 개수
}

public class UIManager : MonoBehaviour
{
    UIState currentState = UIState.Title;

    TitleUI titleUI = null;
    LobbyUI lobbyUI = null;
    GameUI gameUI = null;

    public bool uISceneCameraPlay = false;

    static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (null == _instance)
            {
                return null;
            }
            return _instance;
        }
    }

    private void Awake()
    {


        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (_instance != this)
                Destroy(this.gameObject);
        }


        titleUI = GetComponentInChildren<TitleUI>(true);
        titleUI?.Init(this);
        lobbyUI = GetComponentInChildren<LobbyUI>(true);
        lobbyUI?.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);

        ChangeState(UIState.Title);
    }


    public void ChangeState(UIState state) //UI오브젝트를 on off 해주는 기능
    {
        currentState = state; //아래에서 해당하는 UI오브젝트를 찾아 on off 해줌
        titleUI?.SetActive(currentState);
        lobbyUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
    }


    //Title 내부

    public void OnClickStart() //시작하기를 누른경우
    {
        ChangeState(UIState.Lobby);

        uISceneCameraPlay = true;

        //if (cameraAnimator != null)
        //{
        //    cameraAnimator.SetBool("play", true);
        //}
    }

    public void OnClickExit() //게임 종료를 누른 경우
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    //Lobby 내부

    public void OnClickPlay() //게임 플레이 버튼을 누른 경우
    {
        ChangeState(UIState.Game); //타이틀Ui로 복귀

        SceneManager.LoadScene("Test_KGS");

    }

    public void OnClickPrev() //타이틀로 돌아가기를 누른 경우
    {
        ChangeState(UIState.Title); //GameUI 실행

        uISceneCameraPlay = false;

        //if (cameraAnimator != null)
        //{
        //    cameraAnimator.SetBool("play", false);
        //}
    }

    //Game 내부

    public void OnClickLobby()
    {
        ChangeState(UIState.Lobby);
        SceneManager.LoadScene("Test_KYH");
    }

}
