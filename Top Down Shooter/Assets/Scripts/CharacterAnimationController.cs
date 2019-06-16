using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField, Tooltip("The max speed of the player (in meters per second")]
    private float moveSpeed = 4f;

    [SerializeField, Tooltip("The rotations speed of the player (in degrees per second)")]
    private float rotationSpeed = 90f;

    private Animator anim;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void SetSpeed(Vector3 inputVector)
    {
        inputVector = transform.InverseTransformDirection(inputVector);
        inputVector *= moveSpeed;
        SetAnimationProperties(inputVector);
    }

    private void SetAnimationProperties(Vector3 inputVector)
    {
        anim.SetFloat(Horizontal, inputVector.x);
        anim.SetFloat(Vertical, inputVector.z);
    }

    public void RotateTowardsPoint(Vector3 targetPoint)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}