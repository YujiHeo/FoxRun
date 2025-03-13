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

    public void FixedUpdate() //수시로 HP 정보와 Score 정보 업데이트
    {
        UpdateScoreInfo();
        UpdateHpInfo();
    }

    public void OnClickLobbyButton()
    {
        uiManager.OnClickLobby();
    }

    public void OnClickPauseButton()
    {
        uiManager.OnClickPause();
    }

    public void UpdateScoreInfo()
    {
        scoreText.text = $"Score : {GameManager.instance.Score}";
    }

    public void UpdateHpInfo()
    {
        int hpTemp = GameManager.instance.player.condition.Hp;
        int maxHpTemp = GameManager.instance.player.condition.MaxHp;

        if (hpTemp > 0) //HP가 0보다 큰 경우
        {
            for (int i = 0; i < hpTemp; i++)
            {
                heartImages[i].color = Color.white;
            } //hp 잔여량 만큼 하얀색으로 칠하고

            for (int i = hpTemp; i < maxHpTemp; i++)
            {
                heartImages[i].color = Color.black;
            } //나머지 hp는 검은색으로 칠해라

        }
        else if (hpTemp <= 0) //HP가 0보다 작거나 같은 경우
        {
            for (int i = 0; i < maxHpTemp; i++)
            {
                heartImages[i].color = Color.black;
            } // 모든 hp를 검은색으로 칠해라
        }


        switch (GameManager.instance.player.condition.Hp)
        {
            case 0:
                heartImages[0].color = Color.black;
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

}
