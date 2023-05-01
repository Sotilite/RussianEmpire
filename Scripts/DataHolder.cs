using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder
{
    public static int Level = 1;
    public static int Chapter = 1;
    public static int XP = 0;
    public static int LevelMax = 0;

    public static void IncreaseXP(int xp)
    {
        XP += xp;
    }

    public static void LevelDone()
    {
        LevelMax = Level > LevelMax ? Level : LevelMax;
    }

    public static void SaveGame()
    {
        PlayerPrefs.SetInt("SavedMaxLevel", DataHolder.LevelMax);
        PlayerPrefs.SetInt("SavedXP", DataHolder.XP);
    }

    public static void ResetData()
    {
        PlayerPrefs.DeleteAll();
        DataHolder.LevelMax = 0;
        DataHolder.XP = 0;
    }

    public static void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedMaxLevel"))
        {
            DataHolder.LevelMax = PlayerPrefs.GetInt("SavedMaxLevel");
            DataHolder.XP = PlayerPrefs.GetInt("SavedXP");
        }
    }
}