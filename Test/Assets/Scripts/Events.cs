using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    private Texts Texts;
    private Main Main;
    private Dialogue Dialogue;
    private GameObject playerObject;
    public GameObject enemyPlayerPrefab;
    public GameObject batPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Texts = GameObject.Find("Main").GetComponent<Texts>();
        Main = GameObject.Find("Main").GetComponent<Main>();
        Dialogue = GameObject.Find("Main").GetComponent<Dialogue>();

        playerObject = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        //Debug.Log(other.name);
        switch (other.name) 
        {
            case "Exit": //Попытка выйти наружу

                if (Main.shockTriggered) //Если выйти можно, приходит чувак, диалог. Выход
                {
                    Main.enemyPlayer.spawnPoint = new Vector2(0, -9);
                    Main.enemyPlayer.targetPosition = new Vector2(0, 0);
                    Main.enemyPlayer.color = new Color32(255,255,255,255);

                    GameObject _enemyPlayer = GameObject.Instantiate(enemyPlayerPrefab, Main.enemyPlayer.spawnPoint, Quaternion.identity);    
                    _enemyPlayer.GetComponent<SpriteRenderer>().color = Main.enemyPlayer.color;
                    _enemyPlayer.GetComponent<MovingEnemy>().targetPosition = Main.enemyPlayer.targetPosition; 
                    _enemyPlayer.name = "EnemyPlayerExit";
                    
                }
                else if (Main.player.coinLooted) //Монетку нашли, можно и выйти
                {
                    Main.endGame = 1;
                    Texts.DisplayMainText("Ты нашел хоть что-то, при этом сохранив свой рассудок в целостности. Поздравляю, ты выиграл!", "Вау");
                }
                else //Если выйти нельзя, вывести сообщение
                {
                    Texts.DisplayMainText("ExitGame", true);
                }    
                break;
            
            case "SpawnEnemy": //Приходит чувак
                
                if (!Main.enemyPlayer.triggered)
                {
                    Main.PausePlayer();
                    
                    GameObject _enemyPlayer = GameObject.Instantiate(enemyPlayerPrefab, Main.enemyPlayer.spawnPoint, Quaternion.identity);
                    
                    _enemyPlayer.GetComponent<SpriteRenderer>().color = Main.enemyPlayer.color;

                    _enemyPlayer.GetComponent<MovingEnemy>().targetPosition = Main.enemyPlayer.targetPosition; 

                    _enemyPlayer.name = "EnemyPlayer";

                    Main.enemyPlayer.triggered = true;

                }
                break;

            case "EnemyPlayer": //Запускаем систему диалога

                Dialogue.FillingMainDialogue();

            break;

            case "EnemyPlayerExit": //Запускаем систему диалога

                Dialogue.FillingEndDialogue();

            break;

            case "SpawnBat": 
                
                if (!Main.bat.triggered)
                {
                    Main.PausePlayer();

                    GameObject _bat = GameObject.Instantiate(batPrefab, Main.bat.spawnPoint, Quaternion.identity);

                    _bat.GetComponent<MovingEnemy>().targetPosition = Main.bat.targetPosition; 

                    _bat.name = "Bat";

                    Main.bat.triggered = true;
                }
            break;

            case "Bat":
                
                Texts.DisplayMainText("Bat", true);
               
                Main.player.health -= 30;

                playerObject.GetComponent<SpriteRenderer>().color = new Color32(255,105,105,255);

            break;

            case "Gravestone":
               
                if (!Main.graveStoneSearched) Texts.DisplayMainText("Здесь можно пошуршать");
            break;

            case "Shock":
                
                if (!Main.shockTriggered)
                {
                    Texts.DisplayMainText("Shock", true);
                    Main.shockTriggered = true;
                }
            break;
                    
        }
        
    }
}
