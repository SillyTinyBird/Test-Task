using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] float _projectileSpeed = 1f;

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + transform.right * _projectileSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
