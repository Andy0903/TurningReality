using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    Transform[] players;
    [SerializeField]
    Vector3 cameraOffset = Vector3.zero;
    [SerializeField]
    float minSizeY = 5f;

    private void Start()
    {
        if (players.Length == 0)
        {
            Debug.Log("Player not assigned to the camera!, drag player(s) into the players[]");
        }
    }

    void Update()
    {
        ForcePlayersWithinCameraBounds();
        SetCameraPosition();
    }

    void ForcePlayersWithinCameraBounds() //TODO The player closest to the camera in Z-axis can get pushed around by the camera border.
    {
        if (players.Length < 2)
            return;

        for (int i = 0; i < players.Length; i++)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(players[i].transform.position);

            viewportPos.x = Mathf.Clamp01(viewportPos.x);
            viewportPos.y = Mathf.Clamp01(viewportPos.y);

            players[i].transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
        }
    }

    void SetCameraPosition()
    {
        Camera.main.transform.position = GetNewCameraPos();
    }

    Vector3 GetNewCameraPos()
    {
        Vector3 position = Vector3.zero;
        float zValue = float.PositiveInfinity;
        for (int i = 0; i < players.Length; i++)
        {
            position += players[i].position;
            if (zValue > players[i].position.z)
            {
                zValue = players[i].position.z;
            }
        }

        position.x /= players.Length;
        position.y /= players.Length;
        position.z = zValue;

        return cameraOffset + position;
    }
}
