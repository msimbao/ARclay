using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Moves the ARSessionOrigin in such a way that it makes the given content appear to be
    /// at a given location acquired via a raycast.
    /// </summary>
    [RequireComponent(typeof(ARSessionOrigin))]
    [RequireComponent(typeof(ARRaycastManager))]
    public class MakeAppearOnPlane : MonoBehaviour
    {
        public bool placed;
        public DisableTrackedVisuals disable;
        [SerializeField]
        [Tooltip("A transform which should be made to appear to be at the touch point.")]
        Transform m_Content;

        /// <summary>
        /// A transform which should be made to appear to be at the touch point.
        /// </summary>
        public Transform content
        {
            get { return m_Content; }
            set { m_Content = value; }
        }

        [SerializeField]
        [Tooltip("The rotation the content should appear to have.")]
        Quaternion m_Rotation;

        /// <summary>
        /// The rotation the content should appear to have.
        /// </summary>
        public Quaternion rotation
        {
            get { return m_Rotation; }
            set
            {
                m_Rotation = value;
                if (m_SessionOrigin != null)
                    m_SessionOrigin.MakeContentAppearAt(content, content.transform.position, m_Rotation);
            }
        }

        void Awake()
        {
            m_SessionOrigin = GetComponent<ARSessionOrigin>();
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

        void Update()
        {
            if (Input.touchCount == 0 || m_Content == null)
                return;

            var touch = Input.GetTouch(0);

            if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon) && !placed)
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                // This does not move the content; instead, it moves and orients the ARSessionOrigin
                // such that the content appears to be at the raycast hit position.
                hitPose.position.y += 30;
                m_SessionOrigin.MakeContentAppearAt(content, hitPose.position, m_Rotation);
                disable.OnPlacedObject(false);
                placed = true;
            }

        }

        public void changePlaced(bool state){
            placed = state;
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARSessionOrigin m_SessionOrigin;

        ARRaycastManager m_RaycastManager;
    }
}