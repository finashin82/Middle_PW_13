using Fusion;
using UnityEngine;

public class MoveBullet : NetworkBehaviour
{
    public Vector3 Direction { get; set; }

    [SerializeField] private float speedBullet = 5f;
    public override void FixedUpdateNetwork()
    {
        transform.position += Direction * speedBullet * Runner.DeltaTime;

        //transform.Translate(Direction * speedBullet * Time.deltaTime, Space.World);
    }
}
