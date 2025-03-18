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

    Max //enum CharacterState�� ����
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


    public void ChangeState(UIState state) //UI������Ʈ�� on off ���ִ� ���
    {
        currentState = state; //�Ʒ����� �ش��ϴ� UI������Ʈ�� ã�� on off ����
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


    //Title ����

    public void OnClickStart() //�����ϱ⸦ �������
    {
        ChangeState(UIState.Lobby);

        uISceneCameraPlay = true;
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

    public void OnClickSetting()
    {
        prevState = currentState; //���� ui�� �������� �������
        settingUI.bgmSlider.value = SoundManager.Instance.bgmVolume; //�ʱ� ������ �°� ui ����
        settingUI.sfxSlider.value = SoundManager.Instance.sfxVolume;

        if(SoundManager.Instance.isBGMMute == true) // ��� ��Ʈ�� ���� ���
        {
            settingUI.bgmMuteButton.GetComponent<Image>().color = Color.gray;
        }else if(SoundManager.Instance.isBGMMute == false) //��� ��Ʈ�� ���� ���
        {
            settingUI.bgmMuteButton.GetComponent<Image>().color = Color.white;
        }

        if(SoundManager.Instance.isSFXMute == true) //ȿ���� ��Ʈ�� ���� ���
        {
            settingUI.sfxMuteButton.GetComponent<Image>().color = Color.gray;
        }
        else if (SoundManager.Instance.isSFXMute == false) //ȿ���� ��Ʈ�� ���� ���
        {
            settingUI.sfxMuteButton.GetComponent<Image>().color = Color.white;
        }

        ChangeState(UIState.Setting);
    }

    public void PlayUIClickAudio()
    {
        SoundManager.Instance.PlaySFX("Abstract1", transform.position);
    }

    //Lobby ����

    public void OnClickPlay() //���� �÷��� ��ư�� ���� ���
    {
        ChangeState(UIState.Game); //Ÿ��ƲUi�� ����

        SceneManager.LoadScene("YGM_Maptwo");

        AchivementManager.instance.SignAchivement(00); //ù�÷��� �������� �޼���

    }

    public void OnClickPrev() //Ÿ��Ʋ�� ���ư��⸦ ���� ���
    {
        ChangeState(UIState.Title); //GameUI ����

        uISceneCameraPlay = false; //Ÿ��Ʋ�� ī�޶� ��� ������
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

    //Game ����
    
    public void OnClickPause()
    {
        Time.timeScale = 0; //�Ͻ����������� �ð� ����
        ChangeState(UIState.Pause);
    }

    public void UpdateFeverDuration(float _duration)
    {
        gameUI.UpdateFeverDuration(_duration);
    }

    public void UpdateInvincibilityDuration(float _duration)
    {
        gameUI.UpdateInvincibilityDuration(_duration);
    }

    //Pause ����

    public void OnClickBack()
    {
        Time.timeScale = 1;
        ChangeState(UIState.Game);
    }

    public void OnClickLobby()
    {
        Time.timeScale = 1;
        ChangeState(UIState.Lobby);
        SceneManager.LoadScene("TitleScene");
    }

    //Setting ����

    public void OnClickSettingBack()
    {
        ChangeState(prevState);
    }

    //GameOver ����
    public void PlayerDeadUI()
    {
        gameOverUI.SetScoreText();
        ChangeState(UIState.GameOver);
    }

    public void OnClickGameOverLobby()
    {
        ChangeState(UIState.Lobby);
        SceneManager.LoadScene("TitleScene");
    }

    //Achievement ����

    public void OnClickAchievementLobby()
    {
        ChangeState(UIState.Lobby);
    }

    //Customize����

    public void OnClickCustomizeLobby()
    {
        ChangeState(UIState.Lobby);
    }

}
