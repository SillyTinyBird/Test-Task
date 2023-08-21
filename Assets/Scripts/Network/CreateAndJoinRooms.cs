using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.Linq;

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
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ShowAndroidToastMessage(message);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ShowAndroidToastMessage(message);
    }
    private void ShowAndroidToastMessage(string message)//yeah i just copied it from stackoverlow sorry
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}
