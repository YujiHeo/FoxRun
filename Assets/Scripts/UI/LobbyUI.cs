using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{

    public Button playButton;
    public Button settingButton;
    public Button achievementButton;
    public Button customizeButton;
    public Button prevButton;

    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        playButton.onClick.AddListener(OnClickPlayButton);
        prevButton.onClick.AddListener(OnClickPrevButton);
        settingButton.onClick.AddListener(OnClickSettingButton);
        achievementButton.onClick.AddListener(OnClickAchievementButton);
        customizeButton.onClick.AddListener(OnClickCustomizeButton);
    }

    public void OnClickPlayButton() 
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickPlay();
    }
    public void OnClickPrevButton()
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickPrev();
    }

    public void OnClickSettingButton()
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickSetting();
    }

    public void OnClickAchievementButton()
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickAchievement();
    }

    public void OnClickCustomizeButton()
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickCustomize();
    }

}
