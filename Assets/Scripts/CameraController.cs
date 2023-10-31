using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed;
    [SerializeField] private bool doMovement;
    
    [SerializeField] private float scrollSpeed;
    private float _minY;
    private float _maxY;
    private void Awake()
    {
        panSpeed = 30f;
        scrollSpeed = 30f;
        doMovement = true;
        _minY = 10f;
        _maxY = 90f;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }
        if(!doMovement)
            return;
        
        MoveInDirections();
        ZoomInOut();
    }

    private void MoveInDirections()
    {
        if (Input.GetKey(KeyCode.W) && transform.position.z < 0)
        {
            transform.Translate(Vector3.forward * (panSpeed * Time.deltaTime), Space.World);
        }
        if (Input.GetKey(KeyCode.S) && transform.position.z > -70)
        {
            transform.Translate(Vector3.back * (panSpeed * Time.deltaTime), Space.World);
        }
        if (Input.GetKey(KeyCode.A) && transform.position.x > 0)
        {
            transform.Translate(Vector3.left * (panSpeed * Time.deltaTime), Space.World);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < 70)
        {
            transform.Translate(Vector3.right * (panSpeed * Time.deltaTime), Space.World);
        }
    }

    private void ZoomInOut()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 100 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, _minY, _maxY);
        
        transform.position = pos;
    }
}
