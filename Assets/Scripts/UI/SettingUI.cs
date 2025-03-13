using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
    public Button backButton;
    protected override UIState GetUIState()
    {
        return UIState.Setting;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        backButton.onClick.AddListener(OnClickSettingBackButton);
    }

    public void OnClickSettingBackButton()
    {
        uiManager.OnClickSettingBack();
    }
}
