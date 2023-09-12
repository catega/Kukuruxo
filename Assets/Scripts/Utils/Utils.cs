using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Utils
{
    public static Vector3 GetMousePosition() {
        Camera camera = Camera.main;

        Vector3 mousePosition = Input.mousePosition;
        return camera.ScreenToWorldPoint(mousePosition) - new Vector3(0, 0, camera.transform.position.z);
    }
}