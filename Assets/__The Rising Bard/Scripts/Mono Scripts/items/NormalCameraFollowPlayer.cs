using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform followTarget;

    private Vector3 tempPostion;

    private void LateUpdate()
    {
        tempPostion = transform.position;
        tempPostion.x = followTarget.position.x;
        tempPostion.y = followTarget.position.y;
        transform.position = tempPostion;

        transform.LookAt(followTarget);
    }

}
