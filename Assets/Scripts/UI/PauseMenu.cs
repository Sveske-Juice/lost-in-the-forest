using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    public GameObject pausemenu;
    public GameObject shopMenu;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.transform.root.gameObject);
            return;
        }
        Instance = this;

        SceneManager.sceneLoaded += SceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        pausemenu.SetActive(false);
        shopMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausemenu.SetActive(!pausemenu.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            shopMenu.SetActive(!shopMenu.activeSelf);
        }

        float timeScale = pausemenu.activeSelf ? 0f : 1f;
        Time.timeScale = timeScale;
    }

    public void Enable() => pausemenu.SetActive(true);
    public void Disable() => pausemenu.SetActive(false);
}
