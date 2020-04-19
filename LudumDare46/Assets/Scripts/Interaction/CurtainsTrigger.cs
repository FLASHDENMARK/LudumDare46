using UnityEngine;

public class CurtainsTrigger : TriggerBase
{
    public float openCloseSpeed = 5.0F;

    public Transform curtain;
    public Transform onPosition;
    public Transform OffPosition;

    public float buttonSpeed;

    public Transform onButton;
    public Transform onButtonOnPosition;
    public Transform onButtonOffPosition;

    public Transform offButton;
    public Transform offButtonOffPosition;
    public Transform offButtonOnPosition;

    private Transform _currentPosition;

    public override void Off ()
    {
        _currentPosition = OffPosition;
    }

    public override void On ()
    {
        _currentPosition = onPosition;
    }

    private void Update()
    {
        curtain.position = Vector3.MoveTowards(curtain.position, _currentPosition.position, openCloseSpeed * Time.deltaTime);

        if (on)
        {
            onButton.position = Vector3.MoveTowards(onButton.position, onButtonOffPosition.position, buttonSpeed * Time.deltaTime);
            offButton.position = Vector3.MoveTowards(offButton.position, offButtonOnPosition.position, buttonSpeed * Time.deltaTime);
        }
        else
        {
            onButton.position = Vector3.MoveTowards(onButton.position, onButtonOnPosition.position, buttonSpeed * Time.deltaTime);
            offButton.position = Vector3.MoveTowards(offButton.position, offButtonOffPosition.position, buttonSpeed * Time.deltaTime);
        }
    }
}
