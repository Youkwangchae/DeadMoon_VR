using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwingingArmMotion : MonoBehaviour
{
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject CenterEyeCamera;
    public GameObject ForwardDirection;

    //public Text rotationText;

    private Vector3 PositionPreviousFrameLeftHand;
    private Vector3 PositionPreviousFrameRightHand;
    private Vector3 PlayerPositionPreviousFrame;
    private Vector3 PlayerPositionThisFrame;
    private Vector3 PositionThisFrameLeftHand;
    private Vector3 PositionThisFrameRightHand;

    private Vector3 RotationPreviousFrameLeftHand;
    private Vector3 RotationPreviousFrameRightHand;
    private Vector3 RotationThisFrameLeftHand;
    private Vector3 RotationThisFrameRightHand;

    public float speed = 70;
    private float HandSpeed;

    private void Start()
    {
        PlayerPositionPreviousFrame = transform.localPosition;
        PositionPreviousFrameLeftHand = LeftHand.transform.localPosition;
        PositionPreviousFrameRightHand = RightHand.transform.localPosition;

        RotationPreviousFrameLeftHand = LeftHand.transform.localEulerAngles;
        RotationPreviousFrameRightHand = RightHand.transform.localEulerAngles;
    }

    private void FixedUpdate()
    {
        float yRotation = CenterEyeCamera.transform.eulerAngles.y;
        ForwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);        

        // 현재 왼손-오른손 컨트롤러의 위치
        PositionThisFrameLeftHand = LeftHand.transform.localPosition;
        PositionThisFrameRightHand = RightHand.transform.localPosition;

        PlayerPositionThisFrame = transform.localPosition;

        RotationThisFrameLeftHand = LeftHand.transform.localEulerAngles;
        RotationThisFrameRightHand = RightHand.transform.localEulerAngles;

        // 이전 왼손-오른손 컨트롤러의 위치와의 차이값 가져오기
        var playerDistanceMoved = Vector3.Distance(PlayerPositionThisFrame, PlayerPositionPreviousFrame);
        var leftHandDistanceMoved = Vector3.Distance(PositionPreviousFrameLeftHand, PositionThisFrameLeftHand);
        var rightHandDistanceMoved = Vector3.Distance(PositionPreviousFrameRightHand, PositionThisFrameRightHand);

        var leftHandRotationMoved = Vector3.Angle(RotationPreviousFrameLeftHand, RotationThisFrameLeftHand);
        var rightHandRotationMoved = Vector3.Angle(RotationPreviousFrameRightHand, RotationThisFrameRightHand);

        // HandSpeed = ((leftHandDistanceMoved - playerDistanceMoved) + (rightHandDistanceMoved - playerDistanceMoved));
        HandSpeed = ((leftHandDistanceMoved  + rightHandDistanceMoved ) + (leftHandRotationMoved + rightHandRotationMoved));

        //rotationText.text = HandSpeed.ToString();

        if (Time.timeSinceLevelLoad > 1f)
            transform.position += ForwardDirection.transform.forward.normalized * HandSpeed * speed;

        // transform.position += ForwardDirection.transform.forward * HandSpeed * speed * Time.deltaTime;

        PositionPreviousFrameLeftHand = PositionThisFrameLeftHand;
        PositionPreviousFrameRightHand = PositionThisFrameRightHand;

        PlayerPositionPreviousFrame = PlayerPositionThisFrame;
    }
}
