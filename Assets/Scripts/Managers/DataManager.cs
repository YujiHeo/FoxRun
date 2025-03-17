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

    // Color�� ����
    public void SaveColor(Color color)
    {
        try
        {
            // Color���� Hex �ڵ� ���ڿ��� ��ȯ�� ����
            PlayerPrefs.SetString(ColorKey, ColorUtility.ToHtmlStringRGBA(color));
            PlayerPrefs.Save();
        }
        catch
        {
            Debug.Log("���� ������ �����ϴ� �� ������ �߻��߽��ϴ�.");
            return;
        }

        Debug.Log($"{color.r}, {color.g}, {color.b} ������ �����߽��ϴ�.");
    }

    // Color�� �ҷ�����
    public Color LoadColor()
    {
        Color loadedColor = Color.white;

        try
        {
            if (!PlayerPrefs.HasKey(ColorKey)) return Color.white;

            string loadedColorStirng = PlayerPrefs.GetString(ColorKey, string.Empty);

            // Ű�� ����� �ҷ��� Hex �ڵ� ���ڿ����� Color ������ ��ȯ�� �����Ѵٸ� true�� ��ȯ�ϸ� loadedColor�� ���� �־���
            if (ColorUtility.TryParseHtmlString("#" + loadedColorStirng, out loadedColor))
            {
                Debug.Log($"{loadedColor.r}, {loadedColor.g}, {loadedColor.b} ������ �ҷ��Խ��ϴ�.");
                return loadedColor;
            }
        }
        catch
        {
            Debug.LogError("���� ������ �ҷ����� �� ������ �߻��߽��ϴ�."); return Color.white;
        }

        return loadedColor;
    }

}
