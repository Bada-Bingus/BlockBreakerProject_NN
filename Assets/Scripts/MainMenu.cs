using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameManager gameManager;
    public TMP_Text FinalScore;
    private void Awake()
    {
        this.gameManager = Object.FindFirstObjectByType<GameManager>();
        this.gameManager.CurrentScore.gameObject.SetActive(false);
        this.gameManager.LivesUI.gameObject.SetActive(false);

        if (this.gameManager != null && this.gameManager.gameOver == true)
        {

            this.FinalScore.text = "Finale Score\n" + this.gameManager.score;

        }

    }

    public void PlayGame()
    {
        if (this.gameManager != null && this.gameManager.gameOver == true)
        {
            this.gameManager.level = Random.Range(1, 3);

            SceneManager.LoadScene("Level" + this.gameManager.level);

            this.gameManager.gameOver = false;
            this.gameManager.score = 0;
            this.gameManager.lives = 3;
            this.gameManager.CurrentScore.text = "Score: " + this.gameManager.score;
            this.gameManager.LivesUI.text = "Lives: " + this.gameManager.lives;
            this.gameManager.CurrentScore.gameObject.SetActive(true);
            this.gameManager.LivesUI.gameObject.SetActive(true);
        }

        else
        {
            this.gameManager.level = Random.Range(1, 3);

            SceneManager.LoadScene("Level" + this.gameManager.level);

            this.gameManager.CurrentScore.gameObject.SetActive(true);
            this.gameManager.LivesUI.gameObject.SetActive(true);

        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
