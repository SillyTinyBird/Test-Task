using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform _transformToFollow;
    private Vector3 _offset;
    private void Start()
    {
        _offset = transform.position - _transformToFollow.position;
    }
    void Update()
    {
        transform.position = _transformToFollow.position + _offset;
    }
}
