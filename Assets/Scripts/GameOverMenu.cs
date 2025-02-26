using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject restart;
    [SerializeField]
    private GameObject exit;

    private void Start()
    {
        if (scoreText != null)
        {
            int timerScore = Mathf.FloorToInt(GameManager.Instance.finalTime) * 10;
            GameManager.Instance.currentScore = timerScore + GameManager.Instance.currentScore;
            scoreText.text = GameManager.Instance.currentScore.ToString();
        }
        gameOverMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        // Game Over scale
        Vector3 gameOverScale = new Vector3(0f, gameOver.transform.localScale.y, gameOver.transform.localScale.z);
        gameOver.transform.localScale = gameOverScale;
        // Restart button scale
        Vector3 startScale = new Vector3(0f, restart.transform.localScale.y, restart.transform.localScale.z);
        restart.transform.localScale = startScale;
        // Exit button scale
        Vector3 exitScale = new Vector3(0f, exit.transform.localScale.y, exit.transform.localScale.z);
        exit.transform.localScale = exitScale;

        LeanTween.delayedCall(0.2f, () =>
        {
            LeanTween.scaleX(gameOver, 0.75f, 0.1f)
            .setOnComplete(() =>
            {
                LeanTween.delayedCall(0.6f, () =>
                {
                    //Start button scale
                    LeanTween.scaleX(restart, 0.94f, 0.1f)
                        .setOnComplete(() =>
                        {
                            LeanTween.delayedCall(0.4f, () =>
                            {
                                //Exit button scale
                                LeanTween.scaleX(exit, 0.6f, 0.1f);
                            });
                        });
                });
            });
        });
    }
    public void RestartButton()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(1);
        SceneManager.UnloadSceneAsync(currentScene.name);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
