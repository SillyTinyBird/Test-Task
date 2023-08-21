using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    int _health = 100;
    int _coins = 0;
    [SerializeField] int _bulletDamage;
    [SerializeField] SpriteRenderer _playerSprite;
    [SerializeField] PhotonView _view;
    [SerializeField] GameObject _coinPrefab;

    private const byte DeathEventCode = 1;

    private void Start()
    {
        _playerSprite.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    public int GetHealth() => _health;
    public int GetCoins()
    {
        Debug.Log(_coins);
        return _coins;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Coin"))
        {
            _coins += 1;
            Destroy(collision.gameObject);
            if (_view.IsMine)
            {
                Vector3 pos = new Vector3(transform.position.x + _coinPrefab.transform.position.x + (0.15f * _coins), transform.position.y + _coinPrefab.transform.position.y, transform.position.z + _coinPrefab.transform.position.z);
                GameObject coinIcon = PhotonNetwork.Instantiate(_coinPrefab.name, pos, Quaternion.identity);
                coinIcon.GetComponent<FollowTransform>()._transformToFollow = gameObject;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            _health -= _bulletDamage;
            Destroy(collision.gameObject);
        }
        if(_health <= 0)
        {
            if (_view.IsMine)
            {
                DeathEvent();
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
    private void DeathEvent()
    {
        object[] content = new object[] { PhotonNetwork.LocalPlayer.NickName};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(DeathEventCode, content, raiseEventOptions, SendOptions.SendReliable);
    }
}
