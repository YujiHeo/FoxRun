using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    public TextMeshProUGUI scoreText;
    public Button lobbyButton;
    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

    }

    public void SetScoreText()
    {
        scoreText.text = $"Score : {GameManager.instance.Score}";
    }

    public void OnClickGameOverLobbyButton()
    {
        uiManager.OnClickGameOverLobby();
    }

}
