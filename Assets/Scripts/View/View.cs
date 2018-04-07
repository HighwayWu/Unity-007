using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class View : MonoBehaviour {

    private Ctrl ctrl;
    private RectTransform titleName;
    private RectTransform menuUI;
    private RectTransform upUI;
    private GameObject retryButton;
    private GameObject gameOverUI;
    private GameObject settingUI;
    private GameObject rankUI;
    private GameObject mute;

    private Text score;
    private Text bestScore;
    private Text gameOverScore;
    private Text rankLastScore;
    private Text rankBestScore;
    private Text rankNumbersGame;

    void Awake()
    {
        Screen.SetResolution(576, 1024, false);

        ctrl = GameObject.FindGameObjectWithTag("Ctrl").GetComponent<Ctrl>();
        titleName = transform.Find("Canvas/Title") as RectTransform;
        menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
        upUI = transform.Find("Canvas/UpUI") as RectTransform;
        retryButton = transform.Find("Canvas/MenuUI/RetryButton").gameObject;
        gameOverUI = transform.Find("Canvas/OverUI").gameObject;
        settingUI = transform.Find("Canvas/SettingUI").gameObject;
        rankUI = transform.Find("Canvas/RankUI").gameObject;
        mute = transform.Find("Canvas/SettingUI/AudioButton/Mute").gameObject;

        score = transform.Find("Canvas/UpUI/NowLabel/NowText").GetComponent<Text>();
        bestScore = transform.Find("Canvas/UpUI/BestLabel/BestText").GetComponent<Text>();
        gameOverScore = transform.Find("Canvas/OverUI/ScoreLabel/Text").GetComponent<Text>();
        rankLastScore = transform.Find("Canvas/RankUI/LastLabel/Text").GetComponent<Text>();
        rankBestScore = transform.Find("Canvas/RankUI/BestLabel/Text").GetComponent<Text>();
        rankNumbersGame = transform.Find("Canvas/RankUI/TimesLabel/Text").GetComponent<Text>();
    }

    public void ShowTitle()
    {
        titleName.gameObject.SetActive(true);
        titleName.DOAnchorPosY(-105.78f, 1f);
    }

    public void HideTitle()
    {
        titleName.DOAnchorPosY(105.78f, 1f)
            .OnComplete(delegate { titleName.gameObject.SetActive(false); });
    }

    public void ShowMenu()
    {
        // 显示UI动画
        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(66.9f, 1f);
    }

    public void HideMenu()
    {
        // 隐藏UI动画 顺便激死来节约性能
        menuUI.DOAnchorPosY(-66.9f, 1f)
            .OnComplete(delegate { menuUI.gameObject.SetActive(false); });
    }

    public void UpdateUpUI(int score, int bestScore)
    {
        this.score.text = score.ToString();
        this.bestScore.text = bestScore.ToString();
    }

    public void ShowUpUI(int score = 0, int bestScore = 0)
    {
        this.score.text = score.ToString();
        this.bestScore.text = bestScore.ToString();
        upUI.gameObject.SetActive(true);
        upUI.DOAnchorPosY(-96.5f, 1f);
    }

    public void HideUpUI()
    {
        upUI.DOAnchorPosY(96.5f, 1f)
            .OnComplete(delegate { upUI.gameObject.SetActive(false); });
    }

    public void ShowRetryButton()
    {
        retryButton.SetActive(true);
    }

    public void ShowGameOverUI(int score = 0)
    {
        gameOverUI.SetActive(true);
        gameOverScore.text = score.ToString();
    }

    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }

    public void OnHomeButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnSettingButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(true);
    }

    public void SetMuteActive(bool isActive)
    {
        mute.SetActive(isActive);
    }

    public void OnSettingUIBackgroundClick()
    {
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(false);
    }

    public void ShowRankUI(int lastScore, int bestScore, int numbersGame)
    {
        this.rankLastScore.text = lastScore.ToString();
        this.rankBestScore.text = bestScore.ToString();
        this.rankNumbersGame.text = numbersGame.ToString();
        rankUI.SetActive(true);
    }

    public void OnRankUIBackgroundClick()
    {
        ctrl.audioManager.PlayCursor();
        rankUI.SetActive(false);
    }
}
