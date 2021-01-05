using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropTransform : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool dragging = false;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            rayPoint.y = Mathf.Max(0.75f, rayPoint.y);
            transform.position = rayPoint;
        }
    }

    void OnMouseDown()
    {
        originalPosition = transform.position;
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }

    void OnMouseUp()
    {
        transform.position = originalPosition;
        dragging = false;
    }
}
