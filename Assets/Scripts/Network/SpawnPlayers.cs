using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private void Start()
    {
        Vector2 randomPos = new Vector2(Random.Range(minX,maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(_playerPrefab.name, randomPos, Quaternion.identity);
    }
}
