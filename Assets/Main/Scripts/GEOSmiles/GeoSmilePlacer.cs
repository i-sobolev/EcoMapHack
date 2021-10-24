using System;
using UnityEngine;
using DG.Tweening;

public class GeoSmilePlacer : MonoBehaviour
{
    public static Action GeoSmilePlaced;

    [SerializeField] private UserGeoSmile _geoSmile;
    [SerializeField] private OtherUsersGeoSmiles _otherUsersGeoSmiles;

    private void OnEnable()
    {
        UserGeoSmileSeletor.Selected += (geoSmile) => _geoSmile.SetSprite(geoSmile);

        ARPlaneRaycaster.PlaneRaycasted += OnPlaneRaycasted;
        ARPlaneRaycaster.PlaneNotRaycasted += OnPlaneNotRaycasted;
    }

    private void OnDisable()
    {
        ARPlaneRaycaster.PlaneRaycasted -= OnPlaneRaycasted;
        ARPlaneRaycaster.PlaneNotRaycasted -= OnPlaneNotRaycasted;
    }

    public void PlaceGeoSmile()
    {
        enabled = false;

        _geoSmile.PlaySpawnAnimation();

        _otherUsersGeoSmiles.PostUserGeoSmile(_geoSmile.LinkedGeoSmile, _geoSmile.transform.position);
        GeoSmilePlaced?.Invoke();
    }

    private void OnPlaneNotRaycasted()
    {
        _geoSmile.Fade(0.65f);
    }

    private void OnPlaneRaycasted(Vector3 position)
    {
        _geoSmile.transform.position = position;
        _geoSmile.Fade(1);
    }
}