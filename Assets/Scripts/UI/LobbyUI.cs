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
    }

    public void OnClickPlayButton() 
    {
        uiManager.OnClickPlay();
    }
    public void OnClickPrevButton()
    {
        uiManager.OnClickPrev();
    }

    public void OnClickSettingButton()
    {
        uiManager.OnClickSetting();
    }

}
