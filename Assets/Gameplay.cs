﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkcupGames;
using UnityEngine.SceneManagement;
using Spine.Unity;
using TMPro;
using YG;

public enum GameplayType { Erase, Draw, Find }

public class Gameplay : MonoBehaviour {
    public static Gameplay Instance;

    public GameplayType GameplayType { get; private set; }

    public List<ParticleSystem> effects;
    public List<RectTransform> findBoxs;
    public GameObject iconTick;
    public GameObject guideObject;
    public UIEffect winPopup;
    public Image findItemDemo;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtQuestion;
    public GameObject closeSpecialLevelButton;
    public GameObject popUpRating;
    public AudioClip winSound;
    public Image scanImg;
    public Canvas canvasGameplay;
    public Button homeButton;
    public Button settingButton;
    public DrawManager drawManager;
    public Text playerLevel;
    public Sprite eraseObject;
    public Sprite findObject;
    public Sprite drawObject;
    public bool isBranchLevel;
    public Sprite cucgom;
    public int maxLevel;
    [SerializeField] private GameObject levelObject;
    bool won = false;
    bool drawLevel;

    public bool isPlayingSpecial;

    private void Awake() {
        Instance = this;

        for (int i = 0; i < effects.Count; i++) {
            effects[i].Stop();
        }
        if (findItemDemo)
            findItemDemo.gameObject.SetActive(false);

        homeButton.onClick.AddListener(() => { SceneManager.LoadScene("Home"); });
        drawManager.gameObject.SetActive(false);

        AudioSystem.Instance.SetBGM(GameSystem.userdata.playBGM);
        AudioSystem.Instance.SetFXSound(GameSystem.userdata.playSound);

        iconTick.SetActive(false);
        GameSystem.LoadUserData();
        int level = GameSystem.userdata.level;
        int count = 0;
        won = false;
        isPlayingSpecial = false;
        if (isBranchLevel) {
            GameObject obj = Resources.Load<GameObject>("LevelBranch/Level" + GameSystem.userdata.branchLevel);
            Debug.Log(GameSystem.userdata.branchLevel);
            txtLevel.text = null;
            txtQuestion.text = null;
            if (obj != null) {
                levelObject = Instantiate(obj);
                scanImg.gameObject.SetActive(false);
                var findItem = FindObjectOfType<FindItemLevel>();

                for (int i = 0; i < findBoxs.Count; i++) {
                    findBoxs[i].gameObject.SetActive(false);
                }
                return;
            }
        }

        while (count < 10) {
            GameObject obj = Resources.Load<GameObject>("Levels/Level" + (level + count + 1));
            closeSpecialLevelButton.SetActive(false);
            if (YandexGame.EnvironmentData.language == "ru")
            {
                txtLevel.text = "УРОВЕНЬ " + (level + count + 1);
            }
            if (YandexGame.EnvironmentData.language == "en")
            {
                txtLevel.text = "LEVEL " + (level + count + 1);
            }
            if (YandexGame.EnvironmentData.language == "tr")
            {
                txtLevel.text = "DÜZEY " + (level + count + 1);
            }
            txtQuestion.text = "";
            if (DataManager.Instance.levelInfos.Count > level + count) {
                LevelInfo info = DataManager.Instance.levelInfos[level + count];

                if (GameSystem.userdata.level == 24) {
                    txtQuestion.text = null;
                } else {
                    txtQuestion.text = info.levelTitle;
                }
            }

            if (obj != null) {
                levelObject = Instantiate(obj);
                GameSystem.userdata.level = level + count;
                GameSystem.SaveUserDataToLocal();
                break;
            } else {
                count++;
            }
        }

        if (levelObject == null) {
            GameSystem.userdata.level = 0;
            GameSystem.SaveUserDataToLocal();
            levelObject = Resources.Load<GameObject>("Levels/Level1");
            if (YandexGame.EnvironmentData.language == "ru")
            {
                txtLevel.text = "УРОВЕНЬ 1";
            }
            if (YandexGame.EnvironmentData.language == "en")
            {
                txtLevel.text = "LEVEL 1";
            }
            if (YandexGame.EnvironmentData.language == "tr")
            {
                txtLevel.text = "DÜZEY 1";
            }
          
        }
        if (!GameSystem.userdata.showRating && GameSystem.userdata.level == 5) {
            popUpRating.SetActive(true);
            GameSystem.userdata.showRating = true;
            GameSystem.SaveUserDataToLocal();
        }

        var findItemLevel = FindObjectOfType<FindItemLevel>();
        for (int i = 0; i < findBoxs.Count; i++) {
            findBoxs[i].gameObject.SetActive(findItemLevel != null);
        }

        if (findItemLevel != null || FindObjectOfType<FindAndWinLevel>() != null) {
            GameplayType = GameplayType.Find;
            if (PlayerPrefs.GetInt("tutorial_find", 0) == 0) {
                PlayerPrefs.SetInt("tutorial_find", 1);
                LeanTween.delayedCall(2f, () => {
                    Hint();
                });
            }
            return;
        }

        drawLevel = FindObjectOfType<DrawLevel>();
        if (drawLevel) {
            scanImg.sprite = drawObject;
            drawManager.gameObject.SetActive(true);
            GameplayType = GameplayType.Draw;
            if (PlayerPrefs.GetInt("tutorial_draw") == 0) {
                PlayerPrefs.SetInt("tutorial_draw", 1);
                LeanTween.delayedCall(2f, () => {
                    Hint();
                });
            }
        } else {
            drawManager.gameObject.SetActive(false);
            scanImg.sprite = cucgom;
            GameplayType = GameplayType.Erase;
            if (PlayerPrefs.GetInt("tutorial_erase") == 0) {
                PlayerPrefs.SetInt("tutorial_erase", 1);
                LeanTween.delayedCall(2f, () => {
                    Hint();
                });
            }
        }
    }

