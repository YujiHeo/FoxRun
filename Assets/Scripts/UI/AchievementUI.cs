using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementUI : BaseUI
{
    public Button lobbyButton;
    public GameObject[] achievements;
    public GameObject achievementPrefab;
    protected override UIState GetUIState()
    {
        return UIState.Achievement;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        lobbyButton.onClick.AddListener(OnClickAchievementLobbyButton);

        achievements = new GameObject[AchivementManager.instance.achivs.Count()];
        for (int i = 0; i < AchivementManager.instance.achivs.Count(); i++)
        {
            achievements[i] = Instantiate(achievementPrefab, this.transform);
            achievements[i].GetComponent<RectTransform>().localPosition = new Vector3(0, 280 - i * 160, 0);
        }

        UpdateAchievements();

    }

    public void UpdateAchievements() //도전과제 내역 업데이트용
    {
        for (int i = 0; i < AchivementManager.instance.achivs.Count(); i++)
        {
            achievements[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                = $"{AchivementManager.instance.achivs[i].Name}\n{AchivementManager.instance.achivs[i].Description}";

            if (AchivementManager.instance.achivs[i].isClear) //도전과제를 달성한 경우
            {
                achievements[i].GetComponent<Image>().color = Color.white ;
            }else if (!AchivementManager.instance.achivs[i].isClear) //도전과제를 미달성한 경우
            {
                achievements[i].GetComponent<Image>().color = Color.gray;
            }    
        }
    }

    public void OnClickAchievementLobbyButton()
    {
        uiManager.PlayUIClickAudio();
        uiManager.OnClickAchievementLobby();
    }
}
