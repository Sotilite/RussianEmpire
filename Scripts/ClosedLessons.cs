using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosedLessons : MonoBehaviour
{
    public Image level1;
    public Image level2;
    public Image chest1;
    public Image level3;
    public Image chest2;
    public Image level4;
    public Image chapter;
    public Image info;

    public Sprite closedLevel;
    public Sprite closedBook;
    public Sprite closedChest;
    public Sprite closedCup;

    public Sprite goldenLevel;
    public Sprite goldenChest;
    public Sprite goldenBook;
    public Sprite goldenCup;
    public Sprite goldenChapter;
    public Sprite goldenInfo;

    public void Awake()
    {
        var lvl = DataHolder.LevelMax + 1;
        var chapter = DataHolder.Chapter;
        var actualLevel = 0;
        if (chapter == 0)
            actualLevel = lvl;
        else if (lvl > chapter * 4)
            actualLevel = 5;
        else if (lvl > (chapter - 1) * 4)
            actualLevel = (lvl - (chapter - 1) * 4);
        else
            actualLevel = 0;
        //var lvlForPage = lvl - (chapter - 1) * 4;
        //var actualLevel = lvlForPage > 0 ? lvlForPage % 4 : 0;
        var actualChest = DataHolder.ChestMax - (chapter - 1) * 2;
        switch (actualLevel)
        {
            case 0:
                ReimageAndCloseButton(level1, closedLevel, true);
                ReimageAndCloseButton(level2, closedLevel, true);
                ReimageAndCloseButton(chest1, closedChest, true);
                ReimageAndCloseButton(level3, closedBook, true);
                ReimageAndCloseButton(chest2, closedChest, true);
                ReimageAndCloseButton(level4, closedCup, true);
                ResizeImage(level4.gameObject, new Vector3(0.4f, 0.8f, 1));
                break;
            case 1:
                ReimageAndCloseButton(level2, closedLevel, true);
                ReimageAndCloseButton(chest1, closedChest, true);
                ReimageAndCloseButton(level3, closedBook, true);
                ReimageAndCloseButton(chest2, closedChest, true);
                ReimageAndCloseButton(level4, closedCup, true);
                ResizeImage(level4.gameObject, new Vector3(0.4f, 0.8f, 1));
                break;
            case 2:
                ReimageAndCloseButton(level1, goldenLevel, false);
                ReimageAndCloseButton(chest1, closedChest, true);
                ReimageAndCloseButton(level3, closedBook, true);
                ReimageAndCloseButton(chest2, closedChest, true);
                ReimageAndCloseButton(level4, closedCup, true);
                ResizeImage(level4.gameObject, new Vector3(0.4f, 0.8f, 1));
                break;
            case 3:
                ReimageAndCloseButton(level1, goldenLevel, false);
                ReimageAndCloseButton(level2, goldenLevel, false);
                if (actualChest >= 1) ReimageAndCloseButton(chest1, goldenChest, true);
                ReimageAndCloseButton(chest2, closedChest, true);
                ReimageAndCloseButton(level4, closedCup, true);
                ResizeImage(level4.gameObject, new Vector3(0.4f, 0.8f, 1));
                break;
            case 4:
                ReimageAndCloseButton(level1, goldenLevel, false);
                ReimageAndCloseButton(level2, goldenLevel, false);
                if (actualChest >= 1) ReimageAndCloseButton(chest1, goldenChest, true);
                ReimageAndCloseButton(level3, goldenBook, false);
                if (actualChest >= 2) ReimageAndCloseButton(chest2, goldenChest, true);
                break;
            default:
                ReimageAndCloseButton(level1, goldenLevel, false);
                ReimageAndCloseButton(level2, goldenLevel, false);
                if (actualChest >= 1) ReimageAndCloseButton(chest1, goldenChest, true);
                ReimageAndCloseButton(level3, goldenBook, false);
                if (actualChest >= 2) ReimageAndCloseButton(chest2, goldenChest, true);
                ReimageAndCloseButton(level4, goldenCup, false);
                ReimageAndCloseButton(this.chapter, goldenChapter, false);
                ReimageAndCloseButton(info, goldenInfo, false);
                break;
        }
    }

    public static void ReimageAndCloseButton(Image obj, Sprite sprite, bool needCloseButton)
    {
        obj.sprite = sprite;
        if (needCloseButton)
            obj.gameObject.GetComponent<Button>().enabled = false;
    }

    private static void ResizeImage(GameObject obj, Vector3 scale) => obj.transform.localScale = scale;

    public void MakeGoldChest(Image chest)
    {
        OpenChest();
        //chest.sprite = goldenChestium;
        //chest.gameObject.GetComponent<Button>().enabled = false;
        Awake();
    }

    private static void OpenChest()
    {
        var rnd = new System.Random();
        DataHolder.IncreaseMoney(rnd.Next(25, 75));
        DataHolder.ChestMax++;
        DataHolder.SaveGame();
    }
}