    public void LoadBranchLevel() {
        SceneManager.LoadScene("BranchLevel");
    }

    public void Win(LevelManager level, bool showWinPopupImediately = true, bool loopAnimation = true) {
        drawManager.gameObject.SetActive(false);
        if (won) return;
        won = true;

        if (level.animBefore != null) {
            level.animBefore.gameObject.SetActive(false);
        }
        StartCoroutine(IEWin(level.animAfter, level.winAnims, showWinPopupImediately, level.loopAnimation));
    }

    public IEnumerator IEWin(SkeletonAnimation skeletonAnimation, List<string> anims = null, bool showWinPopupImediately = true, bool loopAnimation = true) {
        var erasers = GameObject.FindGameObjectsWithTag("Eraser");
        for (int i = 0; i < erasers.Length; i++) {
            erasers[i].gameObject.SetActive(false);
        }

        skeletonAnimation.maskInteraction = SpriteMaskInteraction.None;
        for (int i = 0; i < effects.Count; i++) {
            effects[i].Play();
        }
        EasyEffect.Appear(iconTick, 0f, 1f, 0.15f);
        AudioSystem.Instance.PlaySound(winSound, 1);
        skeletonAnimation.AnimationState.Data.DefaultMix = 0;

        if (anims != null && anims.Count > 0) {
            for (int i = 0; i < anims.Count; i++) {
                Spine.Animation win = skeletonAnimation.Skeleton.Data.FindAnimation(anims[i]);
                if (win != null) {
                    skeletonAnimation.AnimationState.SetAnimation(0, anims[i], loopAnimation);
                    yield return new WaitForSeconds(win.Duration);
                }
            }
        } else {
            Spine.Animation win = skeletonAnimation.Skeleton.Data.FindAnimation("win");
            if (win != null) {
                if (loopAnimation) {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win", true);
                } else {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win", false);
                }

                yield return new WaitForSeconds(win.Duration);
            }

            Spine.Animation win1 = skeletonAnimation.Skeleton.Data.FindAnimation("win1");
            if (win1 != null) {
                if (loopAnimation) {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win1", true);
                } else {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win1", false);
                }

                yield return new WaitForSeconds(win1.Duration);
            }

            Spine.Animation win2 = skeletonAnimation.Skeleton.Data.FindAnimation("win2");
            if (win2 != null) {
                if (loopAnimation) {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win2", true);
                } else {
                    skeletonAnimation.AnimationState.SetAnimation(0, "win2", false);
                }
                yield return new WaitForSeconds(win2.Duration);
            }
        }
        yield return new WaitForSeconds(1f);

        if (showWinPopupImediately) {
            ShowWinPopup();
        } else {
            LeanTween.delayedCall(1.5f, () => {
                ShowWinPopup();
            });
        }
    }

    public void ShowWinPopup() {
        winPopup.DoEffect();
    }

