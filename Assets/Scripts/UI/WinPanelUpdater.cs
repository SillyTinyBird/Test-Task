using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanelUpdater : MonoBehaviour, IOnEventCallback
{
    [SerializeField] TMPro.TextMeshProUGUI _coins;
    [SerializeField] TMPro.TextMeshProUGUI _name;
    [SerializeField] GameObject _waitingForPLayersGroup;
    [SerializeField] GameObject _GameDoneGroup;

    private const byte GameStartEventCode = 2;
    private const byte GameEndEventCode = 3;

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == GameStartEventCode)
        {
            StartGame();
        }
        if (eventCode == GameEndEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            string lastPlayerNickName = (string)data[0];
            int coins = (int)data[1];
            EndGame(lastPlayerNickName, coins);
        }
    }
    public void StartGame()
    {
        _waitingForPLayersGroup.SetActive(false);
    }
    public void EndGame(string playerName, int coins)
    {
        _coins.text = "They collected " + coins.ToString() + " coins!";
        _name.text = playerName + " Won!";
        _GameDoneGroup.SetActive(true);
    }

}
