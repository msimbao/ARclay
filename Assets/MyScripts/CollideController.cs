
using UnityEngine.UI;
using UnityEngine;

public class CollideController : MonoBehaviour
{
    [SerializeField]
        [Tooltip("The slider used to control the scale factor.")]
        Slider m_Slider;
        

        /// <summary>
        /// The slider used to control the scale factor.
        /// </summary>
        public Slider slider
        {
            get { return m_Slider; }
            set { m_Slider = value; }
        }

        [SerializeField]
        [Tooltip("The text used to display the current scale factor on the screen.")]
        Text m_Text;

        /// <summary>
        /// The text used to display the current scale factor on the screen.
        /// </summary>
        public Text text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        [SerializeField]
        [Tooltip("Minimum scale factor.")]
        public float m_Min = .1f;

        /// <summary>
        /// Minimum scale factor.
        /// </summary>
        public float min
        {
            get { return m_Min; }
            set { m_Min = value; }
        }

        [SerializeField]
        [Tooltip("Maximum scale factor.")]
        public float m_Max = 10f;

        /// <summary>
        /// Maximum scale factor.
        /// </summary>
        public float max
        {
            get { return m_Max; }
            set { m_Max = value; }
        }

        /// <summary>
        /// Invoked whenever the slider's value changes
        /// </summary>
        public void OnSliderValueChanged()
        {
            if (slider != null)
                scale = slider.value * (max - min) + min;
        }

        float scale
        {
            get
            {
                return myEditor.range;
            }
            set
            {
                myEditor.range = 1 * value;
                UpdateText();
            }
        }


        void OnEnable()
        {
            if (slider != null)
                slider.value = (scale - min) / (max - min);
            UpdateText();
        }

        void UpdateText()
        {
            if (text != null)
                text.text = "CLAY SIZE: " + scale.ToString();;
        }

        public void setMirror (bool mirrorValue){
            myBrush.mirror = !myBrush.mirror;
            Debug.Log("Mirror Toggled");
        }
        public CollideDeform myBrush;
        public TerrainEditor myEditor;

}
