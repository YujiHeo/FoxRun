using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementPopUpUI : BaseUI
{
    public Animator popUpAnimator;
    public TextMeshProUGUI achievementText;

    protected override UIState GetUIState() //안쓰는 함수
    {
        return UIState.Title;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

    }

    public void AchievementCleared(string _description)
    {
        achievementText.text = _description;
        this.StartCoroutine(AchievementAnimationOn(5.0f));
    }

    public IEnumerator AchievementAnimationOn(float _duration)
    {
        popUpAnimator.SetBool("popUp", true);
        yield return new WaitForSeconds(_duration);
        popUpAnimator.SetBool("popUp", false);
    }
}