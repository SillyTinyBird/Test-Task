using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField _createInputFild;
    [SerializeField] TMP_InputField _joinInputField;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(_createInputFild.text);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_joinInputField.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
