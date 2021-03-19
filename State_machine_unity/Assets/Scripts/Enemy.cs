using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Status{ idel,walk  }
    public Status status;

    public enum Face { Right,Left }
    public Face face;

    public float speed;

    private Transform myTransform;

    private Transform playerTransform;

    private SpriteRenderer spr;

    void Start ()
    {
        status = Status.idel;

        spr = this.transform.GetComponent<SpriteRenderer>();

        if (spr.flipX)
        {
            face = Face.Left;
        }
        else
        {
            face = Face.Right;
        }

        myTransform = this.transform;

        if(GameObject.Find("Player") != null)
        {
           playerTransform = GameObject.Find("Player").transform;
        }
       
    }
	
	void Update ()
    {
        if (playerTransform) 
        {
            if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) > 2)/*當敵人根Player位置大於2時 變成idel*/
            {
                status = Status.idel;
                SetEnemyState(status);
            }
            else if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) < 2)/*當敵人根Player位置小於2時 變成walk*/
            {
                status = Status.walk;
                SetEnemyState(status);
              
            }

          
        }





    }

    public void SetEnemyState(Status EnemyState)
    {
        status = EnemyState;
        switch(status)
        {
            case Status.idel:
                Debug.Log("IDEL狀態");
                break;

            case Status.walk: /*敵人走路狀態*/
                Debug.Log("走路狀態");
                if (playerTransform)
                {
                    if (myTransform.position.x >= playerTransform.position.x) /*當敵人的位置在玩家右邊(敵人要看左邊)*/
                    {
                        spr.flipX = true;
                        face = Face.Left;
                        SetEnemyFace(face);
                    }
                    else                                                      /*當敵人的位置在玩家左邊(敵人要看右邊)*/
                    {
                        spr.flipX = false;
                        face = Face.Right;
                        SetEnemyFace(face);
                    }
                }
                break;
        }
    }

    public void SetEnemyFace(Face newFaceState) 
    {
        face = newFaceState;
        switch (face)
        {
            case Face.Right: /*敵人向右走路*/
                myTransform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                break;

            case Face.Left:  /*敵人向左走路*/
                myTransform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                break;
        }

    }

}
