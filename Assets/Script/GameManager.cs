using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string RESULT_UPDATE_SCORE = "刷新紀錄 !!!";
    private const string RESULT_GAME_OVER = "遊戲結束";
    private const string PLAYER_PREFS_HIGH_SCORE = "HIGH_SCORE";

    public GameObject obstaclePrefab;
    public GameObject startMenuPanel;
    public GameObject gameStartedPanel;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text gameResultText;
    public Text finalScoreText;
    public Text highScoreText;
    public Button startButton;
    public Button restartButton;
    public PlayerController playerController;

    private bool isGameOver;
    private float spawnXBase = 24f;// 可用區間為 24 ~ 60
    private float spawnY = 0.1f;
    private float spawnZ = 0.12f;
    private float spawnDelay = 1f;
    private float spawnInterval = 1.8f;
    private float scoreDelay = 0.5f;
    private float scoreInterval = 0.5f;
    private float showGameOverPanelDelay = 2.5f;
    private int score;
    private Scene scene;
    private AudioSource audioSource;

    void Start()
    {
        isGameOver = false;
        startButton.onClick.AddListener(StartButtonClick);
        restartButton.onClick.AddListener(RestartButtonClick);
        scene = SceneManager.GetActiveScene();
        playerController.enabled = false;
        score = 0;
        gameStartedPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void StartSpawn()
    {
        float spawnX = spawnXBase + Random.Range(0f, 36f);
        Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    }

    void CountScore()
    {
        score += 10;
        scoreText.text = score.ToString();
    }

    void StartButtonClick()
    {
        InvokeRepeating("StartSpawn", spawnDelay, spawnInterval);
        InvokeRepeating("CountScore", scoreDelay, scoreInterval);
        startMenuPanel.SetActive(false);
        playerController.enabled = true;
        gameStartedPanel.SetActive(true);
    }

    void RestartButtonClick()
    {
        SceneManager.LoadScene(scene.name);
    }

    void ShowGameOverPanel()
    {
        int tmpHighScore = PlayerPrefs.GetInt(PLAYER_PREFS_HIGH_SCORE, 0);

        gameStartedPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        finalScoreText.text = score.ToString();

        if (score > tmpHighScore)
        {
            gameResultText.text = RESULT_UPDATE_SCORE;
            PlayerPrefs.SetInt(PLAYER_PREFS_HIGH_SCORE, score);
            highScoreText.text = score.ToString();
        }
        else
        {
            gameResultText.text = RESULT_GAME_OVER;
            highScoreText.text = tmpHighScore.ToString();
        }

        Debug.Log(score.ToString());
    }

    public void GameOver()
    {
        CancelInvoke();
        audioSource.Stop();
        isGameOver = true;
        Invoke("ShowGameOverPanel", showGameOverPanelDelay);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
