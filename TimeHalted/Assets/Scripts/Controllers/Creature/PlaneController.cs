using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneController : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rigid;
    SpriteRenderer _sprite;

    private float flapForce = 6f;
    private float forwardSpeed = 3f;

    private bool isDead = false;
    public bool IsDead { get { return isDead; } }

    [SerializeField] private bool isFlap = false;

    public bool godMode = false;

    GameManager gameManager;

    public void Init()
    {
        gameManager = GameManager.Instance;

        _anim = GetComponentInChildren<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();

        Sprite sprite = gameManager.GetCustomSprite();
        SetSprite(sprite);
    }

    private void Update()
    {
 
    }

    private void FixedUpdate()
    {
        if (isDead) 
            return;
        if (_rigid == null)
            return;

        Vector3 velocity = _rigid.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigid.velocity = velocity;

        float angle = Mathf.Clamp((_rigid.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;
        if (isDead) return;

        _anim.SetBool("IsDie", true);
        isDead = true;

        gameManager.FlappyGameOver();
    }

    public void SetSprite(Sprite sprite)
    {
        _sprite.sprite = sprite;
        _sprite.material.color = gameManager.ColorManager.GetColor(gameManager.SelectedColor);
    }

    void OnFlap(InputValue inputValue)
    {
        isFlap = inputValue.isPressed;
    }
}
