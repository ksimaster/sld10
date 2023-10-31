using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using YG;
public enum LevelType {
    Draw, Erase,Drag 
}

public class LevelInfo {
    public int levelId;
    public int levelIdDisplay;
    public string levelTitle;
    public bool unlocked;
    public LevelType levelType;
    public System.Action btnUse;
    public GameObject rewardButton;
    public Sprite normalImage;
    public Sprite disableImage;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public long Ticks;
    public long nextRewards;
    public int Day = 0;
    public static List<int> specialLevels = new List<int>() { 2, 7, 12, 17, 22 };
    public List<LevelInfo> levelInfos;
    public Button claimGiftButton;
    public Button soundButton;
    public Button bgmButton;
    public Button vibrateButton;

    public Sprite normalImage;
    public Sprite disableImage;
    private void Awake() {
        GameSystem.LoadUserData();
        Instance = this;
        Init();
    }

    public void Init() {
        levelInfos = new List<LevelInfo>();

        List<string> titles = new List<string>();

        if (YandexGame.EnvironmentData.language == "ru")
        {
            titles.Add("Снимаю маску");
            titles.Add("Помоги ей согреться");
            titles.Add("На что смотрит эта девушка?");
            titles.Add("Найди вторую девушку");
            titles.Add("Помоги ему поднять вес");
            titles.Add("Поймай грабителя");
            titles.Add("Сделай ее счастливой");
            titles.Add("Помоги им расслабиться");
            titles.Add("Сделай фермера счастливым");
            titles.Add("Где находится женский туалет?");
            titles.Add("Помоги ведьме преобразиться");
            titles.Add("Сделай это круто!");
            titles.Add("Тайм-аут");
            titles.Add("Чего она боится?");
            titles.Add("Найди удивительную девушку");
            titles.Add("Найди ниндзя");
            titles.Add("Что они делают?");
            titles.Add("Помоги девочке летать");
            titles.Add("Сделай его сексуальным");
            titles.Add("Красивая девушка и фотограф");
            titles.Add("Это настоящий Санта?");
            titles.Add("Как заключенному удалось сбежать?");
            titles.Add("Найди лягушку");
            titles.Add("Подарок-сюрприз");
            titles.Add("Где золото?");
            titles.Add("Почему он волнуется?");
            titles.Add("Избавься от вампира");
            titles.Add("Помоги девушке");
            titles.Add("Найди костюм");
            titles.Add("Используй свое воображение");
            titles.Add("Найдите таинственного человека");
            titles.Add("Проснись!");
            titles.Add("Призови джинна");
            titles.Add("Спаси девушку");
            titles.Add("Помоги мальчику не заснуть");
            titles.Add("Кто настоящий?");
            titles.Add("Помоги ему доставить ей удовольствие");
            titles.Add("Помоги ему");
            titles.Add("Найдите первый полный бак");
            titles.Add("Что она делает?");
            titles.Add("Разблокируйте мой телефон");
            titles.Add("Помоги ему стать певцом");
            titles.Add("Убей монстра");
            titles.Add("Помоги ей сбросить вес");
            titles.Add("Убейте вампиров");
            titles.Add("Ему нужен призрак");
            titles.Add("Помогите послушать музыку");
            titles.Add("Поймай вора");
            titles.Add("Найди букву О");
            titles.Add("Найди букву О");
        }
        if (YandexGame.EnvironmentData.language == "en")
        {
            titles.Add("Taking Off The Mask");
            titles.Add("Help her warm up");
            titles.Add("What is the girl looking at?");
            titles.Add("Find the second girl");
            titles.Add("Help him lift the weight");
            titles.Add("Catch the robber");
            titles.Add("Make her happy");
            titles.Add("Help them relax");
            titles.Add("Make the farmer happy");
            titles.Add("Where is a female toilet?");
            titles.Add("Help the witch transform");
            titles.Add("Make it cool!");
            titles.Add("Time out");
            titles.Add("What is she affraid of?");
            titles.Add("Find the suprising girl");
            titles.Add("Find a ninja");
            titles.Add("What are they doing?");
            titles.Add("Help the girl to fly");
            titles.Add("Make him sexy");
            titles.Add("Beautiful girl and photographer");
            titles.Add("Is this the real Santa?");
            titles.Add("How did the prisoner escape?");
            titles.Add("Find the frog");
            titles.Add("Surprise gift");
            titles.Add("Where is the gold?");
            titles.Add("Why is he worried?");
            titles.Add("Get the rid of vampire");
            titles.Add("Help the girl");
            titles.Add("Find the costume");
            titles.Add("Use your imagination");
            titles.Add("Find the mysterious person");
            titles.Add("Wake up!");
            titles.Add("Summon the genie");
            titles.Add("Save the girl");
            titles.Add("Help the boy stay awake");
            titles.Add("Who is real?");
            titles.Add("Help him please her");
            titles.Add("Help him");
            titles.Add("Find the first full tank");
            titles.Add("What is she doing?");
            titles.Add("Unlock My Phone");
            titles.Add("Help him a singer");
            titles.Add("Kill the monster");
            titles.Add("Help her lose weight");
            titles.Add("Kill The Vampires");
            titles.Add("He wants a ghost");
            titles.Add("Help listen to the music");
            titles.Add("Catch the thief");
            titles.Add("Find the letter O");
            titles.Add("Find the letter O");
        }
        if (YandexGame.EnvironmentData.language == "tr")

        {
            titles.Add("Maskeyi Çıkarmak");
            titles.Add("Isınmasına yardım et");
            titles.Add("Kız neye bakıyor?");
            titles.Add("İkinci kızı bul");
            titles.Add("Ağırlığı kaldırmasına yardım et");
            titles.Add("Soyguncuyu yakala");
            titles.Add("Onu mutlu et");
            titles.Add("Rahatlamalarına yardım et");
            titles.Add("Çiftçiyi mutlu et");
            titles.Add("Kadın tuvaleti nerede?");
            titles.Add("Cadının dönüşmesine yardım et");
            titles.Add("Havalı yap!");
            titles.Add("Zaman aşımı");
            titles.Add("Neden korkuyor?");
            titles.Add("Şaşırtıcı kızı bul");
            titles.Add("Bir ninja bul");
            titles.Add("Ne yapıyorlar?");
            titles.Add("Kızın uçmasına yardım et");
            titles.Add("Onu seksi yap");
            titles.Add("Güzel kız ve fotoğrafçı");
            titles.Add("Bu gerçek Noel Baba mı?");
            titles.Add("Mahkum nasıl kaçtı?");
            titles.Add("Kurbağayı bul");
            titles.Add("Sürpriz hediye");
            titles.Add("Altın nerede?");
            titles.Add("Neden endişeleniyor?");
            titles.Add("Vampirden kurtul");
            titles.Add("Kıza yardım et");
            titles.Add("Kostümü bul");
            titles.Add("Hayal gücünüzü kullanın");
            titles.Add("Gizemli kişiyi bulun");
            titles.Add("Uyan!");
            titles.Add("Cini çağır");
            titles.Add("Kızı kurtar");
            titles.Add("Çocuğun uyanık kalmasına yardım et");
            titles.Add("Gerçek kim?");
            titles.Add("Onu memnun etmesine yardım et");
            titles.Add("Ona yardım et");
            titles.Add("İlk dolu tankı bulun");
            titles.Add("O ne yapıyor?");
            titles.Add("Telefonumun Kilidini Aç");
            titles.Add("Ona bir şarkıcıya yardım et");
            titles.Add("Canavarı öldür");
            titles.Add("Kilo vermesine yardım et");
            titles.Add("Vampirleri Öldürün");
            titles.Add("Bir hayalet istiyor");
            titles.Add("Müziği dinlemeye yardım et");
            titles.Add("Hırsızı yakalayın");
            titles.Add("O harfini bulun");
            titles.Add("O harfini bulun");
        }


        //1


        /*   titles.Add("Taking Off The Mask");
           titles.Add("Help her warm up");
           titles.Add("What is the girl looking at?");
           titles.Add("Find the second girl");
           titles.Add("Help him lift the weight");

           //6
           titles.Add("Catch the robber");
           titles.Add("Make her happy");
           titles.Add("Help them relax...");
           titles.Add("Make the farmer happy");
           titles.Add("Where is a female toilet?");

           //11
           titles.Add("Help the witch transform");
           titles.Add("Make it cool!");
           titles.Add("Time out");
           titles.Add("What is she affraid of?");
           titles.Add("Find the suprising girl");

           //16
           titles.Add("Find a ninja");
           titles.Add("What are they doing?");
           titles.Add("Help the girl to fly");
           titles.Add("Make him sexy");
           titles.Add("Beautiful girl and photographer.");

           //21
           titles.Add("Is this the real Santa?");
           titles.Add("How did the prisoner escape?");
           titles.Add("Find the frog.");
           titles.Add("Surprise gift.");
           titles.Add("Where is the gold?");

           //26
           titles.Add("Why is he worried?");
           titles.Add("Get the rid of vampire.");
           titles.Add("Help the girl");
           titles.Add("Find the costume.");
           titles.Add("Use your imagination");

           //31
           titles.Add("Find the mysterious person.");
           titles.Add("Wake up!");
           titles.Add("Summon the genie.");
           titles.Add("Save the girl.");
           titles.Add("Help the boy stay awake.");

           //36
           titles.Add("Who is real?.");
           titles.Add("Help him please her.");
           titles.Add("Help him.");
           titles.Add("Find the first full tank.");
           titles.Add("What is she doing?");

           //41
           titles.Add("Unlock My Phone");
           titles.Add("Help him a singer");
           titles.Add("Kill the monster");
           titles.Add("Help her lose weight.");
           titles.Add("Kill The Vampires.");

           //46
           titles.Add("He wants a ghost.");
           titles.Add("Help listen to the music.");
           titles.Add("Catch the thief.");
           titles.Add("Find the letter O.");
           titles.Add("Find the letter O.");
        */
        for (int i = 0; i < titles.Count; i++) {
            int index = i;

            levelInfos.Add(new LevelInfo() {
                levelId = i,
                levelIdDisplay = i + 1,
                levelTitle = titles[i],
                unlocked = i < GameSystem.userdata.maxLevel || i == 0,
                btnUse = () => {
                    PlayLevel(index);
                }
            });
        }

       
    }
    private void Start()
    {
        if (vibrateButton)
            vibrateButton.gameObject.SetActive(GameSystem.userdata.virate);
        if (soundButton)
            soundButton.gameObject.SetActive(GameSystem.userdata.playSound);
        if (bgmButton)
            bgmButton.gameObject.SetActive(GameSystem.userdata.playBGM);
    }

    public void PlayLevel(int level) {
        GameSystem.userdata.level = level;
        GameSystem.SaveUserDataToLocal();

        DarkcupGames.Utils.ChangeScene(Constants.SCENE_GAMEPLAY);
    }

    public void ClaimGiftExtend(int day)
    {
        Day += day;
        GameSystem.userdata.nextDay = DateTime.Now.AddDays(Day).Ticks;
        GameSystem.SaveUserDataToLocal();
    }
}
