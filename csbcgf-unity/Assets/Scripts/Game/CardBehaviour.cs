using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    public Vector3? targetPosition;
    public Quaternion? targetRotation;

    public const float Speed = 0.05f;
    public const float RotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPosition != null)
        {
            Vector3 diff = (Vector3)(targetPosition - transform.position);
            if(diff.magnitude < Speed)
            {
                transform.position = (Vector3)targetPosition;
                targetPosition = null;
            } else
            {
                transform.position += Speed * diff.normalized;
            }
        }

        if(targetRotation != null)
        {
            float diff = Quaternion.Angle((Quaternion)targetRotation, transform.rotation);
            if(diff < RotationSpeed)
            {
                transform.rotation = (Quaternion)targetRotation;
                targetRotation = null;
            } else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, (Quaternion)targetRotation, RotationSpeed);
            }
        }
    }
}
