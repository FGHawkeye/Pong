using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    public float speed = 3.5f;
    public Text textP1;
    public Text textP2;

    public Transform Respawn_p1;
    public Transform Respawn_p2;

    enum hitBall : byte
    {
        Stanby = 0,
        Player1 = 1,
        Player2 = 2
    }
    private hitBall hb = hitBall.Stanby;
    private int gravity = 0;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space) && hb == hitBall.Stanby)
        {
            hb = hitBall.Player1;
            gravity = 1;
        }
        else if (hb != hitBall.Stanby)
        {
            moveBall();
        }
    }

    /// <summary>
    /// Called when the ball collide an object with 2dCollider
    /// </summary>
    /// <param name="col"> The other object of collision </param>
    void OnCollisionEnter2D(Collision2D col)
    {
        /*
         * If the other collision object is the player 1 then the movement change to right
         * else left
         */
        if (col.gameObject.tag == "P1")
        {
            hb = hitBall.Player1;
        }
        else if (col.gameObject.tag == "P2")
        {
            hb = hitBall.Player2;
        }
        else if (col.gameObject.tag == "field_left-right")
        {
            //if the ball is going right then +1 to player 1, set default position of both players and  set the ball in the player 2 position.
            if (hb == hitBall.Player1)
            {
                textP1.text = (Convert.ToInt32(textP1.text) + 1).ToString();
                reinitialize();
                transform.position = Respawn_p2.position + new Vector3(-0.1f, 0, 0);
            }
            else if (hb == hitBall.Player2)
            {
                textP2.text = (Convert.ToInt32(textP2.text) + 1).ToString();
                reinitialize();
                transform.position = Respawn_p1.position + new Vector3(0.1f, 0, 0);
            }

        }
        else if (col.gameObject.tag == "field_up-down")
        {
            gravity = gravity * -1;
        }
    }

    void moveBall()
    {
        if (hb == hitBall.Player1)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (hb == hitBall.Player2)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if(gravity == -1)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else if(gravity == 1)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }

    void reinitialize()
    {
        GameObject.Find("P1").transform.position = Respawn_p1.position;
        GameObject.Find("P2").transform.position = Respawn_p2.position;
        hb = hitBall.Stanby;
        gravity = 0;
    }
}
