using UnityEngine;

public class CurtainsTrigger : TriggerBase
{
    public float openCloseSpeed = 5.0F;

    public Transform curtain;
    public Transform onPosition;
    public Transform OffPosition;

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
    }
}
