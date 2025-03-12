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

    Max //enum CharacterState�� ����
}

public class UIManager : MonoBehaviour
{
    UIState currentState = UIState.Title;

    TitleUI titleUI = null;
    LobbyUI lobbyUI = null;
    GameUI gameUI = null;

    public bool uISceneCameraPlay = false;

    static UIManager _instance;
    public static UIManager Instance
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


    public void ChangeState(UIState state) //UI������Ʈ�� on off ���ִ� ���
    {
        currentState = state; //�Ʒ����� �ش��ϴ� UI������Ʈ�� ã�� on off ����
        titleUI?.SetActive(currentState);
        lobbyUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
    }


    //Title ����

    public void OnClickStart() //�����ϱ⸦ �������
    {
        ChangeState(UIState.Lobby);

        uISceneCameraPlay = true;

        //if (cameraAnimator != null)
        //{
        //    cameraAnimator.SetBool("play", true);
        //}
    }

    public void OnClickExit() //���� ���Ḧ ���� ���
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }

    //Lobby ����

    public void OnClickPlay() //���� �÷��� ��ư�� ���� ���
    {
        ChangeState(UIState.Game); //Ÿ��ƲUi�� ����

        SceneManager.LoadScene("Test_KGS");

    }

    public void OnClickPrev() //Ÿ��Ʋ�� ���ư��⸦ ���� ���
    {
        ChangeState(UIState.Title); //GameUI ����

        uISceneCameraPlay = false;

        //if (cameraAnimator != null)
        //{
        //    cameraAnimator.SetBool("play", false);
        //}
    }

    //Game ����

    public void OnClickLobby()
    {
        ChangeState(UIState.Lobby);
        SceneManager.LoadScene("Test_KYH");
    }

}
