using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    int _health = 100;
    int _coins = 0;
    [SerializeField] int _bulletDamage;

    public int GetHealth() => _health;
    public int GetCoins() => _coins;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Coin"))
        {
            _coins += 1;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            _health -= _bulletDamage;
            Destroy(collision.gameObject);
        }
    }
}
