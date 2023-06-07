using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DoubleMode : MonoBehaviour
{
    public class Question
    {
        public string question;
        public int answerNumber;

        public Question(string question, string first, string second, int answer)
        {
            this.question = question;
            this.answerNumber = answer;
        }
    }

    public int level;
    public Text realQuestion, questionsDone, mistakesDone;

    public GameObject Finisher, vanisher, ButtonGoNext,
        tick1, tick2, cross1, cross2;

    private static int questionsCount;
    private static int mistakesCount;
    public static bool isAnswered;
    public static LinkedList<Question> questions = new();

    public void Awake()
    {
        level = DataHolder.Level;
        GenerateLevel(level);
    }

    public void Choose(int number)
    {
        if (!isAnswered)
        {
            if (questions.Count != 0)
            {
                if (number == questions.First.Value.answerNumber)
                {
                    isAnswered = true;
                    ButtonGoNext.SetActive(true);
                    questions.RemoveFirst();
                    switch (number)
                    {
                        case 1:
                            tick1.SetActive(true);
                            break;
                        case 2:
                            tick2.SetActive(true);
                            break;
                    }
                }
                else
                {
                    mistakesCount++;
                    switch (number)
                    {
                        case 1:
                            cross1.SetActive(true);
                            break;
                        case 2:
                            cross2.SetActive(true);
                            break;
                    }
                }
            }
        }
    }

    public void RefreshQuestion()
    {
        isAnswered = false;
        ButtonGoNext.SetActive(false);
        RefreshCrossesAndTicks();
        if (questions.Count != 0)
            realQuestion.text = questions.First.Value.question;
        else
        {
            var points = questionsCount * 5 - (mistakesCount % (questionsCount * 3));
            DataHolder.IncreaseMoney(points);
            DataHolder.IncreaseXP(points);
            DataHolder.LevelDone();
            DataHolder.SaveGame();
            questionsDone.text = $"Вы ответили на {questionsCount} вопросов, допустив ошибок: {mistakesCount}";
            mistakesDone.text = $"Получено очков: {points}";
            vanisher.SetActive(false);
            Finisher.SetActive(true);
            mistakesCount = 0;
        }
    }

    private LinkedList<Question> GetRandomizedQuestions(List<Question> listQuestions)
    {
        var random = new System.Random();
        listQuestions = listQuestions.OrderBy(_ => random.Next()).ToList();
        var rndQuestions = new LinkedList<Question>();
        foreach (var question in listQuestions)
            rndQuestions.AddLast(question);
        return rndQuestions;
    }

    public void RefreshCrossesAndTicks()
    {
        cross1.SetActive(false);
        cross2.SetActive(false);
        tick1.SetActive(false);
        tick2.SetActive(false);
    }

    private void GenerateLevel(int level)
    {
        var questionsLoad = new List<Question>();
        switch (level)
        {
            case 4:
                questionsLoad.Add(new Question("Была ли первая четверть XVIII в. эпохой преобразований в России?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Были ли проведены Азовские походы?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Была ли создана антитурецкая коалиция?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Была ли главной целью внешней политики России овладение побережьем Балтики?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Была ли Россия успешна в начале Северной войны?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Была ли Полтавская битва коренным переломом в ходе войны?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Были ли проведены реформы в государственном управлении?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Была ли церковь государственным учреждением во время Петра I?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Были ли проведены реформы в области культуры и просвещения?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Была ли целью Петра I ликвидация отставания России от Запада?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Удалось ли Петру I найти союзников против Турции?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Была ли начальная стадия Северной войны неудачной для России?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Были ли взяты шведские крепости Нотебург и Ниеншанц русскими в ходе Северной войны?", "Да", "Нет", 1));
                break;
            case 8:
                questionsLoad.Add(new Question("Старая аристократия была довольна тем, что Меншиков стал некоронованным царём?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Малолетний император был узником временщика?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Долгоруковы убедили Петра II в необходимости избавиться от Меншикова?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("После смерти Петра II влияние Долгоруковых на двор увеличилось?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Анна Иоанновна была приглашена на царствование Верховным Тайным Советом?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Кондиции были условиями, которые Анна Иоанновна выдвинула для членов Верховного Тайного Совета?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Умер Пётр Алексеевич Романов 28 января 1725 года?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Был ли официальный наследник у Петра Алексеевича Романова в момент его смерти?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Является ли термин 'дворцовый переворот' новый для исторической науки?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Был ли Петр II узником временщика?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Стала ли Екатерина Долгорукова женой Петра II?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Продолжительность правления Анны Иоанновны составила примерно 10 лет?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Во время правления Анны Иоанновны увеличилось влияние 'иноземцев' на дворе?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Русско-турецкая война 1735−1739 годов закончилась поражением России?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Бирон удержался у власти при новом царе не больше месяца?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Помимо подписания мирного договора со врагом России, Петр III больше не совершал неудачных решений?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Елизавета Петровна стала русской самодержавной императрицей на долгие 20 лет?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Русские войска не одержали победы в Семилетней войне?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Большинство исследователей считает приход Екатерины Великой ко власти концом эпохи дворцовых переворотов?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Внутреннее положение империи стабилизировалось после прихода Екатерины Великой ко власти?", "Да", "Нет", 1));
                break;
            case 12:
                questionsLoad.Add(new Question("Подход императрицы к идеям просвещения был избирательным.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("В ходе губернской реформы полномочия губернаторов и органов местной власти не были расширены.", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Екатерина II разделяла идеи французских философов о 'естественных' правах человека, о равенстве всех от рождения.", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Народное восстание, известное как  'пугачёвщина' охватило Оренбургский край, Урал, Среднее и Нижнее Поволжье, часть южных губерний.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Русский флот под командованием  Ф.Ф. Ушакова  потерпел поражение от турок у  о. Тендра.", "Да", "Нет", 2));
                questionsLoad.Add(new Question("По условиям  Ясского мирного договора Турция признала переход под власть России Крыма и Восточной Грузии.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Развитие Российской империи сдерживалось существованием системы крепостного права и беспрецедентных дворянских привилегий.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("'Просветительские' реформы Екатерины II затронули крепостных крестьян, что улучшило их положение.", "Да", "Нет", 2));
                questionsLoad.Add(new Question("В  1783 г. произошло окончательное присоединение Крыма к России.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("В союзе с Пруссией и Австрией Россия участвовала в разделе Османской империи.", "Да", "Нет", 2));
                break;
        }
        questions = GetRandomizedQuestions(questionsLoad);
        questionsCount = questions.Count;
        RefreshQuestion();
    }

}
