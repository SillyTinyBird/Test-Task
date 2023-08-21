using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UIElements;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _playerUIPrefab;
    [SerializeField] private GameObject _playerUIPlatePrefab;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private void Start()
    {
        Vector2 randomPos = new Vector2(Random.Range(minX,maxX), Random.Range(minY, maxY));
        Vector2 uiOffset = new Vector2(randomPos.x, randomPos.y + 0.65f);
        GameObject player = PhotonNetwork.Instantiate(_playerPrefab.name, randomPos, Quaternion.identity);
        GameObject healthUI = PhotonNetwork.Instantiate(_playerUIPrefab.name, uiOffset, Quaternion.identity);
        GameObject plateUI = PhotonNetwork.Instantiate(_playerUIPlatePrefab.name, uiOffset, Quaternion.identity);
        healthUI.GetComponent<HealthBarStatus>()._healthBar = plateUI;//ugly but im tight on time. sorry
        plateUI.GetComponent<FollowTransform>()._transformToFollow = healthUI;//this mess is here because i didnt found a way to sync child objects
        healthUI.GetComponent<FollowTransform>()._transformToFollow = player;//gotta find a better way to do this
        healthUI.GetComponent<HealthBarStatus>()._stats = player.GetComponent<PlayerStats>();
    }
}
