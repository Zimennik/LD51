using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _yOffset = 0.2f;
    private Transform _currentTarget;

    private void Awake()
    {
        _currentTarget = _playerTransform;
    }


    public void SetTarget(Transform target)
    {
        _currentTarget = target;
    }

    void FixedUpdate()
    {
        //smooth follow the target in 2D space
        //ignore Z axis

        transform.position = Vector3.Lerp(transform.position,
            new Vector3(_currentTarget.position.x, _currentTarget.position.y + _yOffset, transform.position.z),
            _speed * Time.deltaTime);
    }
}