using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;

    public float minXRot;
    public float maxXRot;

    public float curXRot;
    public float curYRot;

    public float minZoom;
    public float maxZoom;

    public float zoomSpeed;
    public float rotateSpeed;

    private float curZoom;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        curZoom = cam.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);
        cam.transform.localPosition = Vector3.up * curZoom + Vector3.back * curZoom;
        if(Input.GetKeyDown(KeyCode.R)){
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            curXRot = -y * rotateSpeed;
            curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);

            transform.eulerAngles = new Vector3(curXRot, transform.eulerAngles.y + 90, 0.0f);
        }
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            curXRot += -x * rotateSpeed;
            curYRot = cam.transform.eulerAngles.y;
            Debug.Log(curYRot);
            if (curYRot>= 0 && curYRot <= 90){
                transform.position = new Vector3(transform.position.x - x , transform.position.y  , transform.position.z - y);
                    Debug.Log(1);
            } else if (curYRot>= 90 && curYRot < 180){
                transform.position = new Vector3(transform.position.x - y, transform.position.y  , transform.position.z + x);
                    Debug.Log(2);
            } else if (curYRot>= 180 && curYRot< 270){
                transform.position = new Vector3(transform.position.x + x , transform.position.y  , transform.position.z + y);
                    Debug.Log(3);
            } else if (curYRot>= 270 && curYRot<= 360){
                transform.position = new Vector3(transform.position.x + y , transform.position.y  , transform.position.z - x);
                    Debug.Log(4);
            } 
        }
        Vector3 forward = cam.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();
        Vector3 right = cam.transform.right.normalized;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 dir = forward * moveZ + right * moveX;
        dir.Normalize();

        dir *= moveSpeed * Time.deltaTime;

        transform.position += dir;
    }
}
