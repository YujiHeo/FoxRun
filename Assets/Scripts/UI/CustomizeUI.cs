using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.Customize;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

    }
}
