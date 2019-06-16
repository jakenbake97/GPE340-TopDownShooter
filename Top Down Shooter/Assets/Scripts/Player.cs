using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Tooltip("The max speed of the player")]
    private float speed = 4f;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);
        input = transform.InverseTransformDirection(input);
        input *= speed;
        anim.SetFloat("Horizontal", input.x);
        anim.SetFloat("Vertical", input.z);
    }
}
