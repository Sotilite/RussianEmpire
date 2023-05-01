using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void Loader(int number) => SceneManager.LoadScene(number);

    public void Loader(string name) => SceneManager.LoadScene(name);

    public void LoadLevel(int level)
    {
        DataHolder.Level = level;
        DataHolder.Chapter = (level - 1) / 4 + 1;
        if (level % 4 == 0)
            Loader("LevelDouble");
        else
            Loader("Level");
    }

    public void LoadInfo(int chapter)
    {
        DataHolder.Chapter = chapter;
        Loader("Info");
    }

    public void LoadMenu() => Loader("Menu " + DataHolder.Chapter);

    public void ExitGame() => Application.Quit();
}
