using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public bool shoodMove, shoodRotate;
    public float moveSpeed, rotateSpeed;


    void Update()
    {
        if (shoodMove)
        {
            transform.position += new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime;
        }

        if (shoodRotate)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, rotateSpeed * Time.deltaTime, 0f));
        }
    }







}
