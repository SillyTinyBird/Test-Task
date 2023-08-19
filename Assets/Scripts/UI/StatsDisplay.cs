using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _health;
    [SerializeField] TMPro.TextMeshProUGUI _coins;
    [SerializeField] PlayerStats _plStats;

    private void FixedUpdate()
    {
        _health.SetText(_plStats.GetHealth().ToString());
        _coins.SetText(_plStats.GetCoins().ToString());
    }
}
