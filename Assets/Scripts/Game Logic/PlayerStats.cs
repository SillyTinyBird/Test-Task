using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    int _health = 100;
    int _coins = 0;
    [SerializeField] GameObject _healthBar;
    [SerializeField] int _bulletDamage;
    [SerializeField] SpriteRenderer _playerSprite;

    private void Start()
    {
        _playerSprite.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
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
            UpdateHealthBar();
            Destroy(collision.gameObject);
        }
    }
    private void UpdateHealthBar()
    {
        _healthBar.transform.localScale = new Vector3(_health/100f, 0.25f, 1);
        _healthBar.transform.localPosition = new Vector3(-(1-(_health/100f)) / 2, 0, 0);
    }
}
