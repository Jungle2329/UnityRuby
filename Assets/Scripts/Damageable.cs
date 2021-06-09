using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        var ruby = collision.GetComponent<RubyController>();
        if (ruby == null) return;
        ruby.ChangeHealth(-1);
        Debug.Log("当前血量=" + ruby.currentHealth);
    }
}