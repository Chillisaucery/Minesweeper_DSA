using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float dragSpeed = 0.01f;
    public float zoomSpeed = 0.1f;
    public (float, float) zoomLimits = (1f, 15f);
    public float maximumDeviant = 10f;

    private Vector3 newMousePoint = new Vector3(0,0,0);

    private Vector3 origin = new Vector3(0, 0, 0);

    private float upperBound = 0;
    private float lowerBound = 0;
    private float leftBound = 0;
    private float rightBound = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            origin = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            newMousePoint = Input.mousePosition;
            Vector3 newPosition = gameObject.transform.position + (origin - newMousePoint) * dragSpeed;
            transform.position = newPosition;
        }

        gameObject.GetComponent<Camera>().orthographicSize -= Input.mouseScrollDelta.y * zoomSpeed;

        ConstrainPosition();
        ConstrainZoom();

    }

    public void SetOrigin(GameObject origin)
    {
        transform.position = new Vector3 (origin.transform.position.x, origin.transform.position.y, transform.position.z);
    }

    public void SetPositionConstrain(GameObject topLeft, GameObject bottomRight)
    {
        upperBound = topLeft.transform.position.y + maximumDeviant;
        lowerBound = bottomRight.transform.position.y - maximumDeviant;

        leftBound = topLeft.transform.position.x - maximumDeviant;
        rightBound = bottomRight.transform.position.x + maximumDeviant;

        Debug.Log("Boundaries: " + upperBound + " " + lowerBound + " " + leftBound + " " + rightBound);
    }

    private void ConstrainPosition()
    {
        if (gameObject.transform.position.y > upperBound)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, upperBound, gameObject.transform.position.z);


        if (gameObject.transform.position.y < lowerBound)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, lowerBound, gameObject.transform.position.z);

        if (gameObject.transform.position.x > rightBound)
            gameObject.transform.position = new Vector3(rightBound, gameObject.transform.position.y, gameObject.transform.position.z);

        if (gameObject.transform.position.x < leftBound)
            gameObject.transform.position = new Vector3(leftBound, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    public void ConstrainZoom()
    {
        if (gameObject.GetComponent<Camera>().orthographicSize < zoomLimits.Item1)
            gameObject.GetComponent<Camera>().orthographicSize = zoomLimits.Item1;

        if (gameObject.GetComponent<Camera>().orthographicSize > zoomLimits.Item2)
            gameObject.GetComponent<Camera>().orthographicSize = zoomLimits.Item2;
    }

    public void SetDeviant(float deviant)
    {
        this.maximumDeviant = deviant;
    }

}
