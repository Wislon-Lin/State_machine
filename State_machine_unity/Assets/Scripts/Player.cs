using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerState currentState; /*運用這個currentState*/
    public enum PlayerState /*列舉 列出*/
    {
        Idel,
        WalkLeft,
        WalkRight
    }

    private SpriteRenderer PlayerSprite; /*抓取PlayerSprite圖來控制Flip(翻轉)*/
    private Rigidbody2D rb;

    private void Awake()
    {
        currentState = PlayerState.Idel;
        PlayerSprite = this.transform.GetComponent<SpriteRenderer>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update() /*吃效能的地方*/ 
    {
       switch (currentState)     /*一直持續判斷currentState的狀態去做切換*/
        {
            case PlayerState.Idel:
                SetState(currentState);     
                break;

            case PlayerState.WalkLeft:
                SetState(currentState);
                break;

            case PlayerState.WalkRight:
                SetState(currentState);
                break;
        }
        /*-------------------------------------------------------*/

        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentState = PlayerState.WalkRight;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            currentState = PlayerState.Idel;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentState = PlayerState.WalkLeft;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            currentState = PlayerState.Idel;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)&&isJump==false)
        {
            rb.AddForce(transform.up *400);
            isJump = true;
        }

    }

    public void SetState(PlayerState newState)  
    {
        currentState = newState;
        switch (currentState)
        {
            case PlayerState.Idel:
                Debug.Log("Idel");
                break;
            case PlayerState.WalkLeft:
                Debug.Log("WalkingLeft");
                this.transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);
                PlayerSprite.flipX = true;
                break;
            case PlayerState.WalkRight:
                this.transform.position += new Vector3(2 * Time.deltaTime, 0, 0);
                Debug.Log("WalkingRight");
                PlayerSprite.flipX = false;
                break;
        }
    }



    private bool isJump =false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Floor")
        {
            isJump = false;
        }
    }



}
