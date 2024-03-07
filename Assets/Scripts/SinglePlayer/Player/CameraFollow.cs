using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    private Player_Movement_Single player_Movement;

    private bool isFacingRight;
    [SerializeField] private float flipTime = 0.5f;
    private CinemachineFramingTransposer framingTransposer;

    private void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        player_Movement = playerTransform.GetComponent<Player_Movement_Single>();
        player_Movement.cameraFollow = this;
        isFacingRight = !player_Movement.lookRight;

    }
    public void CallTurn()
    {
        float startPoint = framingTransposer.m_ScreenX;
        float endPoint = DetermineEndPoint();
        LeanTween.value(this.gameObject, updateXCallback, startPoint, endPoint, flipTime).setEase(LeanTweenType.easeInSine);
    }

    private void updateXCallback(float obj)
    {
        framingTransposer.m_ScreenX = obj;
    }

    private float DetermineEndPoint()
    {

        if (!player_Movement.lookRight)
        {
            return 0.45f;
        }

        else
        {
            return 0.55f;
        }
    }



}
