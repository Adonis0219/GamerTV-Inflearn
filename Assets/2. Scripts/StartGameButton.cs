using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
}