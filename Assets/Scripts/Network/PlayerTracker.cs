using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTracker : MonoBehaviourPunCallbacks, IOnEventCallback
{
    List<string> _alivePLayers = new List<string>();
    private bool _isGameStarted = false;
    private bool _isGameEnded = false;

    private const byte DeathEventCode = 1;
    private const byte GameStartEventCode = 2;
    private const byte GameEndEventCode = 3;
    
    
    private void Update()
    {
        if(_alivePLayers.Count > 1 && !_isGameStarted)
        {
            _isGameStarted=true;
            GameStartEvent();
        }
        if(_alivePLayers.Count <= 1 && _isGameStarted)
        {
            Debug.Log(_alivePLayers.Count);
            GameEndEvent(_alivePLayers[0],GetLastPLayerCoins());
            _isGameStarted = false;
            _isGameEnded = true;
        }
    }
    private int GetLastPLayerCoins()
    {
        PhotonView[] photonViews = FindObjectsOfType<PhotonView>();// takes a lot of time. can be done better
        foreach (PhotonView view in photonViews)
        {
            if (view.gameObject.GetComponent<PlayerStats>() != null)
            {
                return view.gameObject.GetComponent<PlayerStats>().GetCoins();
            }
        }
        return 0;
    }
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(gameObject);
        }
        PhotonNetwork.LocalPlayer.NickName = PhotonNetwork.PlayerList.Count().ToString();
        _alivePLayers.Add(PhotonNetwork.LocalPlayer.NickName);
        _alivePLayers.ForEach(a => { Debug.Log(a); });
        Debug.Log(GetLastPLayerCoins());
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (_isGameStarted)
        {
            GameStartEvent();
        }
        if(_isGameEnded) {
            GameEndEvent(_alivePLayers[0], GetLastPLayerCoins());
        }
        newPlayer.NickName = PhotonNetwork.PlayerList.Count().ToString();
        _alivePLayers.Add(newPlayer.NickName);
        _alivePLayers.ForEach(a => { Debug.Log(a); });
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (!_alivePLayers.Contains(otherPlayer.NickName))
        {
            return;
        }
        _alivePLayers.Remove(otherPlayer.NickName);
        _alivePLayers.ForEach(a => { Debug.Log(a); });
    }

    private void GameStartEvent()
    {
        object[] content = new object[] {};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(GameStartEventCode, content, raiseEventOptions, SendOptions.SendReliable);
    }
    private void GameEndEvent(string playerName, int coins)
    {
        object[] content = new object[] { playerName, coins };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(GameEndEventCode, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == DeathEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            string deadPlayerNickName = (string)data[0];
            if (_alivePLayers.Contains(deadPlayerNickName))
            {
                _alivePLayers.Remove(deadPlayerNickName);
            }
        }
    }
}
