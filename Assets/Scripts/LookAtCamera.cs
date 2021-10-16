using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//DNCST - header,comments, line length
public class LookAtCamera : MonoBehaviour
{
    [System.Serializable]
    public class RotationAxis
    {
        public bool x;
        public bool y = true;
        public bool z;
    }
    [SerializeField] Vector3 offset;

    [SerializeField] RotationAxis FollowAxis;
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);


        if (!FollowAxis.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
        }
        if (!FollowAxis.y)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, 0f, transform.rotation.eulerAngles.z));
        }
        if (!FollowAxis.z)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f));
        }
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x + offset.x, transform.rotation.eulerAngles.y + offset.y, transform.rotation.eulerAngles.z + offset.z));
    }
}
