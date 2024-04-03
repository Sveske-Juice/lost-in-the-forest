using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDisplay : MonoBehaviour
{
    public float timeToTemple = 10f;
    public GameObject content;

    public void UpdateUI(bool enabled)
    {
        content.SetActive(enabled);

        if (enabled)
        {
            StartCoroutine(LoadTemple());
        }
    }

    IEnumerator LoadTemple()
    {
        yield return new WaitForSeconds(timeToTemple);
            SceneManager.LoadScene(1);
    }
}
