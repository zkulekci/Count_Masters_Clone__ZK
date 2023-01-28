using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float playerForwardSpeed = 15f;
    [SerializeField] private float playerSwipeSpeed = 1f;
    [SerializeField] private float moveLeftLimit = -4f;
    [SerializeField] private float moveRightLimit = 4f;

    private float mouseStartPositionX = 0;
    private float mouseEndPositionX = 0;

    void Update()
    {
        if (transform.GetComponent<PlayerZone>().PlayerCount < 1)
            return;

        if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.IN_GAME_PHASE_1)
        {
            HorizontalMove();
            ForwardMove();
        }
        else if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.IN_GAME_PHASE_2)
        {
            ForwardMove();
        }
    }

    private void ForwardMove()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * playerForwardSpeed);
    }

    private void HorizontalMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPositionX = Input.mousePosition.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            ResetMousePositions();
        }
        if (Input.GetMouseButton(0))
        {
            mouseEndPositionX = Input.mousePosition.x - mouseStartPositionX;
            mouseStartPositionX = Input.mousePosition.x;
            if (mouseEndPositionX < -0.1f)
            {
                if (!canMoveLeft())
                    mouseEndPositionX = 0f;
            }
            if (mouseEndPositionX > 0.1f)
            {
                if (!canMoveRight())
                    mouseEndPositionX = 0f;
            }
        }

        if (mouseEndPositionX >= 0.1f || mouseEndPositionX <= -0.1f)
        {
            transform.position = new Vector3(transform.position.x + (playerSwipeSpeed * mouseEndPositionX / Screen.width), transform.position.y, transform.position.z);
        }
    }

    private void ResetMousePositions()
    {
        mouseStartPositionX = 0f;
        mouseEndPositionX = 0f;
    }

    private bool canMoveLeft()
    {
        return moveLeftLimit < LeftBound();
    }

    private bool canMoveRight()
    {
        return moveRightLimit > RightBound();
    }

    private float LeftBound()
    {
        float minX = transform.GetChild(1).transform.position.x;
        float boundOffset = 1f;

        for (int i = 2; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).transform.position.x < minX)
                minX = transform.GetChild(i).transform.position.x;
        }
        return minX - boundOffset;
    }

    private float RightBound()
    {
        float maxX = transform.GetChild(1).transform.position.x;
        float boundOffset = 1f;

        for (int i = 2; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).transform.position.x > maxX)
                maxX = transform.GetChild(i).transform.position.x;
        }
        return maxX + boundOffset;
    }
}
