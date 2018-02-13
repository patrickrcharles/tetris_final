using UnityEngine;
using UnityEngine.UI;

using Windows.Kinect;
using System.Linq;

public class KinectManager : MonoBehaviour
{
    private KinectSensor _sensor;
    private BodyFrameReader _bodyFrameReader;
    private Body[] _bodies = null;

    public GameObject kinectAvailableText;
    public Text handXTextL, handXTextR;
    public Text handYTextL, handYTextR;

    public bool IsAvailable;
    public float PaddlePosition;
    public bool IsFire;
    //public float leftHand, rightHand;

    //joint for control
    public Windows.Kinect.Joint leftHand, rightHand;

    // intial position of l/r hands
    public float leftHandInitialPosX, rightHandInitialPosX;
    public float leftHandInitialPosY, rightHandInitialPosY;

    //current pos of l/r hands
    public float leftHandPosX, rightHandPosX;
    public float leftHandPosY, rightHandPosY;

    public static KinectManager instance = null;

    public Body[] GetBodies()
    {
        return _bodies;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {

        _sensor = KinectSensor.GetDefault();

        if (_sensor != null)
        {
            IsAvailable = _sensor.IsAvailable;

            kinectAvailableText.SetActive(IsAvailable);

            _bodyFrameReader = _sensor.BodyFrameSource.OpenReader();

            if (!_sensor.IsOpen)
            {
                _sensor.Open();
            }

            _bodies = new Body[_sensor.BodyFrameSource.BodyCount];

            foreach (var body in _bodies.Where(b => b.IsTracked))
            {

                //this WORKS
                //*********************************************
                //get initial positions
                leftHand = body.Joints[JointType.HandLeft];
                leftHandInitialPosX = leftHand.Position.X;
                leftHandInitialPosY = leftHand.Position.Y;

                rightHand = body.Joints[JointType.HandRight];
                rightHandInitialPosX = rightHand.Position.X;
                rightHandInitialPosY = rightHand.Position.Y;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        IsAvailable = _sensor.IsAvailable;

        if (_bodyFrameReader != null)
        {
            var frame = _bodyFrameReader.AcquireLatestFrame();

            if (frame != null)
            {
                frame.GetAndRefreshBodyData(_bodies);

                foreach (var body in _bodies.Where(b => b.IsTracked))
                {
                    IsAvailable = true;

                        //this WORKS
                        //*********************************************
                        //get left hand current
                        leftHand = body.Joints[JointType.HandLeft];
                        leftHandPosX = leftHand.Position.X;
                        leftHandPosY = leftHand.Position.Y;

                        //get right hand current
                        rightHand = body.Joints[JointType.HandRight];
                        rightHandPosX = rightHand.Position.X;
                        rightHandPosY = rightHand.Position.Y;

                    //***********************************************

                    //PaddlePosition = joint.Position.X;
                    // PaddlePosition = RescalingToRangesB(-1, 1, -8, 8, body.Lean.X);
                    //adlePosition = JointType.HandLeft;
                    handXTextL.text = (leftHandPosX*10).ToString();
                    handXTextR.text = (rightHandPosX*10).ToString();
                    handYTextL.text = (leftHandPosY*10).ToString();
                    handYTextR.text = (rightHandPosY*10).ToString();
                }

                frame.Dispose();
                frame = null;
            }
        }
    }

    static float RescalingToRangesB(float scaleAStart, float scaleAEnd, float scaleBStart, float scaleBEnd, float valueA)
    {
        return (((valueA - scaleAStart) * (scaleBEnd - scaleBStart)) / (scaleAEnd - scaleAStart)) + scaleBStart;
    }

    void OnApplicationQuit()
    {
        if (_bodyFrameReader != null)
        {
            _bodyFrameReader.IsPaused = true;
            _bodyFrameReader.Dispose();
            _bodyFrameReader = null;
        }

        if (_sensor != null)
        {
            if (_sensor.IsOpen)
            {
                _sensor.Close();
            }

            _sensor = null;
        }
    }
}





