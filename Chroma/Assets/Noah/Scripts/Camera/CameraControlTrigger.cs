using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEditor;

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects CustomInspectorObjects;

    [SerializeField] private GameObject HouseOutside;

    private Collider2D _coll;
    [SerializeField] private CinemachineVirtualCamera HouseCamera;

    private void Start()
    {
        _coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (CameraManager.Instance.currentCamera == HouseCamera)
        {
            HouseOutside.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (CustomInspectorObjects.PanCameraOnContact)
            {
                CameraManager.Instance.PanCameraOnContact(CustomInspectorObjects.panDistance, CustomInspectorObjects.panDistance, CustomInspectorObjects.panDirection, false );
            }
            
            if (CameraManager.Instance.currentCamera == HouseCamera)
            {
                HouseOutside.SetActive(false);
            }
        }
    }
    

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Vector2 exitDirection = (col.transform.position - _coll.bounds.center).normalized;
            if (CustomInspectorObjects.SwapCameras && CustomInspectorObjects.cameraOnLeft != null &&
                CustomInspectorObjects.cameraOnRight != null)
            {
                CameraManager.Instance.SwapCamera(CustomInspectorObjects.cameraOnLeft, CustomInspectorObjects.cameraOnRight, exitDirection);
            }
            if (CustomInspectorObjects.PanCameraOnContact)
            {
                CameraManager.Instance.PanCameraOnContact(CustomInspectorObjects.panDistance, CustomInspectorObjects.panDistance, CustomInspectorObjects.panDirection, true );
            }

            if (HouseOutside != null)
            {
                HouseOutside.SetActive(true);
            }
        }
    }
}

[System.Serializable]
public class CustomInspectorObjects
{
    public bool SwapCameras = false;
    public bool PanCameraOnContact = false;

    [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
    [HideInInspector] public CinemachineVirtualCamera cameraOnRight;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;

}

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}
#if UNITY_EDITOR
[CustomEditor(typeof(CameraControlTrigger))]
public class MyScriptEditor : Editor
{
    private CameraControlTrigger _cameraControlTrigger;

    private void OnEnable()
    {
        _cameraControlTrigger = (CameraControlTrigger)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (_cameraControlTrigger.CustomInspectorObjects.SwapCameras)
        {
            _cameraControlTrigger.CustomInspectorObjects.cameraOnLeft = EditorGUILayout.ObjectField("Camera on Left", _cameraControlTrigger.CustomInspectorObjects.cameraOnLeft, typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
            
            _cameraControlTrigger.CustomInspectorObjects.cameraOnRight = EditorGUILayout.ObjectField("Camera on Right", _cameraControlTrigger.CustomInspectorObjects.cameraOnRight, typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
        }

        if (_cameraControlTrigger.CustomInspectorObjects.PanCameraOnContact)
        {
            _cameraControlTrigger.CustomInspectorObjects.panDirection =
                (PanDirection)EditorGUILayout.EnumPopup("Camera Pan Direction",
                    _cameraControlTrigger.CustomInspectorObjects.panDirection);

            _cameraControlTrigger.CustomInspectorObjects.panDistance = EditorGUILayout.FloatField("Pan Distance",
                _cameraControlTrigger.CustomInspectorObjects.panDistance);
            _cameraControlTrigger.CustomInspectorObjects.panTime = EditorGUILayout.FloatField("Pan Time",
                _cameraControlTrigger.CustomInspectorObjects.panTime);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_cameraControlTrigger);
        }
    }
}
#endif

