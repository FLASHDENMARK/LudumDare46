using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0F;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public float lookSensitivity;

    public Camera playerCamera;
    public InventoryManager inventoryManager;
    public InventoryMovement inventoryMovement;

    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;

    private void OnEnable()
    {

    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }


    private void OnDisable()
    {
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        Vector2 look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        bool shoot = Input.GetMouseButton(0);
        bool zoom = Input.GetMouseButton(1);
        bool inspect = Input.GetKeyDown(KeyCode.E);
        bool watch = Input.GetKey(KeyCode.Q);

        // TODO May need to be expanded if we need more weapons
        bool weapon = Input.GetKeyDown(KeyCode.Alpha1);

        Move(move);

        Look(look);

        ToggleWatch(watch);

        ShootWeapon(shoot);
        
        ZoomWeapon(zoom);

        if (inspect)
        {
            InspectObject();
        }

        if (weapon)
        {
            ToggleWeapon();
        }
    }

    private void ZoomWeapon(bool zoom)
    {
        inventoryManager.ZoomWeapon(zoom);
    }

    private void ShootWeapon(bool shoot)
    {
        inventoryManager.ShootWeapon(shoot);
    }

    private void Move (Vector2 vector)
    {
        if (_characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            _moveDirection = new Vector3(vector.y, 0.0f, vector.x);

            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= moveSpeed;

        }
        
        inventoryMovement.Move(vector);

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        _moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void Look (Vector2 vector)
    {
        inventoryMovement.Look(vector);

        float rotationX = transform.localEulerAngles.y + vector.x * lookSensitivity;
        float rotationY = playerCamera.transform.localEulerAngles.x - vector.y * lookSensitivity;

        // TODO Clamp rotationY

        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        playerCamera.transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    }

    private void InspectObject ()
    {
        // TODO Røgen
    }

    private void ToggleWatch (bool isActive)
    {
        inventoryManager.ToggleWatch(isActive);
    }

    private void ToggleWeapon ()
    {
        inventoryManager.ToggleWeapon();
    }
}
