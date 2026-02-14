using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class UIManagerMainMenu : MonoBehaviour
{
    private UIDocument _uiDocument;
    private Button _startGameButton;
    private Button _exitButton;
    private bool shouldLoadGame = false;
    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        if (_uiDocument == null || _uiDocument.rootVisualElement == null)
        {
            Debug.LogError("UI Document на " + gameObject.name);
            return;
        }
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerAssigned += OnPlayerAssignedHandler;
        }
        else
        {
            Debug.LogWarning("GameManager.Instance ещё не создан во время Awake главного меню");
        }
        _startGameButton = _uiDocument.rootVisualElement.Q<Button>("StartGameButton");
        _exitButton = _uiDocument.rootVisualElement.Q<Button>("ExitGameButton");
        if (_startGameButton != null)
            _startGameButton.clicked += _startGameButton_clicked;
        else
            Debug.LogError("Кнопка 'StartGameButton' не найдена в UI!");

        if (_exitButton != null)
            _exitButton.clicked += () => Application.Quit();
        else
            Debug.LogError("Кнопка 'ExitGameButton' не найдена!");
    }
    private void OnPlayerAssignedHandler()
    {
        if (shouldLoadGame)
        {
            GameManager.Instance.LoadGame(); 
            shouldLoadGame = false;
            Debug.Log("Сохранение успешно применено после появления игрока");
        }
    }

    private void _startGameButton_clicked()
    {
        shouldLoadGame = true;
        Time.timeScale = 1f;
        LoadSceneByIndex(1);
    }

    private void LoadSceneByIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    private void OnDestroy()
    {
        if (_startGameButton != null)
            _startGameButton.clicked -= _startGameButton_clicked;
    }
}