using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    private ManoGestureContinuous grab;
    private ManoGestureContinuous pinch;
    private ManoGestureContinuous point;
    private ManoGestureTrigger click;
    private HandSide side;
    

        [SerializeField]
    private Material[] objectMaterial;
    private string handTag = "Player";
    private Renderer cubeRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

        private void Initialize()
    {
        HandSide side = HandSide.Backside;
        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;
        point = ManoGestureContinuous.POINTER_GESTURE;
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.sharedMaterial = objectMaterial[0];
        cubeRenderer.material = objectMaterial[0];
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="other">The collider that stays</param>
    private void OnTriggerStay(Collider other)
    {
        MoveWhenGrab(other);
        RotateWhenHolding(other);
        // SpawnWhenClicking(other);
    }

  /// <summary>
    /// If grab is performed while hand collider is in the cube.
    /// The cube will follow the hand.
    /// </summary>
    private void MoveWhenGrab(Collider other)
    {
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
        {
            transform.parent = other.gameObject.transform;
        }
        else if(ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.hand_side == side){
            // transform.localScale += new Vector3(0.001f,0.001f,0.001f);
            transform.parent = null;
        }

        else
        {
            transform.parent = null;
        }
    }

    /// <summary>
    /// If pinch is performed while hand collider is in the cube.
    /// The cube will start rotate.
    /// </summary>
    private void RotateWhenHolding(Collider other)
    {
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 30, Space.World);
        }
        else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == point)
        {
            transform.Rotate(Vector3.left * Time.deltaTime * 30, Space.World);
        }
    }


    /// <summary>
    /// Vibrate when hand collider enters the cube.
    /// </summary>
    /// <param name="other">The collider that enters</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == handTag)
        {
            cubeRenderer.sharedMaterial = objectMaterial[1];
            // FindObjectOfType<AudioManager>().Play("Swell");

        }
    }

    /// <summary>
    /// Change material when exit the cube
    /// </summary>
    /// <param name="other">The collider that exits</param>
    private void OnTriggerExit(Collider other)
    {
        cubeRenderer.sharedMaterial = objectMaterial[0];
    }
}
