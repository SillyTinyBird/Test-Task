using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FollowTransform : MonoBehaviour
{
    public GameObject _transformToFollow;
    [SerializeField] Vector3 _offset;
    [SerializeField] PhotonView _view;
    private void Start()
    {
        if (!_view.IsMine){
            return;
        }
        _offset = transform.position - _transformToFollow.transform.position;
    }
    void Update()
    {
        if (!_view.IsMine) {
            return;
        }
        if(_transformToFollow == null)
        {
            PhotonNetwork.Destroy(gameObject);
            return;
        }
        transform.position = _transformToFollow.transform.position + _offset;
    }
}
