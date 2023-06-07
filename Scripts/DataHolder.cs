using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder
{
    public static int Level = 1;
    public static int Chapter = 0;
    public static int XP = 0;
    public static int LevelMax = 0;
    public static int ChestMax = 0;
    public static int StoryIndex = 0;
    public static int Money = 0;
    public static int ChosenTheme = 0;
    public static bool HasFirstTheme;
    public static bool HasSecondTheme;
    public static bool HasThirdTheme;
    public static bool HasDoneFirstStory;

    public static void IncreaseXP(int xp)
    {
        XP += xp;
    }

    public static void IncreaseMoney(int money)
    {
        Money += money;
    }

    public static void LevelDone()
    {
        LevelMax = Level > LevelMax ? Level : LevelMax;
    }

    public static void SaveGame()
    {
        PlayerPrefs.SetInt("SavedMaxLevel", DataHolder.LevelMax);
        PlayerPrefs.SetInt("SavedMaxChest", DataHolder.ChestMax);
        PlayerPrefs.SetInt("SavedXP", DataHolder.XP);
        PlayerPrefs.SetInt("SavedStoryIndex", DataHolder.StoryIndex);
        PlayerPrefs.SetInt("SavedMoney", DataHolder.Money);
        PlayerPrefs.SetInt("SavedChosen", DataHolder.ChosenTheme);
        PlayerPrefs.SetInt("SavedFirst", DataHolder.HasFirstTheme == true ? 1 : 0);
        PlayerPrefs.SetInt("SavedSecond", DataHolder.HasSecondTheme == true ? 1 : 0);
        PlayerPrefs.SetInt("SavedThird", DataHolder.HasThirdTheme == true ? 1 : 0);
        PlayerPrefs.SetInt("SavedHasDoneFirstStory", DataHolder.HasDoneFirstStory == true ? 1 : 0);
    }

    public static void ResetData()
    {
        PlayerPrefs.DeleteAll();
        DataHolder.LevelMax = 0;
        DataHolder.XP = 0;
        DataHolder.ChestMax = 0;
        DataHolder.Money = 0;
        DataHolder.ChosenTheme = 0;
        DataHolder.HasFirstTheme = false;
        DataHolder.HasSecondTheme = false;
        DataHolder.HasThirdTheme = false;
        DataHolder.HasDoneFirstStory = false;
        SaveGame();
    }

    public static void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedMaxLevel"))
        {
            DataHolder.LevelMax = PlayerPrefs.GetInt("SavedMaxLevel");
            DataHolder.ChestMax = PlayerPrefs.GetInt("SavedMaxChest");
            DataHolder.XP = PlayerPrefs.GetInt("SavedXP");
            DataHolder.StoryIndex = PlayerPrefs.GetInt("SavedStoryIndex");
            DataHolder.Money = PlayerPrefs.GetInt("SavedMoney");
            DataHolder.ChosenTheme = PlayerPrefs.GetInt("SavedChosen");
            DataHolder.HasFirstTheme = PlayerPrefs.GetInt("SavedFirst") != 0 ? true : false;
            DataHolder.HasSecondTheme = PlayerPrefs.GetInt("SavedSecond") != 0 ? true : false;
            DataHolder.HasThirdTheme = PlayerPrefs.GetInt("SavedThird") != 0 ? true : false;
            DataHolder.HasDoneFirstStory = PlayerPrefs.GetInt("SavedHasDoneFirstStory") != 0 ? true : false;
        }
    }
}