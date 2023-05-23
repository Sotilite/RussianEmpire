using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataControler : MonoBehaviour
{
    public void ChapterUp()
    {
        DataHolder.Chapter++;
    }

    public void ChapterDown()
    {
        DataHolder.Chapter--;
    }

    public void Awake()
    {
        DataHolder.LoadGame();
    }

    public void SetChapter(int chapter)
    {
        DataHolder.Chapter = chapter;
    }
}
