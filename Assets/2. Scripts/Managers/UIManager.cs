using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject[] ui_Booms;
    public GameObject[] ui_Lifes;

    // 점수
    public Text scoreText;

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
    }

    // 폭탄 개수를 체크하는 함수
    public void BoomCheck(int boomCnt)
    {
        for (int i = 0; i < ui_Booms.Length; i++)
        {
            ui_Booms[i].SetActive(i <  boomCnt);
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
}
