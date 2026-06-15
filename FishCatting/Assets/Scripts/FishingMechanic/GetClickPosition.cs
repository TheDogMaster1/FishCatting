using UnityEngine;

public class GetClickPosition
{
    private Touch tap;

    public Vector3 GetTapPos()
    {
        if (Input.touchCount > 0)
        {
            tap = Input.GetTouch(0);
        }
        if (tap.phase == TouchPhase.Ended)
        {
            return GetPosition(tap.position);
        }
        else if (Input.GetMouseButtonDown(0) && tap.tapCount == 0)
        {
            return GetPosition(Input.mousePosition);
        }
        return Vector3.zero;
    }

    private Vector3 GetPosition(Vector3 pos)
    {
        Ray posRay = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(posRay, out RaycastHit hitInfo))
        {
            return hitInfo.point;
        }
        return Vector3.zero;
    }
}
