using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] float steerSpped = 0.5f;
    //[SerializeField] float moveSpeed = 0.5f;
    public Camera MainCamera;
    Vector2 move;
    public float speedX, speedY;
    private Animator Player;
    void Start()
    {
        Player = GetComponent<Animator>();
        Player.SetBool("isRunning", false);
        Player.SetBool("isIdle", true);
        Player.SetBool("isJumping", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 1; i < 10; i++)
        {
            string levelTag = "Level" + i;
            if(collision.tag == levelTag)
            {
                moveCameraToLevel(levelTag);
                Debug.Log("Moving to Level: " + i);
            }   
        }
    }
    private void moveCameraToLevel(string levelTag)
    {
            string levelString = levelTag;
            string levelNumberString = levelString.Substring(5);

            GameObject level = GameObject.FindWithTag("Map" + levelNumberString);
            if (level != null)
            {
                Vector3 newCameraPosition = level.transform.position;
                MainCamera.transform.position = newCameraPosition + new Vector3(0, 0, -20);
            }
    }
    void Update()
    {
        //move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //transform.Translate(move * speed * Time.deltaTime);
        ////if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        ////{
        ////    transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        ////}
        ////if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        ////{
        ////    transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        ////}
        ////if (Input.GetKeyDown(KeyCode.Space))
        ////{
        ////    transform.Translate(0, 2f, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Player.SetBool("isRunning", true);
            Player.SetBool("isIdle",false);
            Player.SetBool("isJumping", false);
            gameObject.transform.Translate(Vector2.left * speedX * Time.deltaTime);
            if(gameObject.transform.localScale.x > 0)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Player.SetBool("isRunning", true);
            Player.SetBool("isIdle", false);
            Player.SetBool("isJumping", false);
            gameObject.transform.Translate(Vector2.right * speedX * Time.deltaTime);
            if (gameObject.transform.localScale.x < 0)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
            }
        }
       else if (Input.GetKey(KeyCode.Space))
        {
            Player.SetBool("isRunning", false);
            Player.SetBool("isIdle", false);
            Player.SetBool("isJumping", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, speedY);
        }
        else
        {
            Player.SetBool("isRunning", false);
            Player.SetBool("isIdle", true);
            Player.SetBool("isJumping", false);
        }

    }
}
