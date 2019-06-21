using UnityEngine;

[RequireComponent(typeof(Health), typeof(Animator))]
public class CharacterAnimationController : MonoBehaviour
{
    [Header("Movement Settings")] [SerializeField, Tooltip("The max speed of the player (in meters per second")]
    private float moveSpeed = 4f;

    [SerializeField, Tooltip("The rotations speed of the player (in degrees per second)")]
    private float rotationSpeed = 90f;

    private Animator anim;
    
    public Health Health { get; private set; }

    // int variables that hold a converted animator look up to increase performance, per the suggestion of Rider
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Health = GetComponent<Health>();
    }


    ///<summary>takes input from input manager, converts it to local space, gives it a magnitude equal to the
    /// max move speed and then passes the input to the setAnimationProperties method </summary>
    public void SetSpeed(Vector3 inputVector)
    {
        inputVector = transform.InverseTransformDirection(inputVector);
        inputVector *= moveSpeed;
        SetAnimationProperties(inputVector);
    }

    ///<summary>sets the float parameters of the animator using the input vector and a hash ID representing the
    ///variable string name</summary>
    private void SetAnimationProperties(Vector3 inputVector)
    {
        anim.SetFloat(Horizontal, inputVector.x);
        anim.SetFloat(Vertical, inputVector.z);
    }

    ///<summary>Takes a target point from the mouse position of the input manager and creates a quaternion for
    ///the rotation needed between the target point and current position, then rotates the game object towards
    /// the target by rotationSpeed degrees per second </summary>
    public void RotateTowardsPoint(Vector3 targetPoint)
    {
        var objectTransform = transform;
        var position = objectTransform.position;
        targetPoint = new Vector3(targetPoint.x, position.y, targetPoint.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - position);
        objectTransform.rotation =
            Quaternion.RotateTowards(objectTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}