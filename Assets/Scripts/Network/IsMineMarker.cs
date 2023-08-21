using Photon.Pun;
using UnityEngine;

public class IsMineMarker : MonoBehaviour
{
    [SerializeField] private PhotonView _view;
    [SerializeField] private GameObject _marker;
    void Start()
    {
        if (!_view.IsMine)
        {
            Destroy(_marker);
        }
    }
}
