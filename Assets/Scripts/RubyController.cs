using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    // 基本属性
    public float speed = 10;
    
    // 生命值
    public int maxHealth = 5;
    public int currentHealth;
    // 属性的使用
    public int Health
    {
        set => currentHealth = value;
        get => currentHealth;
    }
    
    // 无敌时间
    private float invincibleTime = 0.5f;
    private float currentInvincibleTime;
    private bool isInvincible;
    
    public GameObject bulletPrefab;
    private Animator _animator;
    private Vector2 lookDirection = new Vector2();

    void Start()
    {
        // Application.targetFrameRate = 144;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 移动
        Move();
        // 无敌
        Invincible();
        // 攻击
        if (Input.GetKeyDown(KeyCode.H))
        {
            Launch();
        }
    }

    /**
     * 移动
     */
    private void Move()
    {
        // 获取键入的方向
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        // 当玩家输入的某个轴向值不为0
        if (!Mathf.Approximately(horizontal, 0) || !Mathf.Approximately(vertical, 0))
        {
            lookDirection.Set(horizontal, vertical);
            lookDirection.Normalize();
        }

        _animator.SetFloat("Look X", lookDirection.x);
        _animator.SetFloat("Look Y", lookDirection.y);
        _animator.SetFloat("Speed", move.magnitude);


        Vector2 position = transform.position;
        position += move * (speed * Time.deltaTime);
        // position.x += speed * horizontal * Time.deltaTime;
        // position.y += speed * vertical * Time.deltaTime;
        _rigidbody2D.MovePosition(position);
    }

    /**
     * 判断无敌
     */
    private void Invincible()
    {
        if (isInvincible)
        {
            currentInvincibleTime -= Time.deltaTime;
            if (currentInvincibleTime <= 0)
            {
                isInvincible = false;
            }
        }
    }

    public void ChangeHealth(int changeValue)
    {
        // 掉血
        if (changeValue < 0)
        {
            _animator.SetTrigger("Hit");
            // 无敌
            if (isInvincible)
            {
                return;
            }

            currentInvincibleTime = invincibleTime;
            isInvincible = true;
        }

        currentHealth = Mathf.Clamp(currentHealth + changeValue, 0, maxHealth);
        Debug.Log("Ruby当前血量 = " + currentHealth);
    }

    private void Launch()
    {
        _animator.SetTrigger("Launch"); 
        GameObject gameObject = Instantiate(bulletPrefab, _rigidbody2D.position, Quaternion.identity);
        Bullet bullet = gameObject.GetComponent<Bullet>();
        bullet.Launch(lookDirection, 300);
    }
}