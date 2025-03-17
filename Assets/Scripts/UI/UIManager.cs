using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using System.Collections;
using UnityEngine.UI;

public enum UIState
{
    Title, //0
    Lobby, //1
    Game, //2
    Pause, //3
    GameOver, //4
    Setting, //5
    Achievement, //6
    Customize //7
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
    UIState prevState = UIState.Lobby;

    TitleUI titleUI = null;
    LobbyUI lobbyUI = null;
    GameUI gameUI = null;
    PauseUI pauseUI = null;
    GameOverUI gameOverUI = null;
    SettingUI settingUI = null;
    AchievementUI achievementUI = null;
    AchievementPopUpUI achievementPopUpUI = null;
    CustomizeUI customizeUI = null;

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
        pauseUI = GetComponentInChildren<PauseUI>(true);
        pauseUI?.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI?.Init(this);
        settingUI = GetComponentInChildren<SettingUI>(true);
        settingUI?.Init(this);
        achievementUI = GetComponentInChildren<AchievementUI>(true);
        achievementUI?.Init(this);
        achievementPopUpUI = GetComponentInChildren<AchievementPopUpUI>(true);
        achievementPopUpUI?.Init(this);
        customizeUI = GetComponentInChildren<CustomizeUI>(true);
        customizeUI?.Init(this);

        ChangeState(UIState.Title);

    }


    public void ChangeState(UIState state) //UI오브젝트를 on off 해주는 기능
    {
        currentState = state; //아래에서 해당하는 UI오브젝트를 찾아 on off 해줌
        titleUI?.SetActive(currentState);
        lobbyUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        pauseUI?.SetActive(currentState);
        gameOverUI?.SetActive(currentState);
        settingUI?.SetActive(currentState);
        achievementUI?.SetActive(currentState);
        customizeUI?.SetActive(currentState);
    }


    public void AchievementClear(string _description)
    {
        achievementPopUpUI.AchievementCleared(_description);
    }


    //Title 내부

    public void OnClickStart() //시작하기를 누른경우
    {
        ChangeState(UIState.Lobby);

        uISceneCameraPlay = true;
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

    public void OnClickSetting()
    {
        prevState = currentState; //이전 ui가 뭐였는지 기억해줌
        settingUI.bgmSlider.value = SoundManager.Instance.bgmVolume; //초기 볼륨에 맞게 ui 수정
        settingUI.sfxSlider.value = SoundManager.Instance.sfxVolume;

        if(SoundManager.Instance.isBGMMute == true) // 브금 뮤트가 켜진 경우
        {
            settingUI.bgmMuteButton.GetComponent<Image>().color = Color.gray;
        }else if(SoundManager.Instance.isBGMMute == false) //브금 뮤트가 꺼진 경우
        {
            settingUI.bgmMuteButton.GetComponent<Image>().color = Color.white;
        }

        if(SoundManager.Instance.isSFXMute == true) //효과음 뮤트가 켜진 경우
        {
            settingUI.sfxMuteButton.GetComponent<Image>().color = Color.gray;
        }
        else if (SoundManager.Instance.isSFXMute == false) //효과음 뮤트가 꺼진 경우
        {
            settingUI.sfxMuteButton.GetComponent<Image>().color = Color.white;
        }

        ChangeState(UIState.Setting);
    }

    public void PlayUIClickAudio()
    {
        SoundManager.Instance.PlaySFX("Abstract1", transform.position);
    }

    //Lobby 내부

    public void OnClickPlay() //게임 플레이 버튼을 누른 경우
    {
        ChangeState(UIState.Game); //타이틀Ui로 복귀

        SceneManager.LoadScene("YGM_Maptwo");

        AchivementManager.instance.SignAchivement(00); //첫플레이 도전과제 달성용

    }

    public void OnClickPrev() //타이틀로 돌아가기를 누른 경우
    {
        ChangeState(UIState.Title); //GameUI 실행

        uISceneCameraPlay = false; //타이틀씬 카메라 경로 수정용
    }

    public void OnClickAchievement()
    {
        achievementUI.UpdateAchievements();
        ChangeState(UIState.Achievement);
    }

    public void OnClickCustomize()
    {
        ChangeState(UIState.Customize);
    }

    //Game 내부
    
    public void OnClickPause()
    {
        Time.timeScale = 0; //일시정지용으로 시간 멈춤
        ChangeState(UIState.Pause);
    }

    //Pause 내부

    public void OnClickBack()
    {
        Time.timeScale = 1;
        ChangeState(UIState.Game);
    }

    public void OnClickLobby()
    {
        Time.timeScale = 1;
        ChangeState(UIState.Lobby);
        SceneManager.LoadScene("Test_KYH");
    }

    //Setting 내부

    public void OnClickSettingBack()
    {
        ChangeState(prevState);
    }

    //GameOver 내부
    public void PlayerDeadUI()
    {
        gameOverUI.SetScoreText();
        ChangeState(UIState.GameOver);
    }

    public void OnClickGameOverLobby()
    {
        ChangeState(UIState.Lobby);
        SceneManager.LoadScene("Test_KYH");
    }

    //Achievement 내부

    public void OnClickAchievementLobby()
    {
        ChangeState(UIState.Lobby);
    }

    //Customize내부

    public void OnClickCustomizeLobby()
    {
        ChangeState(UIState.Lobby);
    }

}
