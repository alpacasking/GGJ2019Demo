using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private bool draggingItem = false;
    private GameObject draggedPassenge;
    private GameObject draggedBlock;
    private Vector2 touchOffset;
    private Vector3 originalPosition;
    [SerializeField]
    private TrainManager trainManager;
    [SerializeField]
    private StationManager stationManager;
    private int rotateTime = 0;
    public bool IsValid;
    private bool HasInput
    {
        get
        {
            // returns true if either the mouse button is down or at least one touch is felt on the screen
            return Input.GetMouseButton(0);
        }
    }

    void Update ()
	{
        if (!IsValid)
        {
            return;
        }
        if (HasInput)
	    {
	        DragOrPickUp();
            if (Input.GetKeyDown("space"))//Input.GetMouseButtonDown(1))
            {
                RotatePassenge();
            }
        }
	    else
	    {
	        if (draggingItem)
	            DropItem();
	    }
	}

    Vector2 CurrentTouchPosition
    {
        get
        {
           return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    private void DragOrPickUp()
    {
        var inputPosition = CurrentTouchPosition;
        if (draggingItem)
        {
            draggedPassenge.transform.position = inputPosition + touchOffset;
        }
        else
        {
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null &&  hit.transform.parent !=null&&hit.transform.parent.gameObject.tag == "passenge")
                {
                    draggedPassenge = hit.transform.parent.gameObject;
                    if (draggedPassenge != null && draggedPassenge.tag == "passenge")
                    {
                        draggedPassenge.GetComponent<Passenge>().Cache();
                        draggedBlock = hit.transform.gameObject;
                        originalPosition = draggedPassenge.transform.position;
                        draggingItem = true;
                        touchOffset = (Vector2)draggedPassenge.transform.position - inputPosition;
                    }
                    //draggedObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                }
            }
        }
    }
 
    void DropItem()
    {
        draggingItem = false;
        var inputPosition = CurrentTouchPosition;
        RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, Vector2.zero, 0.5f);
        Debug.Log(touches.Length);
        if (touches.Length == 2)
        {
            var hit = touches[1];
            if (hit.transform != null)
            {
                GameObject hitObj= hit.transform.gameObject;
                if (hitObj != null)
                {
                    if (hitObj.tag == "seat" && draggedPassenge.GetComponent<Passenge>().State == Passenge.PassengeState.Standing)
                    {
                        Seat seat = hitObj.GetComponent<Seat>();
                        Vector2 newPos;
                        if (trainManager.IsSeatEmpty(draggedPassenge.GetComponent<Passenge>(),
                        draggedBlock.GetComponent<Block>(), seat, out newPos))
                        {
                            Debug.Log("Yes");
                            trainManager.AddPassenge(draggedPassenge.GetComponent<Passenge>(),
                        draggedBlock.GetComponent<Block>(), seat);
                            draggedPassenge.transform.position = newPos;
                            stationManager.RemovePassenge(draggedPassenge.GetComponent<Passenge>());
                        }
                        else
                        {
                            Debug.Log("No");
                            draggedPassenge.transform.position = originalPosition;
                        }
                    }
                    else if (hitObj.tag == "seat" && draggedPassenge.GetComponent<Passenge>().State == Passenge.PassengeState.Sitting)
                    {
                        Seat seat = hitObj.GetComponent<Seat>();
                        Vector2 newPos;
                        if (trainManager.TryMovePassenge(draggedPassenge.GetComponent<Passenge>(),
                        draggedBlock.GetComponent<Block>(), seat, out newPos))
                        {
                            draggedPassenge.transform.position = newPos;
                        }
                        else
                        {
                            Debug.Log("No");
                            draggedPassenge.transform.position = originalPosition;
                        }
                    }
                    else if (hitObj.tag == "station" && draggedPassenge.GetComponent<Passenge>().State == Passenge.PassengeState.Sitting)
                    {
                        stationManager.AddPassenge(draggedPassenge.GetComponent<Passenge>());
                        trainManager.RemovePassenge(draggedPassenge.GetComponent<Passenge>());
                    }
                    else
                    {
                        Debug.Log("N1"+ hitObj.name);
                        draggedPassenge.transform.position = originalPosition;
                    }
                }
                else
                {
                    Debug.Log("N2");
                    draggedPassenge.transform.position = originalPosition;
                }
            }
            else
            {
                draggedPassenge.transform.position = originalPosition;
            }
        }
        else
        {
            draggedPassenge.transform.position = originalPosition;
        }
        rotateTime = 0;
    }

    void RotatePassenge()
    {
        draggedPassenge.transform.Rotate(new Vector3(0, 0, 90));
        var p = draggedPassenge.GetComponent<Passenge>();
        p.Rotate();
        rotateTime++;
        //draggedPassenge.transform.position = CurrentTouchPosition;
    }
}
