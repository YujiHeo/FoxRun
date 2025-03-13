using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    public Button testButton;
    public Button pauseButton;
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        testButton.onClick.AddListener(OnClickLobbyButton);
        pauseButton.onClick.AddListener(OnClickPauseButton);
    }

    public void OnClickLobbyButton()
    {
        uiManager.OnClickLobby();
    }

    public void OnClickPauseButton()
    {
        uiManager.OnClickPause();
    }

}
