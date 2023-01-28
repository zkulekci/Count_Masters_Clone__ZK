using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float boundLeft = -6f;
    [SerializeField] private float boundRight = 6f;
    [SerializeField] private float horizontalSpeed = 20f;
    
    private GameObject targetToFollow = null;
    private Vector3 resetPosition = new Vector3(0f, 16, -12);
    private Quaternion resetRotation =  new Quaternion(0.34202f, 0f, 0f, 0.93969f);

    public void Setup()
    {
        transform.position = resetPosition;
        transform.rotation = resetRotation;

        targetToFollow = GameManager.Instance.playerZoneGO;
        offset = transform.position - targetToFollow.transform.position;
    }

    void LateUpdate()
    {
        if (targetToFollow == null)
            Setup();

        Vector3 pos = targetToFollow.transform.position + offset;

        if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.IN_GAME_PHASE_1)
        {
            transform.position = CalculatePositionForGamePhase1(pos);
        }
        else if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.IN_GAME_PHASE_2)
        {
            transform.position = CalculatePositionForGamePhase2(pos);
            SetRotationForGamePhase2();
        }
    }

    private Vector3 CalculatePositionForGamePhase1(Vector3 pos)
    {
        if (pos.x < boundLeft)
            pos.x = boundLeft;
        else if (pos.x > boundRight)
            pos.x = boundRight;
        pos.x = Mathf.Lerp(transform.position.x, pos.x, Time.fixedDeltaTime * horizontalSpeed);
        return pos;
    }

    private Vector3 CalculatePositionForGamePhase2(Vector3 pos)
    {
        float speed = 2f;
        pos += new Vector3(-offset.z, 0f, -offset.z);
        pos = Vector3.Lerp(transform.position, pos, Time.fixedDeltaTime * speed);
        return pos;
    }

    private void SetRotationForGamePhase2()
    {
        float speed = 3f;
        Quaternion toRotation = Quaternion.LookRotation(targetToFollow.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.fixedDeltaTime * speed);
    }
}
