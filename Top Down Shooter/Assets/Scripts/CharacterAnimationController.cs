using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField, Tooltip("The max speed of the player (in meters per second")]
    private float moveSpeed = 4f;

    [SerializeField, Tooltip("The rotations speed of the player (in degrees per second)")]
    private float rotationSpeed = 90f;

    private Animator anim;
    
    // int variables that hold a converted animator look up to increase performance, per the suggestion of Rider
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void SetSpeed(Vector3 inputVector) //takes input from input manager, converts it to world space,
                                              //gives it a magnitude equal to the max move speed and then passes
                                              //the input to the setAnimationProperties method
    {
        inputVector = transform.InverseTransformDirection(inputVector);
        inputVector *= moveSpeed;
        SetAnimationProperties(inputVector);
    }

    private void SetAnimationProperties(Vector3 inputVector) //sets the float parameters of the animator using the
                                                             //input vector and a hash ID representing the
                                                             //variable string name
    {
        anim.SetFloat(Horizontal, inputVector.x);
        anim.SetFloat(Vertical, inputVector.z);
    }

    public void RotateTowardsPoint(Vector3 targetPoint) //Takes a target point from the mouse position of the
                                                        //input manager and creates a quaternion for the rotation
                                                        //needed between the target point and current position,
                                                        //then rotates the game object towards the target by
                                                        //rotationSpeed degrees per second
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}