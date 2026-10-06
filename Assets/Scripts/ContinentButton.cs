using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class ContinentButton : MonoBehaviour
{
    public string Name;
    public GameObject GameModePanel; // Reference to the Game Mode Panel

    // Start is called before the first frame update
    public void OnClick()
    {
        GameSettings.Instance.SetContinentName(Name);
        EnableGameModePanel(); // Show the Game Mode Panel when continent is selected
    }

    // This function enables the Game Mode Panel
    public void EnableGameModePanel()
    {
        if (GameModePanel != null)
        {
            GameModePanel.SetActive(true); // Enable Game Mode Panel
            StartCoroutine(ScaleUp(GameModePanel.transform));
        }
    }
   
    IEnumerator ScaleUp(Transform panelTransform)
    {
        Vector3 initialScale = new Vector3(0, 0, 0);
        Vector3 finalScale = new Vector3(1, 1, 1);
        float duration = 0.3f;
        float elapsedTime = 0;

        panelTransform.localScale = initialScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            panelTransform.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / duration);
            yield return null;
        }

        panelTransform.localScale = finalScale;
    }
    IEnumerator ScaleDown(Transform panelTransform)
    {
        Vector3 initialScale = panelTransform.localScale;
        Vector3 finalScale = new Vector3(0, 0, 0);
        float duration = 0.3f;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            panelTransform.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / duration);
            yield return null;
        }

        GameModePanel.SetActive(false);  // Disable the panel after shrinking
    }
}
