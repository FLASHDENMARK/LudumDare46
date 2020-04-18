using UnityEngine;

public class InventoryMovement : MonoBehaviour
{
    // This is all shit :(




    public float bobSpeed = 2.0F;

    public float breathHeight = 1.0F;
    public float moveAmount = 0.5F;

    private Vector3 _breath;
    private Vector3 _movement;

    private float _breathAmount;
    private Vector3 _moveAmount;

    Vector3 _initialPosition = Vector3.zero;
    Vector3 _initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localEulerAngles;
    }

    private Vector3 final;

    public void Update ()
    {
        Breath();

        _breath = new Vector3(0, GetCurveValue(_breathAmount) * breathHeight, 0);
        _movement = Vector3.Lerp(_movement, new Vector3(-_moveAmount.y, 0, -_moveAmount.x) * moveAmount, 1);

        final = _breath + _movement;

        Bob(final);
    }

    private void Breath ()
    {
        _breathAmount += Time.deltaTime * bobSpeed;
    }

    public void Move (Vector2 vector)
    {
        _moveAmount = vector;
    }

    // TODO Fix look
    public void Look (Vector2 vector)
    {
        Vector2 rotation = vector;

        Quaternion newRot = Quaternion.Euler(_initialRotation.x + rotation.x, _initialRotation.y + rotation.y, 0);

        //transform.rotation = newRot;
    }

    private void Bob (Vector3 bob)
    {
        transform.localPosition = _initialPosition + bob;
    }

    private float GetCurveValue (float value)
    {
        float cos = Mathf.Cos(value * Mathf.Deg2Rad);

        return cos;
    }
}
