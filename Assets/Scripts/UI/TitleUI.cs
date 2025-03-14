using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : BaseUI
{

    public Button startButton;
    public Button exitButton;
    protected override UIState GetUIState()
    {
        return UIState.Title;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickStartButton()
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickStart();
    }

    public void OnClickExitButton()
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickExit();
    }

}
