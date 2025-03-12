using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

}
