using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideDeform : MonoBehaviour
{
    public TerrainEditor myEditor;
    public World myWorld;
    public Transform camera;
    public bool editTerrain;
    public bool mirror;
    public bool clayMode;
    // Start is called before the first frame update
    
    private ManoGestureContinuous grab;
    private ManoGestureContinuous pinch;
    private ManoGestureContinuous point;
    private ManoGestureTrigger click;

    [SerializeField]
    private Material[] arSphereMaterial;
    private Renderer renderer;

    void Start()
    {
        myEditor.world = myWorld;
        // myEditor.playerCamera = camera;

        renderer = GetComponent<Renderer>();
        renderer.material = arSphereMaterial[0];

        Initialize();
    }


        private void Initialize()
    {
        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        point = ManoGestureContinuous.POINTER_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;

    }
    // Update is called once per frame
    void Update()
    {
        renderer.sharedMaterial = arSphereMaterial[0];
        Vector3 myMirror = transform.position;
        myMirror.x = -transform.position.x;

        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == point)
            {
            editTerrain = false;
            // FindObjectOfType<AudioManager>().Play("Swell");
            renderer.sharedMaterial = arSphereMaterial[1];
            
            if (clayMode) {
            myEditor.EditTerrain(transform.position, editTerrain, myEditor.force, myEditor.range);
            if (mirror) myEditor.EditTerrain(myMirror, editTerrain, myEditor.force, myEditor.range);
            }

            }
        else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
            {
            editTerrain = true;
            // FindObjectOfType<AudioManager>().Play("Swell");
            renderer.sharedMaterial = arSphereMaterial[2];

            if (clayMode) {
            myEditor.EditTerrain(transform.position, editTerrain, myEditor.force, myEditor.range);
            if (mirror) myEditor.EditTerrain(myMirror, editTerrain, myEditor.force, myEditor.range);
            }

            } 
    }



}
