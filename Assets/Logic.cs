using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Logic : MonoBehaviour
{
    public int score = 0;
    public Text textScore;
    public GameObject gameOverScreen;
    public GameObject mainMenu;

    public int highScore = 0;
    public Text highScoreText;

    public Camera mainCamera; // 🔥 for screen shake

    private Vector3 originalCamPos;

    void Start()
    {
        Time.timeScale = 0f;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();

        if (mainCamera != null)
            originalCamPos = mainCamera.transform.position;
    }

    public void playGame()
    {
        Debug.Log("PLAY BUTTON CLICKED");
        Time.timeScale = 1f;
        mainMenu.SetActive(false);
    }

    [ContextMenu("increase score")]
    public void addScore(int adder)
    {
        score += adder;
        textScore.text = score.ToString();

        // 🔥 JUICE: score bounce
        StopCoroutine("ScoreBounce");
        StartCoroutine(ScoreBounce());

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    public void gameRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;

        highScoreText.text = "High Score: " + highScore.ToString();

        // 🔥 JUICE: screen shake
        if (mainCamera != null)
        {
            StopCoroutine("ScreenShake");
            StartCoroutine(ScreenShake());
        }
    }

    // 🔥 SCORE BOUNCE
    IEnumerator ScoreBounce()
    {
        Vector3 originalScale = textScore.transform.localScale;
        Vector3 targetScale = originalScale * 1.15f; // smaller effect

        float duration = 0.12f;
        float t = 0f;

        // scale up smoothly
        while (t < duration)
        {
            textScore.transform.localScale = Vector3.Lerp(originalScale, targetScale, t / duration);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        t = 0f;

        // scale back smoothly
        while (t < duration)
        {
            textScore.transform.localScale = Vector3.Lerp(targetScale, originalScale, t / duration);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        textScore.transform.localScale = originalScale; // safety reset
    }

    // 🔥 SCREEN SHAKE
    IEnumerator ScreenShake()
    {
        float duration = 0.25f;   // shorter = sharper
        float magnitude = 0.15f;  // reduce if too strong

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.localPosition = originalCamPos + new Vector3(x, y, 0);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = originalCamPos;
    }
}