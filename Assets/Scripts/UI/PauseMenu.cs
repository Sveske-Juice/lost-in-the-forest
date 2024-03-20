using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausemenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausemenu.SetActive(!pausemenu.activeSelf);
        }

        float timeScale = pausemenu.activeSelf ? 0f : 1f;
        Time.timeScale = timeScale;
    }

    public void Enable() => pausemenu.SetActive(true);
    public void Disable() => pausemenu.SetActive(false);
}
