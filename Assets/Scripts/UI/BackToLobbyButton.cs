using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToLobbyButton : MonoBehaviour
{
    public void BackToLobby()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby");
    }
}
