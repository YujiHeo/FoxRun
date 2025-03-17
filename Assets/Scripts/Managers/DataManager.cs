using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private readonly string ColorKey = "SaveColor"; 

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Color값 저장
    public void SaveColor(Color color)
    {
        try
        {
            // Color값을 Hex 코드 문자열로 변환해 저장
            PlayerPrefs.SetString(ColorKey, ColorUtility.ToHtmlStringRGBA(color));
            PlayerPrefs.Save();
        }
        catch
        {
            Debug.Log("색상 정보를 저장하는 중 오류가 발생했습니다.");
            return;
        }

        Debug.Log($"{color.r}, {color.g}, {color.b} 색상을 저장했습니다.");
    }

    // Color값 불러오기
    public Color LoadColor()
    {
        Color loadedColor = Color.white;

        try
        {
            if (!PlayerPrefs.HasKey(ColorKey)) return Color.white;

            string loadedColorStirng = PlayerPrefs.GetString(ColorKey, string.Empty);

            // 키를 사용해 불러온 Hex 코드 문자열값을 Color 값으로 변환에 성공한다면 true를 반환하며 loadedColor에 값을 넣어줌
            if (ColorUtility.TryParseHtmlString("#" + loadedColorStirng, out loadedColor))
            {
                Debug.Log($"{loadedColor.r}, {loadedColor.g}, {loadedColor.b} 색상을 불러왔습니다.");
                return loadedColor;
            }
        }
        catch
        {
            Debug.LogError("색상 정보를 불러오는 중 오류가 발생했습니다."); return Color.white;
        }

        return loadedColor;
    }

}
