using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private float speed;

    private float moveHorizontal;
    private float moveVertical;

    private float cameraFOV;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float minFOV;
    [SerializeField] private float maxFOV;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private float startSize;

    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
        transform.position = startPos;
        cameraFOV = startSize;
    }

    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        transform.position += (transform.up * moveVertical * speed);
        transform.position += (transform.right * moveHorizontal * speed);

        cameraFOV += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        cameraFOV = Mathf.Clamp(cameraFOV, minFOV, maxFOV);
        camera.orthographicSize = cameraFOV;
    }
}
