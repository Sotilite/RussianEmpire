using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryCreater : MonoBehaviour
{
    public int StoryIndex = 0;
    public Sprite[] story0;
    public Sprite[] story1;
    public Sprite[] story2;
    public Sprite[] story3;
    public Sprite[] story4;
    public Sprite[] story5;
    public Sprite[] story6;
    public Sprite[] story7;
    public Sprite[] story8;
    public Sprite[] story9;
    public Image image;
    private Sprite[] currStory;
    private static int count;

    public void Awake()
    {
        StoryIndex = DataHolder.Chapter;
        if (DataHolder.HasDoneFirstStory && StoryIndex == 0)
        {
            DataHolder.Chapter = 1;
            SceneManager.LoadScene("Menu 1");
        }
        switch (StoryIndex)
        {
            case 0:
                currStory = story0;
                break;
            case 1:
                currStory = story1;
                break;
            case 2:
                currStory = story2;
                break;
            case 3:
                currStory = story3;
                break;
            case 4:
                currStory = story4;
                break;
            case 5:
                currStory = story5;
                break;
            case 6:
                currStory = story6;
                break;
            case 7:
                currStory = story7;
                break;
            case 8:
                currStory = story8;
                break;
            case 9:
                currStory = story9;
                break;
            default: break;
        }
        count = 0;
        image.sprite = currStory[0];
    }

    public void NextStoryPage()
    {
        if (currStory.Length > count + 1)
        {
            count++;
            image.sprite = currStory[count];
        }
        else
        {
            if (StoryIndex == 0)
                DataHolder.Chapter = 1;
            SceneManager.LoadScene("Menu " + DataHolder.Chapter);
        }
    }

    public void PreviousStoryPage()
    {
        if (count > 1)
        {
            count--;
            image.sprite = currStory[count];
        }
    }
}
