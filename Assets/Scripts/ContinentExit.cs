using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinentExit : MonoBehaviour
{
    [SerializeField] GameObject GameModePanel;

    public void BackButtonForcontinentlist()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void BackButtonForModeSelect() 
    {
       StartCoroutine(ScaleDown(GameModePanel.transform));
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
