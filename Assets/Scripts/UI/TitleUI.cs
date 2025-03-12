using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.Title;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

}
