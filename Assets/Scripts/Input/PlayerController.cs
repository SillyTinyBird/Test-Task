using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _distance;
    [SerializeField] private PhotonView _view;

    private void Start()
    {
        _playerInput.actions["Shoot"].performed += _ => ShootAction();
        _playerInput.actions["Shoot"].Enable();
    }
    private void OnDestroy()
    {
        _playerInput.actions["Shoot"].Disable();
    }
    void FixedUpdate()
    {
        if(!_view.IsMine) {
            return;
        }
        if(_rigidbody == null){
            Debug.LogWarning("No Rigidbody2D attached to PlayerController script.");
            return;
        }
        if (_playerInput == null){
            Debug.LogWarning("No PlayerInput attached to PlayerController script.");
            return;
        }
        Vector2 direction = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        _rigidbody.MovePosition(currentPos + (direction * Time.deltaTime * _playerSpeed));
        Vector2 currentDir = new Vector2(transform.right.x, transform.right.y);
        float negativeOrPositiveSemicircle = direction.y > 0 ? 1 : -1;
        if(direction != Vector2.zero)
        {
            _rigidbody.MoveRotation(negativeOrPositiveSemicircle * Vector2.Angle(Vector2.right, direction));
        }
    }
    void ShootAction()
    {
        if (!_view.IsMine){ 
            return;
        }
        Vector3 rotatedDirectionVector = Quaternion.Euler(0, 0, 90) * transform.right;
        PhotonNetwork.Instantiate("Projectile", transform.position + transform.right * _distance , Quaternion.LookRotation(Vector3.forward, rotatedDirectionVector));
    }
}
