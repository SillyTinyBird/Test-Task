using Photon.Pun;
using UnityEngine;

public class IsMineMarker : MonoBehaviour
{
    [SerializeField] private PhotonView _view;
    void Start()
    {
        if (!_view.IsMine)
        {
            Destroy(gameObject);
        }
    }
}
