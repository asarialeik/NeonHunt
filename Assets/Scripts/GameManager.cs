using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField]
    private TMP_Text _currentTimeText;
    [SerializeField]
    private TMP_Text _currentScoreText;
    [SerializeField]
    private TMP_Text _currentEnemiesText;
    private float _limitTime = 7 * 60;
    public float currentTime;
    private int _enemiesKilled;
    public int currentScore = 0;
    public List<GameObject> enemies = new List<GameObject>();
    public float finalTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentTime = _limitTime;
    }
    void Update()
    {
        if (_currentTimeText != null)
        {
            UpdateUI();
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else if (currentTime <= 0)
            {
                currentTime = 0;
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene("GameOver");
                SceneManager.UnloadSceneAsync(currentScene.name);
            }

            if (_enemiesKilled == enemies.Count)
            {
                finalTime = currentTime;
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene("YouWon");
                SceneManager.UnloadSceneAsync(currentScene.name);
            }
        }
    }

    void UpdateUI()
    {
        if (_currentTimeText != null)
        {
            _currentTimeText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(currentTime / 60), Mathf.FloorToInt(currentTime % 60));
            _currentScoreText.text = currentScore.ToString();
            _currentEnemiesText.text = (_enemiesKilled + "/" + enemies.Count.ToString());
        }
    }
    public void Register(GameObject enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }

    public void Unregister()
    {
        _enemiesKilled+=1;
    }
}
