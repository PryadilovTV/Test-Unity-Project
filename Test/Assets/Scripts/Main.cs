using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public int tryNumber;
    public bool shockTriggered;
    public bool graveStoneSearched;
    public int endGame;
    
    public Bat bat;
    public EnemyPlayer enemyPlayer;
    public Player player;

    public GameObject mainPanel;
    private GameObject playerObject;
    private Texts Texts; //класс Texts, используются его методы для вывода текста
    private PlayerController PlayerController;

    void Awake()
    {
        InitializationVariables();
        FillingVariables();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Texts.DisplayMainText("StartGame", true);
    }
    void InitializationVariables()
    {
        playerObject = GameObject.Find("Player");
        mainPanel = GameObject.Find("MainPanel");

        Texts = GameObject.Find("Main").GetComponent<Texts>();
        PlayerController = playerObject.GetComponent<PlayerController>();

        player = new Player();

        enemyPlayer = new EnemyPlayer();
        enemyPlayer.color = new Color32(255,105,105,255);

        enemyPlayer.answers = new string[2];
        enemyPlayer.answers[0] = "Я - это ты, ты - это я. И никого не надо нам. Мва-ха-ха-ха!!!";
        enemyPlayer.answers[1] = "Все бессмысленно, ты ничего не сделаешь!";

        bat = new Bat();

        tryNumber = 1;
    }

    public void FillingVariables()
    {
        player.health = 100;
        player.endurance = 100;

        enemyPlayer.spawnPoint = new Vector2(0, 5);
        enemyPlayer.targetPosition = new Vector2(0, -1);

        bat.spawnPoint = new Vector2(-10, -1.5f);
        bat.targetPosition = new Vector2(1, 0);
        bat.destination = 1;
    }
    
    public void RestartGame()
    {
        FillingVariables();
        
        player.coinLooted = false;
        shockTriggered = false;

        enemyPlayer.color = playerObject.GetComponent<SpriteRenderer>().color;
        enemyPlayer.triggered = false;

        playerObject.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
        playerObject.transform.SetPositionAndRotation(new Vector2(0, -7), Quaternion.identity);

        bat.triggered = false;

        var enemyPlayerObject = GameObject.Find("EnemyPlayerExit");
        Destroy(enemyPlayerObject, 0);

        endGame = 0;

        tryNumber++;

        Texts.DisplayMainText("StartGame", true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;   
    }
    public void PauseGame()
    {
        Time.timeScale = 0;   
    }
    public void PausePlayer()
    {
        PlayerController.speed = 0f;
    }
    public void ResumePlayer()
    {
        PlayerController.speed = 5f;
    }
}

public class Player
{
    public int health;
    public float endurance;
    public bool coinLooted;
}

public class Enemy
{
    public Vector2 spawnPoint;
    public Vector2 targetPosition;
    public bool triggered;
}

public class Bat: Enemy
{
    public int destination;
}

public class EnemyPlayer: Enemy
{
    public Color32 color;
    public string[] answers;
}

   
