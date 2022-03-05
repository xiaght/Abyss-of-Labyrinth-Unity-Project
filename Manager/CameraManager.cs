using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;
    public float x=0;
    public float y=0;
    public float z=-10;

    Vector3 cameraPosition;

    private void LateUpdate()
    {
        cameraPosition.x = player.transform.position.x + x;
        cameraPosition.y = player.transform.position.y + y;
        cameraPosition.z = player.transform.position.z + z;

        transform.position = cameraPosition;
    }


}
