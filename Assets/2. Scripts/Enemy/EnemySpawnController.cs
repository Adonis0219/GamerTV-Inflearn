using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    // 생성 위치
    public Transform[] enemySpawns;
    // 적 프리팹
    public GameObject[] enemyGO;
    // 시간을 재는 변수
    float time;

    // 적 생성 시간
    float respawnTime;
    // 적 생성 숫자
    int enemyCount;
    // 랜덤 숫자 변수를 저장하는 배열
    int[] randomCount;
    // 웨이브 >> 추후 사용
    int wave;

    // 보스 관련
    bool bossCreate;
    public GameObject bossGO;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
        respawnTime = 4.0f;
        enemyCount = 5;
        randomCount = new int[enemyCount];
        wave = 0;

        bossCreate = false;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();

        if (wave >= 1 && !bossCreate)
            BossCreate();
    }
    
    /// <summary>
    /// 시간을 체크하는 함수
    /// </summary>
    void Timer()
    {
        time += Time.deltaTime;

        if (time > respawnTime)
        {
            RandomPos();
            EnemyCreate();
            wave++;
            time -= time;
        }
    }

    void RandomPos()
    {
        // 랜덤 위치를 위한 숫자
        for (int i = 0; i < enemyCount; i++)
        {
            randomCount[i] = Random.Range(0, 9);
        }
    }
    
    void EnemyCreate()
    {
        if (GameManager.instance.LifeCnt < 0)
            return;

        for (int i = 0; i < enemyCount; i++)
        {
            // 랜덤한 적 선택
            int tmpCnt = Random.Range(0, enemyGO.Length);
            // 생성
            GameObject tmp = GameObject.Instantiate(enemyGO[tmpCnt]);
            // 위치
            tmp.transform.position = enemySpawns[randomCount[i]].position;
            // 동일 위치 방지를 위한 조정
            float tmpX = tmp.transform.position.x;
            float result = Random.Range(tmpX - 2.0f, tmpX + 2.0f);
            tmp.transform.position = new Vector3(result, tmp.transform.position.y, tmp.transform.position.z);
        }
    }

    void BossCreate()
    {
        bossCreate = true;
        GameObject tmp = Instantiate(bossGO);
        int randCnt = Random.Range(0, 9);
        tmp.transform.position = enemySpawns[randCnt].position;

        BossController bossController = tmp.GetComponent<BossController>();

        UIManager.instance.isBossSpawn = true;
        UIManager.instance.bossController = bossController;
        UIManager.instance.MaxHp1 = bossController.hp1;
        UIManager.instance.MaxHp2 = bossController.hp2;
    }
}
