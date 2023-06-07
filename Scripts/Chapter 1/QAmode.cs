﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class QAmode : MonoBehaviour 
{
    public class Question
    {
        public string question;
        public string firstOption;
        public string secondOption;
        public string thirdOption;
        public string fourthOption;
        public int answerNumber;

        public Question(string question, string first, string second, string third, string fourth, int number)
        {
            this.question = question;
            this.firstOption = first;
            this.secondOption = second;
            this.thirdOption = third;
            this.fourthOption = fourth;
            answerNumber = number;
        }
    }

    public int level;
    public Text realQuestion, first, second, third, fourth;

    public Text questionsDone, mistakesDone;
    public GameObject Finisher, tick1, tick2, tick3, tick4, 
        cross1, cross2, cross3, cross4, vanisher, buttonGoNextQuestion;

    private static int questionsCount;
    private static int mistakesCount;
    private static bool isAnswered;
    private LinkedList<Question> questions = new();

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
                var question = questions.First.Value;
                if (number == question.answerNumber)
                {
                    isAnswered = true;
                    buttonGoNextQuestion.SetActive(true);
                    questions.RemoveFirst();
                    switch (number)
                    {
                        case 1:
                            tick1.SetActive(true);
                            break;
                        case 2:
                            tick2.SetActive(true);
                            break;
                        case 3:
                            tick3.SetActive(true);
                            break;
                        case 4:
                            tick4.SetActive(true);
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
                        case 3:
                            cross3.SetActive(true);
                            break;
                        case 4:
                            cross4.SetActive(true);
                            break;
                    }
                }
            }
        }
    }

    public void RefreshQuestion()
    {
        isAnswered = false;
        buttonGoNextQuestion.SetActive(false);
        RefreshCrossesAndTicks();
        if (questions.Count != 0)
        {
            var question = questions.First.Value;
            realQuestion.text = question.question;
            first.text = question.firstOption;
            second.text = question.secondOption;
            third.text = question.thirdOption;
            fourth.text = question.fourthOption;
        }
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

    private void RefreshCrossesAndTicks()
    {
        cross1.SetActive(false);
        cross2.SetActive(false);
        cross3.SetActive(false);
        cross4.SetActive(false);
        tick1.SetActive(false);
        tick2.SetActive(false);
        tick3.SetActive(false);
        tick4.SetActive(false);
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


    private void GenerateLevel(int level)
    {
        var listQuestions = new List<Question>();
        switch (level)
        {
            case 1:
                listQuestions.Add(new Question("В каком году родился Петр 1?", "1689", "1672", "1654", "1700", 2));
                listQuestions.Add(new Question("Какая сверхзадача была у Петра 1?", "ликвидировать отставание России от Запада",
                   "развитие селььского хозяйства", "развитие промышленности", "развитие торговли", 1));
                listQuestions.Add(new Question("В каком году был введен воинский устав при Петре 1?", "1716", "1705", "1720", "1704", 1));
                listQuestions.Add(new Question("Как назывался высшее правительственное учреждение, созданнное Петром в 1711 году?", "Синод", "Государственная Дума", "Сенат", "Палата старейшин", 3));
                listQuestions.Add(new Question("На сколько губерний изначально была поделена Россия при Петре 1?", "8", "4", "10", "16", 1));
                listQuestions.Add(new Question("В каком году будет введен «Табель о рангах»?", "1722", "1720", "1715", "1725", 1));
                listQuestions.Add(new Question("Какую реформу Петр I начал с проведения строительства военно-морского флота?",
                    "Реформа государственного управления", "Церковная реформа", "Военная реформа", "Общественная реформа", 3));
               listQuestions.Add(new Question("Какие вооруженные силы представляли Россию до XVIII в.?", "Регулярная армия", "Дворянское конное ополчение, стрелецкие полки, гарнизоны", "Военный флот Балтийского моря", "Вооруженные силы не существовали", 2));
               listQuestions.Add(new Question("Какую реформу провел Петр I для улучшения местного управления?", "Реформа государственного управления", "Церковная реформа", "Реформа в социально-экономической сфере", "Реформа образования", 1));
               listQuestions.Add(new Question("Какие идеи были положены в основу политики Петра I в области промышленности и торговли?", "Идеи монархизма", "Идеи социализма", "Идеи либерализма и свободной торговли", "Идеи меркантилизма и протекционизма", 4));
               listQuestions.Add(new Question("Как называется указ, который Петр I издал в 1714 г. для консолидации российской аристократии?", "Указ о наследовании", "Указ о единонаследии", "Указ о слиянии землевладения", "Указ о пятнадцатой доле", 2));
               listQuestions.Add(new Question("Какой чиновник стоял во главе Священного Синода, созданного Петром I в 1722 г.?", "Митрополит", "Патриарх", "Обер-прокурор", "Папа Римский", 3));
                break;
            case 2:
               listQuestions.Add(new Question("Какое направление было выбрано в начале правления Петра I как основное?", "Северное", "Восточное", "Южное", "Западное", 3));
               listQuestions.Add(new Question("Какой город был основан в 1703 году?", "Москва", "Санкт-Петербург", "Киев", "Севастополь", 2));
               listQuestions.Add(new Question("Какая крепость была взята русскими в ходе Азовских походов?", "Азов", "Нотебург", "Ниеншанц", "Константинополь", 1));
               listQuestions.Add(new Question("Какой целью было направлено Великое посольство?", "Создание антитурецкой и антишведской коалиции", "Захват побережья Балтики", "Поиск союзников против Польши", "Заключение союза с Испанией", 1));
               listQuestions.Add(new Question("Какой союз был создан Россией в ходе поиска союзников против Швеции?", "Антишведская четвёрка", "Восточный союз", "Южный союз", "Северный союз", 4));
               listQuestions.Add(new Question("Какая задача стала главной внешней политикой России?", "Захват побережья Балтики", "Создание антитурецкой коалиции", "Захват территории на Кавказе", "Добыча золота в Северной Америке", 1));
               listQuestions.Add(new Question("Какой город был основан в ходе Северной войны?", "Москва", "Санкт-Петербург", "Нотебург", "Париж", 2));
               listQuestions.Add(new Question("Какие крепости были взяты русскими в ходе Северной войны?", "Нотебург и Ниеншанц", "Рига и Ревель", "Ни одной", "Дерпт и Нарва", 4));
               listQuestions.Add(new Question("Какой был исход сражения у деревни Лесной?", "Победа шведской армии", "Ничья", "Битва не состоялась", "Победа русской армии", 4));
               listQuestions.Add(new Question("Какое сражение стало коренным переломом в ходе войны?", "Сражение под Нарвой", "Сражение под крепостью Полтава", "Сражение у деревни Лесной", "Битва за Петербург", 2));
               listQuestions.Add(new Question("Какие города были захвачены русскими в 1710 году?", "Выборг, Рига, Ревель", "Нотебург, Ниеншанц, Дерпт", "Москва, Санкт-Петербург, Киев", "Берлин и Франкфурт", 1));
               listQuestions.Add(new Question("Как назывался договор, подписанный Россией и Швецией в 1721 году, оглашающий победу" +
                    " России в Северной войне?", "Ливонский мир", "Ништадтский мир", "Балтийский мир", "Северный договор", 2));
                listQuestions.Add(new Question("Какие территории приобрела Россия в результате Ништадтского мира?", "Ингрия, Эстляндия, Лифляндия," +
                    " Карелия и южная Финляндия", "Крым, Кавказ, Сибирь", "Польша, Украина, Беларусь", "Вся Швеция", 1));
                break;
            case 3:
                listQuestions.Add(new Question("Какое событие положило начало светскому образованию в России?", "Открытие Пушкарской школы в Москве", "Создание инженерной школы", "Открытие «Школы математических и навигацких наук» в Москве", "Открытие первой университетской кафедры в России", 3));
                listQuestions.Add(new Question("Какое событие произошло в 1714 году?", "Введение обязательного образования для дворян", "Открытие Кунсткамеры", "Утверждение устава Академии наук", "Переход России на григорианский календарь", 1));
                listQuestions.Add(new Question("Какое событие произошло в 1724 году?", "Утверждение устава Академии наук", "Принятие Петром I титула императора Всероссийского", "Издание закона о престолонаследии", "Создание первого государственного театра в России", 3));
                listQuestions.Add(new Question("Какое новшество было введено в быт дворянства?", "Изменение системы летоисчисления", "Ношение европейской одежды", "Введение ассамблей", "Введение обязательного уклада для домашнего персонала", 2));
                listQuestions.Add(new Question("Что происходило на ассамблеях?", "Вели беседы (часто деловые), играли в шахматы", "Читали лекции по различным темам", "Проводили научные эксперименты", "Обсуждали политические реформы", 1));
                listQuestions.Add(new Question("Какое образование стало обязательным для дворян в России в 1714 г.?", "Среднее образование", "Высшее образование", "Начальное общеобразовательное образование", "Обучение в инженерной школе", 3));
                listQuestions.Add(new Question("Какое учреждение было открыто в Санкт-Петербурге в 1714 г.?", "Первый общедоступный музей России", "Первый общедоступный театр России", "Первый общедоступный парк России", "Первый университет в России", 1));
                listQuestions.Add(new Question("Что было изменено в системе летоисчисления в России во время правления Петра I?", "Начало года стало 1 января", "Начало года стало 1 марта", "Начало года стало 1 сентября", "Начало года стало 1 октября", 1));
                listQuestions.Add(new Question("Что было введено указом царя в 1718 г.?", "Ассамблеи", "Обязательное военное служение", "Обязательное налоговое обложение", "Обязательное государственное медицинское страхование", 1));
                break;
            case 5:
                listQuestions.Add(new Question("Когда скончался Пётр Алексеевич Романов?", "28 января 1725 года", "28 декабря 1725 года", "28 января 1726 года", "28 февраля 1725 года", 1));
                listQuestions.Add(new Question("Почему у Петра Алексеевича Романова не было наследника престола?", "Он отказался от наследников", "Он был бесплоден", "Он издал указ о престонаследии, по которому наследник назначался самим монархом", "Его наследник скончался ранее", 3));
                listQuestions.Add(new Question("Кто возглавлял 'консервативную' оппозицию Петру Алексеевичу Романову?", "царевич Алексей", "Екатерина II Великая", "Пётр Алексеевич Романов", "Александр Меншиков", 1));
                listQuestions.Add(new Question("Какое решение принял Сенат после смерти Петра Алексеевича Романова?", "Не считать наследницей трона Екатерину", "Назначить наследником престола сына Петра", "Назначить наследницей трона жену Петра Екатерину", "Провести выборы наследника престола", 3));
                listQuestions.Add(new Question("Какой указ Петра Великого упразднил передачу власти наследникам по мужской линии?", "Указ о наследии", "Указ о престолонаследии", "Указ о праве наследования", "Указ о праве наследства", 2));
                listQuestions.Add(new Question("Кто был первым наследником Петра Великого по 'Указу о престолонаследии'?", "Екатерина I", "царевич Алексей", "Анна Иоанновна", "Павел I", 2));
                listQuestions.Add(new Question("Кто был мужем Екатерины II?", "Пётр III", "Александр I", "Николай I", "Иван VI", 1));
                listQuestions.Add(new Question("Какой историк распространил термин 'эпоха дворцовых переворотов' на весь 18-й век?", "Сергей Соловьёв", "Василий Ключевский", "Николай Карамзин", "Александр Герцен", 2));
                listQuestions.Add(new Question("По какому принципу назначался наследник престола по 'Указу о престолонаследии'?", "Самим монархом", "По мужской линии", "По женской линии", "По выборам", 1));
                listQuestions.Add(new Question("Кто был готов к скорой смерти государя?", "Высшие сановники империи", "Простые люди", "Крестьяне", "Рабочие", 1));
                break;
            case 6:
                listQuestions.Add(new Question("Кто руководил придворными делами после смерти Екатерины I?", "Петр II", "Александр Меншиков", "Мария Меншикова", "Дмитрий Голицын", 2));
                listQuestions.Add(new Question("Кто был внуком первого российского императора?", "Петр II", "Петр I", "Александр I", "Николай I", 1));
                listQuestions.Add(new Question("Кто отправился в ссылку после того, как его изгнали с придворных дел?", "Петр II", "Екатерина I", "Дмитрий Голицын", "Александр Меншиков", 4));
                listQuestions.Add(new Question("Какая болезнь помогла Долгоруковым избавиться от влияния Александра Меншикова?", "Грипп", "Оспа", "Корь", "Скарлатина", 2));
                listQuestions.Add(new Question("Какое событие стало причиной уменьшения влияния Долгоруковых на дворе?", "Избрание Петра II на царство", "Смерть Екатерины I", "Убийство Александра Меншикова", "Смерть Петра II от оспы", 4));
                listQuestions.Add(new Question("Кого пригласил Верховный Тайный Совет на царствование после смерти Петра II?", "Екатерину I", "Дмитрия Голицына", "Анну Иоанновну", "Петра III", 3));
                listQuestions.Add(new Question("Где жила Анна Иоанновна до своего приглашения на царство?", "В Москве", "В Санкт-Петербурге", "В Курляндии", "Во Франции", 3));
                listQuestions.Add(new Question("Кто составил 'кондиции' для Анны Иоанновны?", "Екатерина Долгорукова", "Верховный Тайный Совет", "Александр Меншиков", "Дмитрий Голицын", 2));
                listQuestions.Add(new Question("Кто оказывал большое влияние на Петра II?", "Екатерина I", "Петр Великий", "Александр Меншиков", "Александр Данилович", 3));
                listQuestions.Add(new Question("Почему ни Петр II, ни старая аристократия не были довольны Александром Меншиковым?", "Он был некоронованным царём", "Он был слишком молодым", "Он был неспособным", "Он был из другой страны", 1));
                listQuestions.Add(new Question("Кто убедил Петра II избавиться от влияния Александра Меншикова?", "Дмитрий Голицын", "Екатерина Долгорукова", "Мария Меншикова", "Долгоруковы", 4));
                listQuestions.Add(new Question("Какая болезнь стала причиной смерти Петра II?", "Туберкулез", "Оспа", "Грипп", "Корь", 2));
                listQuestions.Add(new Question("Почему провалилась «затейка верховников»?", "Военные опасались, что Верховный Тайный Совет попытается узурпировать власть", "Большинство дворян и аристократов не поддерживали Верховный Тайный Совет", "Анна Иоанновна пришла к выводу, что Верховный Тайный Совет не работает", "Верховный Тайный Совет сам решил распуститься", 2));
                listQuestions.Add(new Question("Кто приобрел всё большее влияние при дворе во время правления Анны Иоанновны?", "Русские дворяне", "Военные", "Иностранцы", "Крестьяне", 3));
                listQuestions.Add(new Question("Кто фактически вел государственные дела при Анне Иоанновне?", "Армия", "Правительство", "Курляндский фаворит", "Русские дворяне", 3));
                listQuestions.Add(new Question("Что произошло после смерти Анны Иоанновны?", "Начался новый виток закулисной борьбы за власть", "В России установился период стабильности", "Иностранцы были вынуждены покинуть Россию", "Русские дворяне взяли на себя управление страной", 1));
                listQuestions.Add(new Question("Кто занял должность регента при Иоанне Антоновиче?", "Андрей Остерман", "Бирон", "Елизавета Петровна", "Анна Леопольдовна", 2));
                listQuestions.Add(new Question("Кто поддержал Анну Иоанновну при упразднении Верховного Тайного Совета?", "Большинство дворян и аристократов", "Мелкие дворяне и аристократы", "Военные", "Народ", 2));
                listQuestions.Add(new Question("Кто стал регентом при Иоанне Антоновиче после смерти Анны Иоанновны?", "Бирон", "Христофор Миних", "Андрей Остерман", "Анна Леопольдовна", 4));
                listQuestions.Add(new Question("Какое положение дел не устраивало русскую аристократию и гвардейцев?", "Засилье немцев в управлении страной", "Слабое экономическое развитие", "Отсутствие войны", "Низкий уровень культуры", 1));
                break;
            case 7:
                listQuestions.Add(new Question("Кого Россия возвела на трон Речи Посполитой в 1735 году?", "Курфюрста Августа III", "Короля Станислава II Августа", "Князя Януша Радзивилла", "Князя Дмитрия Голицына", 1));
                listQuestions.Add(new Question("Какая война закончилась Белградским мирным договором?", "Великая Северная война", "Семилетняя война", "Русско-турецкая война", "Война за австрийское наследство", 3));
                listQuestions.Add(new Question("Какое положение Россия стремилась урегулировать во время правления Анны Иоанновны?", "Балтийское", "Черноморское", "Каспийское", "Арктическое", 2));
                listQuestions.Add(new Question("Какая война привела к поражению Стокгольма и потере ей приграничных крепостей в Финляндии?", "Великая Северная война", "Семилетняя война", "Русско-турецкая война", "Война шляп", 4));
                listQuestions.Add(new Question("Какой страной была верна Россия при приходе Елизаветы Петровны к власти?", "Франция", "Англия", "Германия", "Испания", 1));
                listQuestions.Add(new Question("Кто продолжил политику, намеченную Петром Великим, несмотря на нестабильность во внутренних делах?", "Анна Иоанновна", "Елизавета Петровна", "Екатерина II", "Александр I", 2));
                listQuestions.Add(new Question("Какое событие помогло России вернуть в свои владения Азов после Прутского похода Петра Великого?", "Русско-турецкая война 1735−1739 годов", "Победа в Великой Северной войне", "Семилетняя война", "Война за польское наследство", 1));
                listQuestions.Add(new Question("С кем продолжалось противостояние России в 18-м веке?", "Германией", "Францией", "Швецией", "Англией", 3));
                listQuestions.Add(new Question("Что вызвало «войну шляп» в начале правления Елизаветы Петровны?", "Голод", "Шведский реваншизм", "Восстание крестьян", "Финансовый кризис", 2));
                listQuestions.Add(new Question("Кто победил в «войне шляп»?", "Россия", "Швеция", "Ничья", "Война не состоялась", 1));
                listQuestions.Add(new Question("Какую политику продолжала Россия на международной арене в эпоху дворцовых переворотов?", "Стремилась воевать с другими государствами", "Стремилась укрепить свои отношения с другими монархиями", "Изолировала себя от международного сообщества", "Не занималась международной политикой", 2));
                listQuestions.Add(new Question("В результате какой войны Россия ввела себя в число вершителей судеб Европы?", "Великой Отечественной войны", "Войны за польское наследство", "Русско-турецкой войны 1735-1739 годов", "Великой Северной войны", 4));
                listQuestions.Add(new Question("Какую проблему Россия стремилась решить во время правления Анны Иоанновны?", "Проблему черноморских государств", "Проблему с Швецией", "Проблему с Пруссией", "Проблему с Турцией", 4));
                listQuestions.Add(new Question("Что вернула Россия в свои владения после Русско-турецкой войны 1735-1739 годов?", "Крым", "Азов", "Кавказ", "Сибирь", 2));
                listQuestions.Add(new Question("Какой конфликт стал самым крупным в 18-м веке для России?", "Русско-шведская война", "Семилетняя война", "Война за польское наследство", "Великая Северная война", 2));
                break;
            case 9:
                listQuestions.Add(new Question("Сколько губерний было до и после губернской реформы?", "Вместо 25 было образовано 51", "Вместо 23 было образовано 52", "Вместо 23 было образовано 50", "Вместо 22 было образовано 50", 3));
                listQuestions.Add(new Question("На сколько департаментов был поделен Сенат после Сенатской реформы?", "На 5", "На 4", "На 6", "На 7", 3));
                listQuestions.Add(new Question("Кем провозгласил себя донской казак Емельян Пугачев в сентябре 1773 г.?", "Петром II Алексеевичем", "Петром III Федоровичем", "Павлом I Петровичем", "Иваном ⅤI Антоновичем", 2));
                listQuestions.Add(new Question("Как называлось правительственное учреждение, занимавшееся изъятыми у церкви в государственную казну землями и крестьянами?", "Синод", "Коллегия экономии", "Уложенная комиссия", "Сенат", 2));
                listQuestions.Add(new Question("В каком году была издана 'Жалованная грамота дворянству'?", "В 1785 г.", "В 1780 г.", "В 1786 г.", "В 1783 г.", 1));
                listQuestions.Add(new Question("В ходе  Сенатской реформы Сенат лишился:", "Административной функции", "Контроля судебных дел в столице", "Законодательной функции", "Рассмотрения политических и государственных дел в Москве", 3));
                listQuestions.Add(new Question("Какая реформа была проведена вследствие пугачевского бунта?", "Городская", "Губернская", "Полицейская", "Образовательная", 2));
                listQuestions.Add(new Question("В каком году был издан указ, разрешавший лицам всех сословий заниматься любыми видами производства и торговли?", "В 1774 г.", "В 1775 г.", "В 1772 г.", "В 1776 г.", 2));
                listQuestions.Add(new Question("С кем из крупнейших мыслителей европейского Просвещения Екатерина II состояла в переписке?", "Вольтер и Д. Дидро", "Лейбниц и Вольтер", "Ж. де Ламетри и Д. Дидро", "Гельвеций и Лейбниц", 1));
                listQuestions.Add(new Question("Какая идея французских философов отвергалась Екатериной II?", "Идея образования", "Идея просвещения", "Идея воспитания", "Идея о равенстве всех от рождения", 4));
                break;
            case 10:
                listQuestions.Add(new Question("Сколько войн против Османской империи было при Екатерине II?", "Одна", "Три", "Две", "Четыре", 3));
                listQuestions.Add(new Question("В союзе с какими странами Россия участвовала в разделе соседней Речи Посполитой?", "С Османской Империей и Пруссией", "С Пруссией и Австрией", "С Австрией и Италией", "С Швецией и Османской империей", 2));
                listQuestions.Add(new Question("Что из перечисленного не входило в условия  Кучук-Кайнарджийского мирного договора?", "Получение части черноморского побережья", "Получение степей Причерноморья (Новороссии)", "Получение права иметь свой флот на Черном море", "Получение Крыма и Восточной Грузии", 4));
                listQuestions.Add(new Question("Под командованием какого полководца русская армия одержала две победы на р. Ларга и р. Кагул?", "Петра Панина", "Петра Румянцева", "Александра Суворова", "Григория Орлова", 2));
                listQuestions.Add(new Question("В каком году произошло окончательное присоединение Крыма к России?", "В 1782 г.", "В 1780 г.", "В 1784 г.", "В 1783 г.", 4));
                listQuestions.Add(new Question("Как назывался трактат, заключенный с Восточной Грузией, по которому царь Ираклий II признал власть российской императрицы?", "Георгиевский", "Айгунский", "Грузинский", "Крымский", 1));
                listQuestions.Add(new Question("Под командованием какого полководца русские войска разгромили турецкую армию у Фокшан?", "Григория Орлова", "Федора Ушакова", "Петра Панина", "Александра Суворова", 4));
                listQuestions.Add(new Question("В каком году русские войска взяли крепость Измаил?", "В 1790 г.", "В 1791 г.", "В 1789 г.", "В 1788 г.", 1));
                listQuestions.Add(new Question("Что в большей степени способствовало блокировке социальной мобильности и формированию рынка свободной рабочей силы в Российской империи?", "Опора на дворянство", "Постоянные войны", "Крепостное право", "Промышленная революция", 3));
                listQuestions.Add(new Question("Укажите годы второй русско-турецкой войны при Екатерине II.", "1786-1792 гг.", "1787-1791 гг.", "1785-1791 гг.", "1784-1790 гг.", 2));
                break;
            case 11:
                listQuestions.Add(new Question("Как звали княгиню, ставшую первой в мире женщиной возглавившей Академию наук?", "М.П. Душкова", "М.С. Перекусихина", "Е.Р. Дашкова", "А.Н. Нарышкина", 3));
                listQuestions.Add(new Question("В каком году был основан Эрмитаж?", "В 1759 г.", "В 1764 г.", "В 1761 г.", "В 1765 г.", 2));
                listQuestions.Add(new Question("Какой из перечисленных сатирических журналов не принадлежит Н.И. Новикову?", "'Кошелек'", "'Живописец'", "'Трутень'", "'Адская почта'", 4));
                listQuestions.Add(new Question("Открытие чего положило начало женского образования в России?", "Смольного училища благородных девиц", "Высших женских медицинских курсов", "Императорской медико-хирургической академии", "Смольного института благородных девиц", 4));
                listQuestions.Add(new Question("Сколько учебных заведений насчитывалось к концу XVIII в.?", "550", "490", "350", "600", 1));
                listQuestions.Add(new Question("Куда был отправлен в десятилетнюю ссылку А.Н. Радищев?", "На Дальний Восток", "На Урал", "В Сибирь", "На Кавказ", 3));
                listQuestions.Add(new Question("Против чего выступал издатель и публицист Н.И. Новиков?", "Против улучшения положения крепостных крестьян", "Против крепостного права", "Против развития частного предпринимательства", "Против губернской реформы", 2));
                listQuestions.Add(new Question("Как называется главное произведение А.Н. Радищева, изданное в 1790 г.?", "'Путешествие из Петербурга в Москву'", "'Интересное путешествие до Москвы'", "'Путешествие до Петербурга из Москвы'", "'Путешествие из Москвы до Новгорода'", 1));
                listQuestions.Add(new Question("В каком году была основана Публичная библиотека?", "В 1789 г.", "В 1793 г.", "В 1785 г.", "В 1795 г.", 4));
                listQuestions.Add(new Question("Выберите того, кто не принадлежал к эпохе екатерининского времени.", "А.Н. Радищев", "А. Д. Кантемир ", "Н.И. Новиков", "Е.Р. Дашкова", 2));
                break;
        }
        questions = GetRandomizedQuestions(listQuestions);
        questionsCount = questions.Count;
        RefreshQuestion();
    }
}
