using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    public Button testButton;
    public Button pauseButton;
    public TextMeshProUGUI scoreText;
    public Image[] heartImages = new Image[3];



    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        testButton.onClick.AddListener(OnClickLobbyButton);
        pauseButton.onClick.AddListener(OnClickPauseButton);

    }

    public void FixedUpdate() //���÷� HP ������ Score ���� ������Ʈ
    {
        UpdateScoreInfo();
        UpdateHpInfo();
    }

    public void OnClickLobbyButton()
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickLobby();
    }

    public void OnClickPauseButton()
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickPause();
    }

    public void UpdateScoreInfo()
    {
        scoreText.text = $"Score : {GameManager.instance.score}";
    }

    public void UpdateHpInfo()
    {
        int hpTemp = GameManager.instance.player.condition.Hp;
        int maxHpTemp = GameManager.instance.player.condition.MaxHp;

        if (hpTemp > 0) //HP�� 0���� ū ���
        {
            for (int i = 0; i < hpTemp; i++)
            {
                heartImages[i].color = Color.white;
            } //hp �ܿ��� ��ŭ �Ͼ������ ĥ�ϰ�

            for (int i = hpTemp; i < maxHpTemp; i++)
            {
                heartImages[i].color = Color.black;
            } //������ hp�� ���������� ĥ�ض�

        }
        else if (hpTemp <= 0) //HP�� 0���� �۰ų� ���� ���
        {
            for (int i = 0; i < maxHpTemp; i++)
            {
                heartImages[i].color = Color.black;
            } // ��� hp�� ���������� ĥ�ض�
        }

    }

}
