using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public Text xpInfo, levelsInfo;

    public void Awake()
    {
        xpInfo.text = $"{DataHolder.XP} XP";
        levelsInfo.text = $"{DataHolder.LevelMax} / 36";
    }

    public void ResetData()
    {
        DataHolder.ResetData();
    }
}
