using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _playerSpeed = 2.0f;

    void Update()
    {
        if(_rigidbody == null)
        {
            Debug.LogWarning("No Rigidbody2D attached to PlayerController script.");
            return;
        }
        if (_playerInput == null)
        {
            Debug.LogWarning("No PlayerInput attached to PlayerController script.");
            return;
        }
        Vector2 direction = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        _rigidbody.MovePosition(currentPos + (direction * Time.deltaTime * _playerSpeed));
        Debug.Log(Vector2.Angle(new Vector2(transform.forward.x,transform.right.y), direction));
        Vector2 currentDir = new Vector2(transform.right.x, transform.right.y);
        float negativeOrPositiveSemicircle = direction.y > 0 ? 1 : -1;
        if(direction != Vector2.zero)
        {
            _rigidbody.MoveRotation(negativeOrPositiveSemicircle * Vector2.Angle(Vector2.right, direction));
        }
        
    }
}
