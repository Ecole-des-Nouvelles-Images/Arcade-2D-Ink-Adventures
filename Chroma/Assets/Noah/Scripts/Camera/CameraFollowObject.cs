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
    [SerializeField] private float _flipRotationTime = 0.5f;

    private Coroutine _turnCoroutine;
    private PlayerController _playerController;
    private bool _isFacingRight;

    private void Awake()
    {
        _playerController = _playerTransform.gameObject.GetComponent<PlayerController>();
        _isFacingRight = _playerController.IsFacingRight;
    }

    private void Update()
    {
        transform.position = _playerTransform.position;
    }

    public void CallTurn()
    {
        _turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < _flipRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / _flipRotationTime));
            transform.rotation = quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
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
