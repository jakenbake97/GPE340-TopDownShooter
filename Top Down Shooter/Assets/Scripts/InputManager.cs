using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField, Tooltip("A light to be placed on the cursor to make it easier to see")]
    private LightFollowsMouse mousePointLight;

    private CharacterAnimationController characterAnimations;

    //boolean to store a check to see if main camera is not null
    private bool _isMainNotNull;
    private Plane plane;

    private void Awake()
    {
        _isMainNotNull = Camera.main != null; // checks to see if there is a main camera in the scene to prevent from
        // null reference exception, called in awake method to prevent
        // repeated null comparison
        plane = new Plane(Vector3.up, transform.position); //construct a new plane in the awake method to
        //increase performance as opposed to creating a new plane
        //each frame
    }

    private void Start()
    {
        characterAnimations = GetComponent<CharacterAnimationController>();
    }

    // Update is called once per frame
    private void Update()
    {
        AxisInput();
        MouseInputPlaneCast();
    }

    private void AxisInput() //gets horizontal and vertical axis input and clamps the magnitude to 1, then passes input
        //to characterAnimationController to move the character using root motion from
        //the blend tree
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);
        characterAnimations.SetSpeed(input);
    }

    private void MouseInputPlaneCast() //takes the mouse position and casts a ray from the screen point to the plane
        //created in the awake method, if the ray hits something then it gets that
        //point in world space and passes it to the CharacterAnimationController for
        //rotation and the LightFollowsMouse to move the light to that point
    {
        if (!_isMainNotNull) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!plane.Raycast(ray, out var distance)) return;
        var point = ray.GetPoint(distance);
        characterAnimations.RotateTowardsPoint(point);
        mousePointLight.LightMovesToPoint(point);
    }
}