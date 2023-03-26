using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameEndType {WIN,LOSE}
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject gameEndMenuScreen;
    [SerializeField] private TMP_Text gameEndText;
    [SerializeField] private TMP_Text enemyLeftCount;

    [SerializeField] private TMP_Text bombTimerText;
    public static MenuManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();
    }
    public void EnableMainMenu()
    {
        Time.timeScale = 0f;
        mainMenuScreen.SetActive(true);
        gameEndMenuScreen.SetActive(false);
    }
    public void EnableGameEndMenuMenu(GameEndType gameEndType)
    {
        Time.timeScale = 0f;
        mainMenuScreen.SetActive(false);
        gameEndMenuScreen.SetActive(true);
        if(gameEndType == GameEndType.WIN)
        {
            gameEndText.text = "Congratulations\n You Won!";
        }
        else
        {
            gameEndText.text = "GameOver!";
        }
    }
    public void StartGame()
    {
        mainMenuScreen.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateGameEnemyLeftCount(int count)
    {
        enemyLeftCount.text = "Enemies Left : " + count.ToString();
    }
    public void StartBombTimer()
    {
        StopAllCoroutines();
        StartCoroutine(BombTimer());
    }
    IEnumerator BombTimer()
    {
        int bombTimer = 3;
        while(bombTimer >=0)
        {
            bombTimerText.text = "BOMB TIMER : " + bombTimer;
            yield return new WaitForSeconds(1f);
            bombTimer--;
        }
    }
}
