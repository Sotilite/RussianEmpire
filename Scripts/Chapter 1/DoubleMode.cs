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
            case 16:
                questionsLoad.Add(new Question("Сын Екатерины II, Павел Петрович, был единственным наследником престола?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Екатерина II была недовольна своим сыном?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Екатерина II считала, что Павел Петрович никогда не будет годен к власти?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Павел был образованным человеком, который знал несколько иностранных языков?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Мария была первой женой Павла I?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Павел I больше всего любил армию?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Павел I предпочитал конфликты и гражданскую войну в своей политике?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Был ли акт о престолонаследии, обнародованный Павлом I в 1797 году, важным документом для России?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Было ли восстановлено петровские коллегии Павлом I?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Был ли акт о престолонаследии Павла I успешным для России?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Бумага о престолонаследии Петра Великого была найдена и использована для восстановления справедливости?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Был ли швейцарский поход А.В. Суворова успешно завершен?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Обратился ли Наполеон к Павлу I с лестным письмом и возвратил всех русских пленных?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Разорвал ли Павел I союзнические отношения с Британией и Австрией в 1800 г.?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Первая антипавловская 'конспирация' возникла сразу после восшествия Павла I на престол?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Цесаревич Александр Павлович не был одним из главных заговорщиков во второй антипавловской 'конспирации'?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("В третьей антипавловской 'конспирации' были заговорщики, связанные с графом Н.П. Паниным и английским посолом лордом Ч. Уитвортом?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Группа заговорщиков, ворвавшаяся в Михайловский замок, задушила Павла I в ночь с 11 на 12 марта 1801 года?", "Да", "Нет", 1));
                break;
            case 20:
                questionsLoad.Add(new Question("В 1812 году император отстранил Сперанского от всех должностей и сослал в Нижний Новгород.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("На военном совете в Филях после Бородинского сражения было решено дать новое генеральное сражение под Москвой.", "Да", "Нет", 2));
                questionsLoad.Add(new Question("В 1814–1825 во внутренней политике Александра I усилились либеральные тенденции", "Да", "Нет", 2));
                questionsLoad.Add(new Question("После отступления и пересечения р. Неман численность французской армии составляла около 1600 человек", "Да", "Нет", 1));
                questionsLoad.Add(new Question("В целях укрепления государственного аппарата в 1802 г. вместо коллегий учреждается 6 министерств", "Да", "Нет", 2));
                questionsLoad.Add(new Question("В царствование Александра стали выходить журналы, которые во многом изменили общественно-политическое и культурное лицо России", "Да", "Нет", 1));
                questionsLoad.Add(new Question("В Священный союз входили такие страны, как Россия, Пруссия и Австрия", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Русско-турецкая война при Александре I завершилась поражением России", "Да", "Нет", 2));
                questionsLoad.Add(new Question("При вступлении на престол Александр I в своем Манифесте прямо подчеркнул приверженность идеям политического курса своего отца Павла I", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Результатом восстановления палочной дисциплины стали волнения в 1820 году в Семеновском полку", "Да", "Нет", 1));
                break;
            case 24:
                questionsLoad.Add(new Question("Николай 1 был не военным человеком?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Связано ли правление Николая 1 с попытками перенести армейские порядки и обычаи на повседневную жизнь и управление страной? ", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Стремился ли Николай 1 привнести порядок во все органы государственной власти?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Укрепилась ли полицейская и бюрократическая власть в николаевскую эпоху?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("У полицейских III отделения была ограниченная власть?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Война началась из-за споров между католиками и православными за право контролировать христианские святыни в Иерусалиме, Вифлееме и Назарете", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Черноморская русская эскадра уничтожила турецкий флот?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Российская армия под руководством генерала Меншикова одержала победу в Крыму?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Россия потеряла право держать военные корабли и укрепления на Черном море?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Активно ли Россия вмешивалась в дела Турции?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Греция не была провозглашена независимой при активном участии России?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Стал ли И. Каподистрия первым президентом Греции?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Разгорелась ли очередная русско-турецкая война в 1828-1829 гг.?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Просуществовал ли русский сентиментализм долго?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Был ли романтизм широко распространен в России?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Являлась ли Академия художеств консервативным и косным учреждением?", "Да", "Нет", 1));
                break;
            case 28:
                questionsLoad.Add(new Question("Главный идеолог пропагандистского направления – П. Лавров.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Начало правления Александра II совпало с победой в Крымской войне.", "Да", "Нет", 2));
                questionsLoad.Add(new Question("В 1861 году было отменено крепостное право.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Одной из наиболее последовательных реформ была военная реформа (1874), подготовкой которой занимался М. Рейтерн.", "Да", "Нет", 2));
                questionsLoad.Add(new Question("В пореформенный период получают окончательное оформление три направления в общественном движении – консерваторы, либералы и радикалы.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Александра II пытались убить четыре раза, но роковым стало пятое покушение, произошедшие в 1881 году, после которого император скончался.", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Александр II и его окружение посчитали нерентабельной Аляску, поэтому в 1867 году продали ее США.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("После победы в русско-турецкой войне в состав Российской империи вошли Румыния, Сербия и Черногория", "Да", "Нет", 2));
                questionsLoad.Add(new Question("В 1867-1873 годах территория России увеличилась за счет завоевания Туркестанского края и Ферганской долины и добровольного вхождения на вассальных правах Бухарского эмирата и Хивинского ханства.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("С 1860-1864 гг. проводилась финансовая реформа, разработкой которой занимался Д. Милютин.", "Да", "Нет", 2));
                break;
            case 32:
                questionsLoad.Add(new Question("Ставил ли Александр III целью улучшение начатых реформ?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Считают ли некоторые историки преобразования, введенные при Александре III, контрреформами?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Был ли проект конституции, подготовленный при Александре III, принят?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Предоставило ли Положение об усиленной и чрезвычайной охране право местным властям арестовывать «подозрительных лиц» без суда?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Временные правила для органов печати усилили цензуру?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Ужесточился ли контроль над высшими учебными заведениями при Александр III?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Для поддержания помещичьих хозяйств был создан Дворянский земельный банк?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Установление порядка в стране означало ущемление прав простого народа?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("При Александр III запретили использовать труд детей до 12 лет на промышленных предприятиях?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("С 1885 года воспрещались ночные работы женщин и несовершеннолетних?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Существовал контроль за условиями труда рабочих в странах Европы?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Проводилась ли русификация под лозунгом «Россия для русских» при Александр III?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Принимал ли участие Александр III в русско-турецкой войне?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Были ли крупные вооруженные конфликты у России в период правления Александра III?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Был ли заключен военно-политический союз между Россией и Францией в период правления Александра III?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Одобрял ли Достоевский политику Александра III?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Сводилась ли политика Александра III к изменению традиций и идеалов России?", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Александр III оказал большое влияние на развитие отечественной культуры и науки?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Александр III лично возглавлял Русское императорское историческое общество?", "Да", "Нет", 1));
                questionsLoad.Add(new Question("Александр III планировал создать русский национальный театр во главе с Островским?", "Да", "Нет", 1));
                break;
            case 36:
                questionsLoad.Add(new Question("Отречение от престола Николая ІІ случилось 2 марта 1917 года.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("В январе 1897 года была проведена денежная реформа министром финансов П.А. Столыпиным.", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Россия в 1900-1903 гг. переживает первый в своей истории кризис перепроизводства.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("К 1917 году 55% населения империи оставалось абсолютно безграмотным.", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Решение малоземелья происходило путем переселения крестьян в малозаселенные регионы — Сибирь, Среднюю Азию и Кавказ.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("В августе 1913 года началось наступление русских войск на Восточную Пруссию.", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Российский флот потерпел неожиданное и сокрушительное поражение в Цусимском сражении.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("В 1900 г. Россия и Китай заключили договор об аренде Ляодунского полуострова.", "Да", "Нет", 2));
                questionsLoad.Add(new Question("Во время проведения коронации Николая II произошла Ходынская трагедия, по причине плохой организации народных гуляний, что привело к давке, и погибло 1379 человек.", "Да", "Нет", 1));
                questionsLoad.Add(new Question("17 июля 1919 года стал днём, когда чудовищное убийство семейства последнего императора положило конец всем надеждам на прежний путь монархии в России.", "Да", "Нет", 2));
                break;
        }
        questions = GetRandomizedQuestions(questionsLoad);
        questionsCount = questions.Count;
        RefreshQuestion();
    }

}
