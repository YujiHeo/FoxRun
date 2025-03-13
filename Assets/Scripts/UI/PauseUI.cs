using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    public Button backButton;
    public Button settingButton;
    public Button lobbyButton;
    protected override UIState GetUIState()
    {
        return UIState.Pause;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        backButton.onClick.AddListener(OnBackButtonClick);
        lobbyButton.onClick.AddListener(OnLobbyButtonClick);
    }

    public void OnBackButtonClick()
    {
        uiManager.OnClickBack();
    }

    public void OnLobbyButtonClick()
    {
        uiManager.OnClickLobby();
    }

}
