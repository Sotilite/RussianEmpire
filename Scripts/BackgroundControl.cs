using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    public GameObject back1, back2, back3;

    public void Awake()
    {
        DataHolder.HasDoneFirstStory = true;
        DataHolder.SaveGame();
        switch (DataHolder.ChosenTheme)
        {
            case 0:
                back1.SetActive(false);
                back2.SetActive(false);
                back3.SetActive(false);
                break;
            case 1:
                back1.SetActive(true);
                back2.SetActive(false);
                back3.SetActive(false);
                break;
            case 2:
                back1.SetActive(false);
                back2.SetActive(true);
                back3.SetActive(false);
                break;
            case 3:
                back1.SetActive(false);
                back2.SetActive(false);
                back3.SetActive(true);
                break;
        }
    }
}
