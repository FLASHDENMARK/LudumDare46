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
        bool inspect = Input.GetKeyDown(KeyCode.E);
        bool watch = Input.GetKeyDown(KeyCode.Q);

        Move(move);

        Look(look);

        if (inspect)
        {
            InspectObject();
        }

        if (watch)
        {
            ToggleWatch();
        }
    }

    private void Move (Vector2 vector)
    {
        if (_characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            _moveDirection = new Vector3(vector.y, 0.0f, vector.x);

            _moveDirection = playerCamera.transform.TransformDirection(_moveDirection);
            _moveDirection *= moveSpeed;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        _moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void Look (Vector2 vector)
    {
        float rotationX = transform.localEulerAngles.y + vector.x * lookSensitivity;
        float rotationY = playerCamera.transform.localEulerAngles.x - vector.y * lookSensitivity;

        //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        playerCamera.transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    }

    private void InspectObject ()
    {

    }

    private void ToggleWatch ()
    {

    }
}
