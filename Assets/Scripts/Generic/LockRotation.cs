using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Vector3 startRotation;

    private void Start()
    {
        startRotation = transform.eulerAngles;
    }

    private void LateUpdate()
    {
        transform.eulerAngles = startRotation;
    }
}
