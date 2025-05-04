using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    protected Rigidbody2D _rigid;
    protected Animator _anim;

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 moveDirection = Vector2.zero;
    public Vector2 MoveDirection { get { return moveDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    Define.CreatureState creatureState = Define.CreatureState.Idle;
    public virtual Define.CreatureState CreatureState
    {
        get { return creatureState; }
        set
        {
            creatureState = value;
            UpdateAnimation();
        }
    }

    protected virtual void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Move(moveDirection);
    }

    protected virtual void HandleAction()
    {

    }

    public virtual void UpdateAnimation() 
    {
        switch (CreatureState)
        {
            case Define.CreatureState.Idle:
                _anim.Play("Idle");
                break;
            case Define.CreatureState.Move:
                _anim.Play("Move");
                break;
            case Define.CreatureState.Damage:
                _anim.Play("Damage");
                break;
        }
    }

    private void Move(Vector2 direction)
    {
        if (direction.magnitude > 0.5f)
            CreatureState = Define.CreatureState.Move;
        else
            CreatureState = Define.CreatureState.Idle;

        direction = direction * 5;
        _rigid.velocity = direction;
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        _sprite.flipX = isLeft;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
}
