using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : BaseUI
{
    public Button lobbyButton;
    protected override UIState GetUIState()
    {
        return UIState.Achievement;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        lobbyButton.onClick.AddListener(OnClickAchievementLobbyButton);
    }

    public void OnClickAchievementLobbyButton()
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickAchievementLobby();
    }
}
