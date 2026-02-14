using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOverUiManager : MonoBehaviour
{
    private UIDocument _uiDocument;

    private Button _backToMainMenu;
    private Button _exitGame;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        if (_uiDocument == null || _uiDocument.rootVisualElement == null)
        {
            Debug.LogError("UI Document на " + gameObject.name);
            return;
        }

        _backToMainMenu = _uiDocument.rootVisualElement.Q<Button>("BackToMainMenuButton");
        _exitGame = _uiDocument.rootVisualElement.Q<Button>("ExitGameButton");

        if (_backToMainMenu != null)
            _backToMainMenu.clicked += _backToMainMenu_clicked; 
        else
            Debug.LogError("Кнопка 'StartGameButton' не найдена в UI!");

        if (_exitGame != null)
            _exitGame.clicked += () => Application.Quit();
        else
            Debug.LogError("Кнопка 'ExitGameButton' не найдена!");

    }

    private void _backToMainMenu_clicked()
    {
            GameFlowManager.Instance.MainMenu();
    }
}
