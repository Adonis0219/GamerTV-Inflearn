using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerPrefab;
    public PlayerController playerController;
    public Vector3 playerPos;

    int lifeCnt;
    public int LifeCnt
    {
        get => lifeCnt;
        set
        {
            lifeCnt = value;
            UIManager.instance.LifeCheck(lifeCnt);
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LifeCnt = 2;

        CreatePlayer();
    
    }

    // 플레이어 생성
    public void CreatePlayer()
    {
        if (LifeCnt < 0)
            return;

        GameObject player = Instantiate(playerPrefab);
        playerPos = new Vector3(Random.Range(-9.0f, 9.0f), -18f, 0);
        player.transform.position = playerPos;
        playerController = player.GetComponent<PlayerController>();

        UIManager.instance.BoomCheck(playerController.Boom);
    }

    // 라이프 감소
    public void PlayerLifeRemove()
    {
        LifeCnt--;
    }
}
