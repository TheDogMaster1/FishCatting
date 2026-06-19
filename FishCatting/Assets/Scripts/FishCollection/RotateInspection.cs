using UnityEngine;

public class RotateInspection : MonoBehaviour
{
    Vector3 mousePrevPosition;
    Vector3 mouseNewPosition;
    private Touch tap;
    private bool isTapDown = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if(!isTapDown)
            {
                mousePrevPosition = Input.mousePosition;
                isTapDown = true;
            }
            tap = Input.GetTouch(0);
            mouseNewPosition = Input.mousePosition - mousePrevPosition;
            RotateItem();
        }
        else if (Input.GetMouseButton(0))
        {
            mouseNewPosition = Input.mousePosition - mousePrevPosition;
            RotateItem();
        }
        else
        {
            isTapDown = false;
        }
        mousePrevPosition = Input.mousePosition;
    }
    public void RotateItem()
    {
        if (Vector3.Dot(transform.up, Vector3.up) >= 0)
        {
            transform.Rotate(transform.up, -Vector3.Dot(mouseNewPosition, Camera.main.transform.right), Space.World);
        }
        else
        {
            transform.Rotate(transform.up, Vector3.Dot(mouseNewPosition, Camera.main.transform.right), Space.World);
        }

        transform.Rotate(Camera.main.transform.right, Vector3.Dot(mouseNewPosition, Camera.main.transform.up), Space.World);
    }
}
