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
        // UI Ȱ��ȭ �� ����� Color ���� �ҷ��� �� �����̵� ��, �÷��̾� �̹����� �ݿ�
        Color loadColor = DataManager.Instance.LoadColor();

        RSlider.value = loadColor.r;

        GSlider.value = loadColor.g;

        BSlider.value = loadColor.b;

        UpdateColor();
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
            Debug.Log("DataManager�� ã�� ���߽��ϴ�.");
            return;
        }

        // ���� ��ư Ŭ�� �� PlayerPrefs�� ���� Color�� ����
        DataManager.Instance.SaveColor(foxImage.color);
    }

    public void UpdateColor()
    {
        Color foxColor = new Color(RSlider.value,GSlider.value,BSlider.value);
        foxImage.color = foxColor;
    }
}
