using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject[] ui_Booms;
    public GameObject[] ui_Lifes;

    // 암막
    public Image blackOut;
    float blackOut_value;
    float blackOut_speed;

    // 점수
    public Text scoreText;
    public Text highScoreText;

    public int highScore;
    int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
    }

    // 게임 오버
    public Image gameOverImg;

    // 보스
    public Image hpbarFrame;
    public Image hpbar1;
    public Image hpbar2;
    public float MaxHp1;
    public float MaxHp2;
    public BossController bossController;
    public bool isBossSpawn;

    // 상수 목록
    static string HIGH_SCORE_KEY = "HighScore";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Score = 0;
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        blackOut_value = 1.0f;
        blackOut_speed = 0.5f;

        isBossSpawn = false;
    }

    private void Update()
    {
        if (blackOut_value > 0)
        {
            HideBlackOut();
        }
        if (isBossSpawn)
        {
            BossHpBarCheck();
        }
        if (!isBossSpawn)
        {
            hpbarFrame.gameObject.SetActive(false);
            hpbar1.gameObject.SetActive(false);
            hpbar2.gameObject.SetActive(false);
        }
    }

    // 폭탄 개수를 체크하는 함수
    public void BoomCheck(int boomCnt)
    {
        for (int i = 0; i < ui_Booms.Length; i++)
        {
            ui_Booms[i].SetActive(i < boomCnt);
        }
    }

    // 라이프 개수를 체크하는 함수
    public void LifeCheck(int lifeCnt)
    {
        for (int i = 0; i < ui_Lifes.Length; i++)
        {
            ui_Lifes[i].SetActive(i < lifeCnt);
        }
    }

    void HideBlackOut()
    {
        blackOut_value -= Time.deltaTime * blackOut_speed;
        blackOut.color = new Color(0, 0, 0, blackOut_value);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverImg.gameObject.SetActive(true);

        if (score > highScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
            highScore = score;
        }

        highScoreText.text = highScore.ToString();
    }

    public void ReturnTitle()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
        DestroyManagers();
    }

    void DestroyManagers()
    {
        // 각종 게임 데이터 초기화를 위해
        Destroy(gameObject);
        Destroy(GameManager.instance.gameObject);
        Destroy(SoundManager.instance.gameObject);
    }

    public void BossHpBarCheck()
    {
        hpbarFrame.gameObject.SetActive(true);
        hpbar1.gameObject.SetActive(true);
        hpbar2.gameObject.SetActive(true);

        hpbar1.fillAmount = bossController.hp1 / MaxHp1;
        hpbar2.fillAmount = bossController.hp2 / MaxHp2;
    }
}