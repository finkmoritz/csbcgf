using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DragDropTransform : MonoBehaviourPun
{
    public bool isDraggable = false;
    public bool isHoverable = false;

    private static PlayMaker playMaker;

    private Vector3 originalPosition;
    private bool dragging = false;
    private bool hovering = false;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine && isDraggable && dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            rayPoint.y = Mathf.Max(0.75f, rayPoint.y);
            transform.position = rayPoint;
        }
    }

    void OnMouseDown()
    {
        if (photonView.IsMine && isDraggable)
        {
            if (!hovering)
            {
                originalPosition = transform.position;
            }
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
        }
    }

    void OnMouseUp()
    {
        if (photonView.IsMine && isDraggable)
        {
            transform.position = originalPosition;
            dragging = false;
            CheckPlay();
        }
    }

    private void OnMouseEnter()
    {
        if (photonView.IsMine && isHoverable && !dragging)
        {
            originalPosition = transform.position;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            transform.position -= 0.25f * ray.direction.normalized;
            hovering = true;
        }
    }
    
    private void OnMouseExit()
    {
        if (photonView.IsMine && isHoverable && !dragging)
        {
            transform.position = originalPosition;
            hovering = false;
        }
    }

    [PunRPC]
    public void SetDraggable(bool isDraggable)
    {
        this.isDraggable = isDraggable;
    }

    [PunRPC]
    public void SetHoverable(bool isHoverable)
    {
        this.isHoverable = isHoverable;
    }

    private void CheckPlay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int slotsLayerMask = 1 << 9; //bit shift to retrieve mask for layer #9
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, slotsLayerMask) && hit.collider != null)
        {
            Slot slot = hit.collider.gameObject.GetComponent<Slot>();
            if (slot != null)
            {
                int cardUid = gameObject.GetComponent<Card3D>().uid;
                GetPlayMaker().PlayMonsterCard(cardUid, slot.slotIndex);
            }
        }
    }

    public PlayMaker GetPlayMaker()
    {
        if (playMaker == null)
        {
            playMaker = GameObject.Find("PlayMaker").GetComponent<PlayMaker>();
        }
        return playMaker;
    }
}
