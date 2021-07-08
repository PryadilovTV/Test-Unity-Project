using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity; 
    private Main Main;
    private Texts Texts;
    private Joystick joystick;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 5f;

        Main = GameObject.Find("Main").GetComponent<Main>();
        Texts = GameObject.Find("Main").GetComponent<Texts>();

        joystick = GameObject.Find("FixedJoystick").GetComponent<Joystick>();
    }

    void Update()
    {
        if (Main.shockTriggered)
        {
            Vector2 moveInput = new Vector2(0, -1);
            moveVelocity = moveInput.normalized * speed * 2;
        }
        else
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal") + joystick.Horizontal, Input.GetAxisRaw("Vertical") + joystick.Vertical);
            moveVelocity = moveInput.normalized * speed;
        }
    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);  
    }

    public void Attack()
    {
        Texts.DisplayLittleText("Тык");
    }

    public void Button1()
    {
        Texts.DisplayLittleText("Бум");
    }
    public void Button2()
    {
        Texts.DisplayLittleText("Шур шур");
        
        if (!Main.player.coinLooted)
        {
            var graveStone = GameObject.Find("Gravestone");

            if (Vector2.Distance(graveStone.transform.position, transform.position) < 2)
            {
                Texts.DisplayMainText("Вы нашли монету", "Класс");
                Main.player.coinLooted = true;
            }
        }
    }
    public void Button3()
    {
        if (Main.player.coinLooted) Texts.DisplayLittleText("У тебя есть монетка");
        else Texts.DisplayLittleText("А в рюкзаке пусто");
    }

}
