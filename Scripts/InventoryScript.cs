using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public Text xpInfo, levelsInfo;

    public void Awake()
    {
        xpInfo.text = $"���������� �����: {DataHolder.XP}";
        levelsInfo.text = $"�������� �������: {DataHolder.LevelMax}";
    }

    public void ResetData()
    {
        DataHolder.ResetData();
    }
}
