using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    public bool isVertical;
    public float speed;
    private float _direction = 1;
    private float _directionTime = 2;
    private float _timer;
    private bool isBroken = true;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3.0f;
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _timer = _directionTime;
        // _animator.SetFloat("direction", _direction);
        // _animator.SetBool("vertical", isVertical);
        UpdateAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBroken)
        {
            return;
        }

        ChangeDirection();
        Move();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController ruby = other.gameObject.GetComponent<RubyController>();
        if (ruby != null)
        {
            ruby.ChangeHealth(-1);
        }
    }

    /**
     * 敌人自动换向
     */
    private void ChangeDirection()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0) return;
        _timer = _directionTime;
        _direction = -_direction;
        // _animator.SetFloat("direction", _direction);
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        // 垂直轴向动画
        if (isVertical)
        {
            _animator.SetFloat("MoveX", 0);
            _animator.SetFloat("MoveY", _direction);
        }
        // 水平轴向动画
        else
        {
            _animator.SetFloat("MoveX", _direction);
            _animator.SetFloat("MoveY", 0);
        }
    }

    /**
     * 移动
     */
    private void Move()
    {
        // 敌人移动
        var position = _rigidbody2D.position;
        if (isVertical)
        {
            position.y += Time.deltaTime * speed * _direction;
        }
        else
        {
            position.x += Time.deltaTime * speed * _direction;
        }

        _rigidbody2D.position = position;
    }

    /**
     * 修复
     */
    public void Fix()
    {
        isBroken = false;
        _rigidbody2D.simulated = false;
    }
}