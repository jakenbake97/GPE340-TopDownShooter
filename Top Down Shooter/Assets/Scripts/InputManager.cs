using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField, Tooltip("A light to be placed on the cursor to make it easier to see")]
    private LightFollowsMouse mousePointLight;

    private CharacterAnimationController characterAnimations;
    private bool _isMainNotNull;
    private Plane plane;

    // Start is called before the first frame update
    private void Awake()
    {
        _isMainNotNull = Camera.main != null;
        plane = new Plane(Vector3.up, transform.position);
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

    private void MouseInputPlaneCast()
    {
        if (!_isMainNotNull) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out var distance))
        {
            var point = ray.GetPoint(distance);
            characterAnimations.RotateTowardsPoint(point);
            mousePointLight.LightMovesToPoint(point);
        }
    }

    private void AxisInput()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);
        characterAnimations.SetSpeed(input);
    }
}