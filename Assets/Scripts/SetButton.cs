using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetButton : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject firstButton;

    private void OnEnable()
    {
        StartCoroutine(SetFirstButtonDelayed());
    }

    private IEnumerator SetFirstButtonDelayed()
    {
        yield return null;

        if (firstButton != null && eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(firstButton);
            firstButton.GetComponent<Button>().Select();
        }
    }
}
