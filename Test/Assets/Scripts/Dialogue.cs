using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueNode[] node;
    public int currentNode = 0;
    public bool showDialogue;
    private byte currentQuestion;
    private Texts Texts;
    private Main Main;
    void Start()
    {
       Main = GameObject.Find("Main").GetComponent<Main>();
       Texts = GameObject.Find("Main").GetComponent<Texts>();
    }

    public void FillingMainDialogue() {
        
        showDialogue = true;

        node[0].npcText = "Беги, глупец!";
        node[1].npcText = Main.enemyPlayer.answers[0];
        node[2].npcText = Main.enemyPlayer.answers[1];

        node[0].playerAnswer[0].text = "Кто ты?";
        node[0].playerAnswer[0].toNode = 1;

        node[0].playerAnswer[1].text = "Что случилось?";
        node[0].playerAnswer[1].toNode = 2;

        node[0].playerAnswer[2].text = "Сам беги";
        node[0].playerAnswer[2].SpeakEnd = true;

        node[1].playerAnswer[0].text = "В смысле?";
        node[1].playerAnswer[0].toNode = 0;

        node[2].playerAnswer[0].text = "Что ты имеешь ввиду?";
        node[2].playerAnswer[0].toNode = 0;

    }

    public void FillingEndDialogue() 
    {
        currentQuestion = 0;
        Texts.DiaplayUsualText("Кто ты?");
    }

    public void OnEndEdit()
    {
        Main.enemyPlayer.answers[currentQuestion] = Texts.inputField.text;
        
        Texts.CloseUsualText();

        if (currentQuestion == 0)
        {
            Texts.DiaplayUsualText("Что случилось?");
        }
        else
        {
            Main.endGame = 2;

            Texts.DisplayMainText("Пока", "Перезапустить");            
        }
        currentQuestion++;
    }

    void OnGUI()
    {
        if (showDialogue) {

            Main.PauseGame();

            var _currentNode = node[currentNode];

            GUI.Box(new Rect (Screen.width / 2 - 250, Screen.height - 500, 500, 125), "Незнакомец");
            GUI.Label(new Rect (Screen.width / 2 - 250, Screen.height - 480, 500, 90), _currentNode.npcText);
            
            for (int i = 0; i < _currentNode.playerAnswer.Length; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2 -250, Screen.height - 450 + 25*i, 500, 25), _currentNode.playerAnswer[i].text)) 
                {
                    currentNode = _currentNode.playerAnswer[i].toNode;

                    if (_currentNode.playerAnswer[i].SpeakEnd) 
                    {
                        showDialogue = false;
                        Main.ResumeGame();
                        Main.ResumePlayer();
                    }

                    
                }
            }

        }

    }
}

[System.Serializable]
public class DialogueNode
{
    public string npcText;
    public Answer[] playerAnswer;
}

[System.Serializable]
public class Answer

{
    public string text;
    public int toNode;
    public bool SpeakEnd;
}

