using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDisplay : MonoBehaviour
{
    public float timeToTemple = 10f;
    public GameObject content;

    private void Awake()
    {
        BossKilledNotifier.BossKilled += BossKilled;
    }

    private void OnDestroy()
    {
        BossKilledNotifier.BossKilled -= BossKilled;
    }

    private void BossKilled()
    {
        UpdateUI(true);
    }

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
        UpdateUI(false);
        SceneManager.LoadScene(1);
    }
}
