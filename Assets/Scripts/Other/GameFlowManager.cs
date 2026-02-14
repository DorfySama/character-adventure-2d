using UnityEngine;
using UnityEngine.SceneManagement;
public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance { get; private set; }
    private const string _MAINMENU = "MainMenu";
    private const string _LEVEL1 = "Level1";
    private const string _GAMEOVER = "GameOver";
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return; 
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(_MAINMENU);
    }
    public void StartLevel1()
    {
        SceneManager.LoadScene(_LEVEL1);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(_GAMEOVER);
    }
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("Попытка загрузить пустую сцену!");
            sceneName = _LEVEL1;
        }
        SceneManager.LoadScene(sceneName);
    }
}