using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomThirdPersonController : MonoBehaviour
{
    public FixedJoystick LeftJoystick;
    public FixedButton JumpButton;
    public FixedTouchField TouchField;

    protected Rigidbody Rigidbody;
    private PhotonView PV;

    protected float CameraAngleY;
    protected float CameraAngleSpeed = 0.1f;
    protected float CameraPosY;
    protected float CameraPosSpeed = 0.1f;



    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            var input = new Vector3(LeftJoystick.input.x, 0, LeftJoystick.input.y);
            var vel = Quaternion.AngleAxis(CameraAngleY + 180, Vector3.up) * input * 5f;

            Rigidbody.velocity = new Vector3(vel.x, Rigidbody.velocity.y, vel.z);
            transform.rotation = Quaternion.AngleAxis(CameraAngleY + 180 + Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.001f, Vector3.up), Vector3.up);

            CameraAngleY += TouchField.TouchDist.x * CameraAngleSpeed;

            Camera.main.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, 3, 4);
            Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);
        }
    }
}
