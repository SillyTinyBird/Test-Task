using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarStatus : MonoBehaviour
{
    public GameObject _healthBar;
    public PlayerStats _stats;
    [SerializeField] PhotonView _view;

    private void Update()
    {
        if (!_view.IsMine){
            return;
        }
        UpdateHealthBar(_stats.GetHealth());
    }
    private void UpdateHealthBar(int health)//health is range between 100 and 0
    {
        _healthBar.transform.localScale = new Vector3(health / 100f, 0.25f, 1);
        //_healthBar.transform.localPosition = new Vector3(-(1 - (health / 100f)) / 2, 0, 0);
    }
}
