using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject tittle;
    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject exit;

    // Start is called before the first frame update
    void Start()
    {
        //Start button scale
        Vector3 startScale = new Vector3(0f, start.transform.localScale.y, start.transform.localScale.z);
        start.transform.localScale = startScale;
        //Exit button scale
        Vector3 exitScale = new Vector3(0f, exit.transform.localScale.y, exit.transform.localScale.z);
        exit.transform.localScale = exitScale;
        //Tittle
        Image img = tittle.GetComponent<Image>();
        Color startColor = img.color;
        startColor.a = 0f;
        img.color = startColor;
        LeanTween.value(tittle, img.color.a, 1, 2.5f)
            .setOnUpdate((float newAlpha) =>
            {
                //Tittle alpha
                Color color = img.color;
                color.a = newAlpha;
                img.color = color;
            })
            .setOnComplete(() =>
            {
                //Tittle position
                Vector3 targetPosition = tittle.transform.localPosition + new Vector3(0, 100, 0);
                LeanTween.moveLocal(tittle, targetPosition, 0.25f)
                .setOnComplete(() =>
                {
                    LeanTween.delayedCall(0.4f, () =>
                    {
                        //Start button scale
                        LeanTween.scaleX(start, 0.9f, 0.1f)
                            .setOnComplete(() =>
                            {
                                LeanTween.delayedCall(0.4f, () =>
                                {
                                    //Exit button scale
                                    LeanTween.scaleX(exit, 0.9f, 0.1f);
                                });
                            });
                    });
                });
            });
    }

    public void StartButton()
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
