using System;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D _playerRB;
    private Animator _playerAnim;
    float xDir;
    [SerializeField] float jumpForce;
    private float jumpCount;
    private bool isJumping;
    [SerializeField] float xSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _playerRB =  GetComponent<Rigidbody2D>();
        _playerAnim = GetComponentInChildren<Animator>();
        
    }
    
    void FixedUpdate()
    {
        Movimentar(); 
        if (isJumping && jumpCount < 2)
        {
            Pular();
        }
    }
    
    void OnMove(InputValue inputValue)
    {
        xDir= inputValue.Get<Vector2>().x;
    }

    void OnJump(InputValue inputValue)
    {
        isJumping = true;
    }
    void Movimentar()
    {
        _playerRB.linearVelocityX = xDir * xSpeed;
        bool isRunning = Mathf.Abs(_playerRB.linearVelocityX) > Mathf.Epsilon;
        _playerAnim.SetBool("isRunning", isRunning);
        if (isRunning)
            flipSprite();
        
    }


    void Pular()
    {
        jumpCount += 1;
        _playerRB.linearVelocity = new Vector2(_playerRB.linearVelocity.x, jumpForce);
        isJumping = false;
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Foreground"))
        {
            isJumping = false;
            jumpCount = 0;
        }
    }

    void flipSprite()
    {
        float sinal = Mathf.Sign(_playerRB.linearVelocityX);
        transform.localScale = new Vector3(sinal, 1, 1);
    }
}
