using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DisableTrackedVisuals : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Disables spawned feature points and the ARPointCloudManager")]
    bool m_DisableFeaturePoints;

    public bool disableFeaturePoints
    {
        get => m_DisableFeaturePoints;
        set => m_DisableFeaturePoints = value;
    }

    [SerializeField]
    [Tooltip("Disables spawned planes and ARPlaneManager")]
    bool m_DisablePlaneRendering;

    public bool disablePlaneRendering
    {
        get => m_DisablePlaneRendering;
        set => m_DisablePlaneRendering = value;
    }

    [SerializeField]
    ARPointCloudManager m_PointCloudManager;

    public ARPointCloudManager pointCloudManager
    {
        get => m_PointCloudManager;
        set => m_PointCloudManager = value;
    }
    
    [SerializeField]
    ARPlaneManager m_PlaneManager;

    public ARPlaneManager planeManager
    {
        get => m_PlaneManager;
        set => m_PlaneManager = value;
    }
    

    public void OnPlacedObject(bool change)
    {
        if (m_DisableFeaturePoints)
        {
            m_PointCloudManager.SetTrackablesActive(change);
            m_PointCloudManager.enabled = change;
        }

        if (m_DisablePlaneRendering)
        {
            m_PlaneManager.SetTrackablesActive(change);
            m_PlaneManager.enabled = change;
        }
    }

    
}
