using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlaneRaycaster : MonoBehaviour
{
    public static Action<Vector3> PlaneRaycasted;
    public static Action PlaneNotRaycasted;

    private ARRaycastManager _raycastManager;

    private void Awake() => _raycastManager = GetComponent<ARRaycastManager>();

    private void Update()
    {
        var hitResults = new List<ARRaycastHit>();

        _raycastManager.Raycast(
            screenPoint: Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f)), 
            hitResults: hitResults, 
            trackableTypes: UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hitResults.Count > 0)
        {
            var position = hitResults[0].pose.position;
            PlaneRaycasted?.Invoke(position);
        }

        else
        {
            PlaneNotRaycasted?.Invoke();
        }
    }
}