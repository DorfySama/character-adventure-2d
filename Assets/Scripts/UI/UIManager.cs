using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public static bool GamePaused { get; private set; }
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private Player _player;
    private Button _pauseMenuButton;
    private Button _returnButton;
    private Button _restartButton;
    private Button _settingsButton;
    private Button _exitButton;
    private VisualElement _pause;
    private VisualElement _m_HealthBarMask;
    private void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {
        _pause = _uiDocument.rootVisualElement.Q<VisualElement>("Pause");
        _m_HealthBarMask = _uiDocument.rootVisualElement.Q<VisualElement>("HealthBarMask");
        _pauseMenuButton = _uiDocument.rootVisualElement.Q<Button>("PauseButton");
        _returnButton = _uiDocument.rootVisualElement.Q<Button>("Return");
        _restartButton = _uiDocument.rootVisualElement.Q<Button>("Restart");
        _exitButton = _uiDocument.rootVisualElement.Q<Button>("Exit");
        _pauseMenuButton.clicked += _pauseMenuButton_clicked;
        _returnButton.clicked += _returnButton_clicked;
        _restartButton.clicked += _restartButton_clicked;
        _exitButton.clicked += _exitButton_clicked;
        GameInput.Instance.OnPauseMenu += GameInput_OnPauseMenu;
    }
    private void Update()
    {
        HealthChanged();
    }
    private void GameInput_OnPauseMenu(object sender, System.EventArgs e)
    {
        if (GamePaused) 
        {
            _returnButton_clicked();
        }
        else 
        {
            _pauseMenuButton_clicked(); 
        }
    }
    private void HealthChanged()
    {
        float healthRatio = (float)_player.currentHealthPoint / _player.maxHealthPoint;
        float healthPercent = Mathf.Lerp(22, 100, healthRatio);
        _m_HealthBarMask.style.width = Length.Percent(healthPercent);
    }

    private void _exitButton_clicked()
    {
        GameManager.Instance.SaveGame(); 
        GameFlowManager.Instance.MainMenu(); 
    }
    private void _restartButton_clicked()
    {
        Time.timeScale = 1f; 
        GameFlowManager.Instance.StartLevel1();
    }
    private void _pauseMenuButton_clicked()
    {
        GamePaused = true; 
        _pause.style.display = DisplayStyle.Flex; 
        Time.timeScale = 0f;
    }
    private void _returnButton_clicked()
    {
        GamePaused = false; 
        Time.timeScale = 1f; 
        _pause.style.display = DisplayStyle.None; 
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnPauseMenu -= GameInput_OnPauseMenu;
    }
}