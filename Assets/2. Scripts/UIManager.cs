using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject[] ui_Booms;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // 폭탄 개수를 체크하는 함수
    public void BoomCheck(int boomCnt)
    {
        for (int i = 0; i < ui_Booms.Length; i++)
        {
            ui_Booms[i].SetActive(i <  boomCnt);
        }
    }
}
