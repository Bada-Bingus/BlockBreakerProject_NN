using NUnit.Framework.Constraints;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball {  get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }
    public int level = 1;
    public int score = 0;
    public int lives = 3;
    public bool gameOver = false;
    public TMP_Text CurrentScore;
    public TMP_Text LivesUI;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }
    void Start()
    {
        NewGame();
    }
    void Update()
    {

    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;

        //LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        SceneManager.LoadScene("Level" + level);
    }
    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = UnityEngine.Object.FindFirstObjectByType<Ball>();
        this.paddle = UnityEngine.Object.FindFirstObjectByType<Paddle>();
        this.bricks = UnityEngine.Object.FindObjectsOfType<Brick>();
    }
    public void Hit(Brick brick)
    {
        this.score += brick.point;
        this.CurrentScore.text = "Score: " + this.score;

        if (Cleared())
        {
            LoadLevel(this.level + 1);
        }
    }
    public void Miss()
    {
        this.lives--;
        this.LivesUI.text = "Lives: " + this.lives;

        if (this.lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }
    private void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }
    private void GameOver()
    {
        gameOver = true;
        this.CurrentScore.gameObject.SetActive(false);
        this.LivesUI.gameObject.SetActive(false);
        SceneManager.LoadScene("GameOver");
    }
    private bool Cleared()
    {
        for(int i = 0; i < this.bricks.Length; i++)
        {
            if(this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
            {
                return false;
            }
        }

        return true;
    }
}
