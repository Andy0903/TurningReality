using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Transform[] players;
    [SerializeField]
    float minSizeY = 5f;

    void Update()
    {
        SetCameraPos();
        SetCameraSize();
    }

    void SetCameraSize()
    {
        //Based on screen ratio
        float minSizeX = minSizeY * Screen.width / Screen.height;

        //0.5f multiplication due to ortographicalSize being half of actual size.
        float width = Mathf.Abs(players[0].position.x - players[1].position.x * 0.5f);
        float height = Mathf.Abs(players[0].position.y - players[1].position.y * 0.5f);

        //computing the size
        float camSizeX = Mathf.Max(width, minSizeX);
        Camera.main.orthographicSize = Mathf.Max(height, camSizeX * Screen.height / Screen.width, minSizeY);
    }

    void SetCameraPos()
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

        position.x *= 0.5f;
        position.y *= 0.5f;

        return position;
    }
}
