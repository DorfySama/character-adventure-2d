using UnityEngine;
using System;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action OnPlayerAssigned; 
    public Player player { get; set; }
    public PlayerVisual playerVisual { get; set; }    
    private PlayerSaveData _playerSaveData = new PlayerSaveData();

    private void Awake()
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

        FindAndAssignPlayer();

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    

    private void Update()
    {
        if (player == null)
        {
            FindAndAssignPlayer();
        }
    }

    private void FindAndAssignPlayer()
    {
        if (player != null) return;

        Player found = FindFirstObjectByType<Player>();
        if (found != null)
        {
            player = found;
            Debug.Log($"Игрок найден и присвоен: {player.name}");
            OnPlayerAssigned?.Invoke();
        }
    }
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            DeleteSave();
            Debug.Log("Игра окончена, сохранение удалено.");
        }
    }
   

    private static string GetSaveFilePath()
    {
        string path = Application.persistentDataPath + "/save.json";
        Debug.Log("Путь сохранения: " + path);
        return path;
    }

    public void SaveGame()
    {
        if (player == null)
        {
            Debug.LogWarning("Нельзя сохранить — игрок не найден!");
            return;
        }

        player.Save(ref _playerSaveData);

        string json = JsonUtility.ToJson(_playerSaveData, true);
        File.WriteAllText(GetSaveFilePath(), json);

        Debug.Log("💾 Игра сохранена!");
    }

    public void LoadGame()
    {
        string path = GetSaveFilePath();
        if (!File.Exists(path))
        {
            Debug.LogWarning("❌ Файл сохранения не найден: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        _playerSaveData = JsonUtility.FromJson<PlayerSaveData>(json);

        if (player != null)
        {
            player.Load(_playerSaveData);
            Debug.Log("📂 Игра загружена!");
        }
        else
        {
            Debug.LogWarning("Игрок ещё не найден на сцене → данные загружены, но не применены. Применятся, когда игрок появится.");
        }
    }

    public bool SaveExists()
    {
        return File.Exists(GetSaveFilePath());
    }

    public void DeleteSave()
    {
        string path = GetSaveFilePath();
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("🗑️ Сохранение удалено!");
        }
    }
}