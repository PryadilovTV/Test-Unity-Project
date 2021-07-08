using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConditions : MonoBehaviour
{
    private Main Main;
    private Texts Texts;

    private Slider healthPanel;
    private Slider endurancePanel;
    private Joystick joystick;

    void Start()
    {
        Main = GameObject.Find("Main").GetComponent<Main>();
        Texts = GameObject.Find("Main").GetComponent<Texts>();

        healthPanel = GameObject.Find("HealthPanel").GetComponent<Slider>();
        endurancePanel = GameObject.Find("EndurancePanel").GetComponent<Slider>();

        joystick = GameObject.Find("FixedJoystick").GetComponent<Joystick>();
    }

    void FixedUpdate()
    {
        float enduranceLower = System.Math.Abs(Input.GetAxisRaw("Horizontal")) 
                             + System.Math.Abs(Input.GetAxisRaw("Vertical")) 
                             + System.Math.Abs(joystick.Horizontal) 
                             + System.Math.Abs(joystick.Vertical);
        
        Main.player.endurance -= enduranceLower * Time.deltaTime;

        endurancePanel.value = Main.player.endurance;
        healthPanel.value = Main.player.health;

        if (Main.player.endurance <= 0)
        {
            Texts.DisplayMainText("У тебя кончились силы и ты внезапно умер. Конец игры", "Совсем печалька");
            Main.endGame = 1;
        }
 
    }
}
