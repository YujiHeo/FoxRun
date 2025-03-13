using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
    public Button backButton;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Button bgmMuteButton;
    public Button sfxMuteButton;
    protected override UIState GetUIState()
    {
        return UIState.Setting;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        backButton.onClick.AddListener(OnClickSettingBackButton);
        bgmMuteButton.onClick.AddListener(OnClickBgmMuteButton);
        sfxMuteButton.onClick.AddListener(OnClickSfxMuteButton);
    }

    public void OnClickSettingBackButton()
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickSettingBack();
    }

    public void OnClickBgmMuteButton()
    {
        uiManager.PlayUIClickAudio();

        if (SoundManager.Instance.isBGMMute == true) // 브금 뮤트가 켜진 경우
        {
           bgmMuteButton.GetComponent<Image>().color = Color.white;
        }
        else if (SoundManager.Instance.isBGMMute == false) //브금 뮤트가 꺼진 경우
        {
            bgmMuteButton.GetComponent<Image>().color = Color.gray;
        }

        SoundManager.Instance.ToggleBGMMute();
    }

    public void OnClickSfxMuteButton()
    {
        uiManager.PlayUIClickAudio();

        if (SoundManager.Instance.isSFXMute == true) // 브금 뮤트가 켜진 경우
        {
            sfxMuteButton.GetComponent<Image>().color = Color.white;
        }
        else if (SoundManager.Instance.isSFXMute == false) //브금 뮤트가 꺼진 경우
        {
            sfxMuteButton.GetComponent<Image>().color = Color.gray;
        }

        SoundManager.Instance.ToggleSFXMute();
    }
}
