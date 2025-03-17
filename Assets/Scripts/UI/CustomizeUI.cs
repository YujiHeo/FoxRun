using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeUI : BaseUI
{
    public Button applyButton;
    public Button lobbyButton;
    public Slider RSlider;
    public Slider GSlider;
    public Slider BSlider;
    public Image foxImage;
    public Material foxMaterial;
    protected override UIState GetUIState()
    {
        return UIState.Customize;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        applyButton.onClick.AddListener(OnClickApplyButton);
        lobbyButton.onClick.AddListener(OnClickCustomizeLobbyButton);

        RSlider.onValueChanged.AddListener(delegate { UpdateColor(); });
        GSlider.onValueChanged.AddListener(delegate { UpdateColor(); });
        BSlider.onValueChanged.AddListener(delegate { UpdateColor(); });

    }

    private void OnEnable()
    {
        // UI 활성화 시 저장된 Color 값을 불러온 후 슬라이드 값, 플레이어 이미지에 반영
        Color loadColor = DataManager.Instance.LoadColor();

        RSlider.value = loadColor.r;

        GSlider.value = loadColor.g;

        BSlider.value = loadColor.b;

        UpdateColor();
        foxMaterial.color = DataManager.Instance.LoadColor();
    }

    public void OnClickCustomizeLobbyButton()
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickCustomizeLobby();
    }

    void OnClickApplyButton()
    {
        uiManager.PlayUIClickAudio();

        if (DataManager.Instance == null)
        {
            Debug.Log("DataManager를 찾지 못했습니다.");
            return;
        }

        // 저장 버튼 클릭 시 PlayerPrefs에 현재 Color값 저장
        DataManager.Instance.SaveColor(foxImage.color);
        foxMaterial.color = DataManager.Instance.LoadColor();
    }

    public void UpdateColor()
    {
        Color foxColor = new Color(RSlider.value,GSlider.value,BSlider.value);
        foxImage.color = foxColor;
    }
}
