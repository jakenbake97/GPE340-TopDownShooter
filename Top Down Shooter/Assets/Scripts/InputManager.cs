using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
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
            characterAnimations.RotateTowardsPoint(ray.GetPoint(distance));
        }
    }

    private void AxisInput()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);
        characterAnimations.SetSpeed(input);
    }
}