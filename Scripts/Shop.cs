using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text MoneyInfo;
    public Button[] buttons;
    public Text[] texts;
    public GameObject back1, back2, back3;

    public void Awake()
    {
        MoneyInfo.text = $"{DataHolder.Money}";
        switch (DataHolder.ChosenTheme)
        {
            case 0:
                texts[3].text = "Выбрано";
                //buttons[3].GetComponent<Image>().color = ;
                texts[2].text = DataHolder.HasThirdTheme ? "Приобретено" : "Купить за 350";
                texts[1].text = DataHolder.HasSecondTheme ? "Приобретено" : "Купить за 250";
                texts[0].text = DataHolder.HasFirstTheme ? "Приобретено" : "Купить за 450";
                break;
            case 3:
                texts[3].text = "Приобретено";
                texts[2].text = "Выбрано";
                texts[1].text = DataHolder.HasSecondTheme ? "Приобретено" : "Купить за 250";
                texts[0].text = DataHolder.HasFirstTheme ? "Приобретено" : "Купить за 450";
                break;
            case 2:
                texts[3].text = "Приобретено";
                texts[2].text = DataHolder.HasThirdTheme ? "Приобретено" : "Купить за 350";
                texts[1].text = "Выбрано";
                texts[0].text = DataHolder.HasFirstTheme ? "Приобретено" : "Купить за 450";
                break;
            case 1:
                texts[3].text = "Приобретено";
                texts[2].text = DataHolder.HasThirdTheme ? "Приобретено" : "Купить за 350";
                texts[1].text = DataHolder.HasSecondTheme ? "Приобретено" : "Купить за 250";
                texts[0].text = "Выбрано";
                break;
        }
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

    public void BuyOrChoose(int option)
    {
        switch (option)
        {
            case 1:
                if (DataHolder.HasFirstTheme)
                    DataHolder.ChosenTheme = 1;
                else if (DataHolder.Money > 450)
                {
                    DataHolder.IncreaseMoney(-450);
                    DataHolder.HasFirstTheme = true;
                }
                break;
            case 2:
                if (DataHolder.HasSecondTheme)
                    DataHolder.ChosenTheme = 2;
                else if (DataHolder.Money > 250)
                {
                    DataHolder.IncreaseMoney(-250);
                    DataHolder.HasSecondTheme = true;
                }
                break;
            case 3:
                if (DataHolder.HasThirdTheme)
                    DataHolder.ChosenTheme = 3;
                else if (DataHolder.Money > 350)
                {
                    DataHolder.IncreaseMoney(-350);
                    DataHolder.HasThirdTheme = true;
                }
                break;
            case 0:
                DataHolder.ChosenTheme = 0;
                break;
        }
        DataHolder.SaveGame();
        Awake();
    }
}
