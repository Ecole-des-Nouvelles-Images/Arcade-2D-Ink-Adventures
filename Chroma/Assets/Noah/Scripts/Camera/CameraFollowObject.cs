using System;
using System.Collections;
using System.Collections.Generic;
using Noah.Scripts.Player;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _playerTransform;

    [Header("Flip Rotation Stats")] 
    [SerializeField] private float _flipYRotationTime = 0.5f;

    private Coroutine _turnCoroutine;
    private PlayerControllerElias _playerControllerElias;
    private bool _isFacingRight;

    private void Awake()
    {
        _playerControllerElias = _playerTransform.gameObject.GetComponent<PlayerControllerElias>();
        //_isFacingRight = _playerControllerElias.IsFacingRight;
    }

    private void Update()
    {
        transform.position = _playerTransform.position;
    }

    public void CallTurn()
    {
        LeanTween.rotateY(gameObject, DetermineEndRotation(), _flipYRotationTime).setEaseInOutSine();
    }
    private float DetermineEndRotation()
    {
        _isFacingRight = !_isFacingRight;

        if (_isFacingRight)
        {
            return 180f;
        }

        else
        {
            return 0f;
        }
    }
}