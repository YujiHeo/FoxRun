using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achivement", menuName = "AchiveData")]
public class AchivData : ScriptableObject
{
    public string achivName;
    public string description;
    public Sprite icon;
    public bool isClear = false;
}
