using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField, Tooltip("The max speed of the player")]
    private float speed = 4f;

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
        inputVector *= speed;
        SetAnimationProperties(inputVector);
    }

    private void SetAnimationProperties(Vector3 inputVector)
    {
        anim.SetFloat(Horizontal, inputVector.x);
        anim.SetFloat(Vertical, inputVector.z);
    }
}
