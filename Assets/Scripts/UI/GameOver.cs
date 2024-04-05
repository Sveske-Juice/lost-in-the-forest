using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public float timeToTemple = 5f;
    public GameObject content;

    public void GameOverStart()
    {
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        content.SetActive(true);
        CombatPlayer.combatPlayer.GetComponent<SpriteRenderer>().enabled = false;
        CombatPlayer.combatPlayer.GetComponent<TempMove>().enabled = false;
        Time.timeScale = 0f;

        yield return new WaitForSeconds(timeToTemple);

        content.SetActive(false);
        CombatPlayer.combatPlayer.GetComponent<SpriteRenderer>().enabled = true;
        CombatPlayer.combatPlayer.GetComponent<TempMove>().enabled = true;
        CombatPlayer.combatPlayer.GetComponent<HealthComponent>().SimpleKill();

        // reset game
        // WARNING: this mf code is so bad and smelly but i really dont give a FUCk
        foreach (var go in GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            if (go != null && go != this.gameObject)
                Destroy(go);
        }
        Destroy(this.gameObject);

        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // main menu
    }
}
