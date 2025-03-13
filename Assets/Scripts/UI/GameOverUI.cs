using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

    }

}