    public void Hint()
    {
        //var draw = FindObjectOfType<DrawLevel>();
        var drawOur = GameObject.FindGameObjectsWithTag("Find");
        if (drawOur != null)
        {
            //draw.Hint();
            StartCoroutine(DelayedGuidePosition(drawOur[0].transform.position));
            return;
        }

        //var manyTimes = FindObjectOfType<EraseManyTimes>();
        var erase = GameObject.FindGameObjectsWithTag("Find");
        if (erase != null)
        {
            StartCoroutine(DelayedGuidePosition(erase[0].transform.position));
            return;
        }

        //var level = FindObjectOfType<LevelManager>();
        var lvl = GameObject.FindGameObjectsWithTag("Find");
        if (lvl != null)
        {
            StartCoroutine(DelayedGuidePosition(lvl[0].transform.position));
        }
    }

    IEnumerator DelayedGuidePosition(Vector3 position)
    {
        yield return new WaitForSeconds(1f);
        GuidePosition(position);
    }

    public void GuidePosition(Vector2 pos) {
        guideObject.SetActive(true);
        guideObject.transform.position = new Vector2(0, 0);
        LeanTween.move(guideObject, pos, 3f).setEaseOutCubic().setOnComplete(() => {
            guideObject.SetActive(false);
        });
    }

    public int GetSpecialLevel() {
        var userLevel = GameSystem.userdata.level;
        for (int i = 0; i < DataManager.specialLevels.Count; i++) {
            if (userLevel == DataManager.specialLevels[i]) {
                return i + 1;
            }
        }
        return -1;
    }

    public void Next() {
        if (isBranchLevel) {
            SceneManager.LoadScene("Home");
            isBranchLevel = false;
            return;
        }
        if (GetSpecialLevel() > 0 && !isPlayingSpecial) {

            isPlayingSpecial = true;
            SpawnSpecialLevel();
            return;
        }
        if (GameSystem.userdata.level < maxLevel-1)
        {
            GameSystem.userdata.level++;
        }
        else
        {
            GameSystem.userdata.level = 0;
        }
        
        if (GameSystem.userdata.level > GameSystem.userdata.maxLevel && GameSystem.userdata.maxLevel != maxLevel) 
        {
            GameSystem.userdata.maxLevel = GameSystem.userdata.level;
        }
        
        GameSystem.SaveUserDataToLocal();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SpawnSpecialLevel() {
        Destroy(levelObject);
        closeSpecialLevelButton.SetActive(true);
        var obj = Resources.Load<GameObject>("LevelSpecials/Special" + GetSpecialLevel());
        txtLevel.gameObject.SetActive(isPlayingSpecial);
        txtQuestion.gameObject.SetActive(isPlayingSpecial);
        levelObject = Instantiate(obj);
        EasyEffect.Disappear(winPopup.gameObject, 1, 0);
        canvasGameplay.gameObject.SetActive(false);
        won = false;
        GameplayType = GameplayType.Erase;
        var iconManager = FindObjectOfType<IconManager>();
        if (iconManager) {
            iconManager.Init();
        }
    }

    public void LevelUp() {
        closeSpecialLevelButton.SetActive(false);
        GameSystem.userdata.level++;
        GameSystem.SaveUserDataToLocal();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Destroy(levelObject);
        //var obj = Resources.Load<GameObject>("Levels/Level" + GameSystem.userdata.level);
        //txtLevel.text = "LEVEL " + GameSystem.userdata.level;
        //levelObject = Instantiate(obj);
        //EasyEffect.Disappear(winPopup.gameObject, 1, 0);
    }

    public void FoundItem(SpriteRenderer renderer) {
        Transform emptyBox = null;

        for (int i = 0; i < findBoxs.Count; i++) {
            Transform box = findBoxs[i].transform;
            if (box.childCount == 0) {
                emptyBox = box;
                break;
            }
        }
        if (emptyBox == null) return;
        StartCoroutine(IEFoundItem(renderer, emptyBox));
    }

    IEnumerator IEFoundItem(SpriteRenderer renderer, Transform emptyBox) {
        var demo = Instantiate(findItemDemo);

        demo.transform.position = Camera.main.WorldToScreenPoint(renderer.transform.position);
        demo.sprite = renderer.sprite;
        demo.transform.SetParent(emptyBox);
        demo.gameObject.SetActive(true);

        EasyEffect.Appear(demo.gameObject, 0f, 1f);

        yield return new WaitForSeconds(1f);

        LeanTween.move(demo.gameObject, emptyBox.transform.position, 1f).setDelay(.5f).setEaseOutCubic();
    }

    public void AddGold(int amount) {
        GameSystem.userdata.gold += amount;
        GameSystem.SaveUserDataToLocal();
    }

    public void Virate() {
        /*if (GameSystem.userdata.virate)
            Handheld.Vibrate();*/
    }
}
