using UnityEngine;

public class GetClickPosition
{
    private Touch tap;

    public Vector3 GetTapPos(string tag)
    {
        if (Input.touchCount > 0)
        {
            tap = Input.GetTouch(0);
        }
        if (tap.phase == TouchPhase.Ended)
        {
            return GetPosition(tap.position, tag);
        }
        else if (Input.GetMouseButtonDown(0) && tap.tapCount == 0)
        {
            return GetPosition(Input.mousePosition, tag);
        }
        return Vector3.zero;
    }

    private Vector3 GetPosition(Vector3 pos, string tag)
    {
        Ray posRay = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(posRay, out RaycastHit hitInfo) && hitInfo.transform.CompareTag(tag))
        {
            return hitInfo.point;
        }
        return Vector3.zero;
    }
}
