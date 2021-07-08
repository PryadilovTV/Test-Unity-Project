using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texts : MonoBehaviour
{
    private Text mainText;
    private Text usualText;
    private Text littleText;
    private Text continueText;
    public InputField inputField;

    private GameObject littleTextObject;
    private GameObject usualTextObject;
    private GameObject inputFieldObject;

    public Main Main;

    void Awake()
    {
        mainText = GameObject.Find("MainText").GetComponent<Text>();
        littleText = GameObject.Find("LittleText").GetComponent<Text>();
        usualText = GameObject.Find("UsualText").GetComponent<Text>();
        continueText = GameObject.Find("ContinueText").GetComponent<Text>();
        inputField = GameObject.Find("InputField").GetComponent<InputField>();

        Main = GameObject.Find("Main").GetComponent<Main>();

        littleTextObject = GameObject.Find("LittleText");
        usualTextObject = GameObject.Find("UsualText");
        usualTextObject.SetActive(false);

        inputFieldObject = GameObject.Find("InputField");
        inputFieldObject.SetActive(false);
    }

    void Update()
    {
        if (littleText.color.a > 0)
        {
            littleTextObject.SetActive(true);
            littleText.color = new Color(1, 1, 1, littleText.color.a - (1f / 255f));        
        }
        else littleTextObject.SetActive(false);
    }

    public void DisplayMainText(string _text, string _continueText="Продолжить")
    {
        Main.PauseGame();

        Main.mainPanel.SetActive(true);
        
        mainText.text = _text;
        continueText.text = _continueText;
    }

    public void DisplayMainText(string typeText, bool unusual)
    {
        string _mainText = "";
        string _continueText = "";

        switch (typeText)
        {
            case "StartGame":
                _mainText = GetStartText();
                _continueText = "Продолжить";
                break;

            case "ExitGame":
                _mainText = GetExitText();
                _continueText = "Продолжить";
                break;

            case "Bat":
                _mainText = GetBatText();
                _continueText = "Печалька";
                break;
            case "Shock":
                _mainText = GetShockText();
                _continueText = "На выход";
                break;
        }

        DisplayMainText(_mainText, _continueText);
    }
    public void DisplayLittleText(string _text)
    {
       littleText.color = new Color32(255, 255, 255, 255);
       littleText.text = _text;
    }

    public void DiaplayUsualText(string _text)
    {
        Main.PauseGame();

        littleTextObject.SetActive(false);

        usualTextObject.SetActive(true);
        usualText.text = _text;

        inputFieldObject.SetActive(true);
    }

    public void CloseUsualText()
    {
        inputField.text = "";

        usualTextObject.SetActive(false);
        inputFieldObject.SetActive(false);

        Main.ResumeGame();
    }
    
    public void ContinuePressed()
    {

        if (Main.endGame == 1) //Выходим из игры
        {
            Debug.Log("Выход из игры");
            Application.Quit();
        }
        else if (Main.endGame == 2) //Перезапускаем игру
        {

            Main.mainPanel.SetActive(false);

            Main.ResumeGame();
            Main.ResumePlayer();
            Main.RestartGame();
        }
        else //Продолжаем игру
        {
            Main.mainPanel.SetActive(false);
            Main.ResumeGame();
            Main.ResumePlayer();
        }
    }

    string GetStartText()
    {
        string _text = "";

        if (Main.tryNumber == 1) _text = "Ты - отважный герой, пришедший в эту пещеру, чтобы убить легендарное чудовище, добыть славы и немножко полезных ингредиентов";
        else _text = "Ты - отважный герой и все такое. Где-то ты уже слышал подобное, но где?";
        return _text;
    }
    string GetExitText()
    {
        string text = "";

        text = "Ты еще не готов покинуть эту пещеру. ";
        if (Main.enemyPlayer.triggered) text += "Хотя уже начинаешь задумываться об этом. ";
        if (Main.bat.triggered) text += "Все сильнее и сильнее";
            
        return text;
    }
    string GetBatText()
    {
        string _text = "";

        _text = "Внезапно из пещеры вылетела стая летучих мышей (да, оранжевых!). Они тебя покусали, дыхнули огнем и улетели вдаль";
                
        return _text; 
    }
    string GetShockText()
    {
        string _text = "";

        _text = "Открывшаяся перед тобой картина отпечаталась в памяти навсегда. Теперь ты понял, о чем говорил незнакомец. К сожалению...";    
        return _text;
    }

}
