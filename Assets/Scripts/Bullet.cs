using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    // Awake方法会在对象实例化（Instantiate）后马上调用
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start方法只有在预先创建好的物体上会调用，代码实例化的时候不调用
    private void Start()
    {
    }

    // 每帧调用一次
    private void Update()
    {
        if (transform.position.magnitude > 10)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        _rigidbody2D.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 防止子弹和子弹发生碰撞消失
        if (collision.gameObject.name.Equals(gameObject.name))
        {
            return;
        }

        Debug.Log("子弹碰撞到了：" + collision.gameObject.name);
        var enemyController = collision.gameObject.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.Fix();
        }

        Destroy(gameObject);
    }
}