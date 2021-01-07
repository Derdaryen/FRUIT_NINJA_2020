using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour

{

    public enum GameState
    {
        loanding,
        inGame,
        gameOver
    }

    public GameState gameState;

    public List<GameObject> targetPrefabs;
    private float spawnRate = 1.9f;

    public TextMeshProUGUI scoreText;

    //SobreEscribir el score para asignarle un valor

    private int _score;
    private int score

    {
        set
        {
            _score = Mathf.Clamp(value, 0,999); //mantiene valor entre 0 y 999
        }
        get
        {
            return _score;
        }
    }
    private string nameScore = "Puntuacion:\n";

    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    public GameObject titleButton;


    private void Start()
    {
        ShowMaxScore();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    

    /// <summary>
    /// Nos devuelve un estado que es publico 
    /// </summary>

    public  void StartGame(int difficulty)
    {
        titleButton.gameObject.SetActive(false);
        gameState = GameState.inGame;

        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        score = 0;
         UpDateScore(0);
    }




    /// <summary>
    /// Cooruotinas nos ayuda sperar un periodo de tiemo y despues hacer un Spawning
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnTarget()
    {
        while (gameState == GameState.inGame ) 
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
            //Aumentara el Score
            
        }
    }

    /// <summary>
    /// Cambia el valor del Puntaje y lo aumenta
    /// </summary>
    /// <param name="scoreDate">Puntaje de score </param>

    public void  UpDateScore(int scoreDate)
    {
        score += scoreDate;
        scoreText.text = nameScore + score;
    }

    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt(MAX_SCORE, score);
        }
    }

    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        scoreText.text = "Max Score: \n " + maxScore;
    }


    private const string MAX_SCORE = "MAX_Score";

    /// <summary>
    /// Activa el texto de Game over cuando acaba la partida
    /// </summary>
    public void GameOverText()
    {

        SetMaxScore();

        gameOverText.gameObject.SetActive(true);
        gameState = GameState.gameOver;
        restartButton.gameObject.SetActive(true);
        
    }


    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
