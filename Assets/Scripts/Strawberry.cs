using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ruby = collision.GetComponent<RubyController>();
        if (ruby == null || ruby.Health >= ruby.maxHealth) return;
        ruby.ChangeHealth(1);
        Debug.Log("Ruby当前血量 = " + ruby.Health);
        Destroy(gameObject);
    }
}