using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    Transform[] players;

    const float distanceMargin = 3.0f;
    const float yOffset = 2.0f;
    float aspectRatio;
    float tanFov;
    GameObject mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.gameObject;

        if (players.Length == 0)
        {
            Debug.Log("Player not assigned to the camera!, drag player(s) into the players[]");
        }

        aspectRatio = Screen.width / Screen.height;
        tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
    }

    void FixedUpdate()
    {
        if (mainCamera.GetComponent<Camera>().enabled == false) return;

        Vector3 vectorBetweenPlayers = players[1].position - players[0].position;
        Vector3 middlePoint = players[0].position + 0.5f * vectorBetweenPlayers;

        Vector3 newCameraPos = new Vector3(
            middlePoint.x,
            middlePoint.y + yOffset,
            Camera.main.transform.position.z);

        float distanceBetweenPlayers = vectorBetweenPlayers.magnitude;

        //FoV = 2 * arctan((0.5 * distanceBetweenPlayers) / (distanceFromMiddlePoint * aspectRatio))
        float cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;

        Vector3 dirFromMidToPos = (newCameraPos - middlePoint).normalized;
        Camera.main.transform.position = middlePoint + dirFromMidToPos * (cameraDistance + distanceMargin);
    }
}
