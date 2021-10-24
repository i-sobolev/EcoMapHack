using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OtherUsersGeoSmiles : MonoBehaviour
{
    [SerializeField] private GeoSmilesConfig _geoSmilesConfig;
    [SerializeField] private GeoSmileAPIHelper _apiHelper;
    [SerializeField] private UserGeoSmile _userGeoSmileTemplate;

    private List<GeoSmileModel> _usersGeoSmiles = new List<GeoSmileModel>();

    private void OnEnable()
    {
        _apiHelper.DataReceived += OnDataReceived;
    }

    public void PostUserGeoSmile(GeoSmile geoSmile, Vector3 position)
    {
        var latlong = Vector3ToLatLong(position);

        var newGeoSmile = new GeoSmileModel()
        {
            GeosmileId = geoSmile.Id,
            Latitude = latlong.x,
            Longitude = latlong.y
        };

        _apiHelper.Post(newGeoSmile).Start(_apiHelper);
    }

    private void OnDataReceived(List<GeoSmileModel> receivedData)
    {
        _usersGeoSmiles = receivedData;

        var worldOrigin = LatLongToVector3(UserLocation.UserCoordinates.Latitude, UserLocation.UserCoordinates.Logitude);

        _usersGeoSmiles.ForEach(otherUserSmile =>
        {
            var geoSmileCoords = LatLongToVector3(otherUserSmile.Latitude, otherUserSmile.Longitude);

            var newSmile = Instantiate(
                original: _userGeoSmileTemplate,
                position: geoSmileCoords - worldOrigin,
                rotation: Quaternion.identity);

            newSmile.SetSprite(_geoSmilesConfig.GeoSmiles.ToList().First(smile => smile.Id == otherUserSmile.GeosmileId));
        });
    }

    private Vector3 LatLongToVector3(double latitude, double longitude)
    {
        float DegreesLatitudeInMeters = 111132;
        float DegreesLongitudeInMetersAtEquator = 111319.9f;

        return new Vector3()
        {
            x = (float)latitude * DegreesLatitudeInMeters,
            z = (float)longitude * DegreesLongitudeInMetersAtEquator * Mathf.Cos((float)latitude * (Mathf.PI / 180))
        };
    }

    private Vector3 Vector3ToLatLong(Vector3 vector)
    {
        FindMetersPerLat((float)UserLocation.UserCoordinates.Latitude);

        var geoLocation = new Vector2();

        geoLocation.x = ((float)UserLocation.UserCoordinates.Latitude + (vector.z) / _metersPerLat);
        geoLocation.y = ((float)UserLocation.UserCoordinates.Latitude + (vector.x) / _metersPerLon);

        return geoLocation;
    }

    private float _metersPerLat;
    private float _metersPerLon;

    private void FindMetersPerLat(float lat)
    {
        float m1 = 111132.92f;
        float m2 = -559.82f;
        float m3 = 1.175f;
        float m4 = -0.0023f;
        float p1 = 111412.84f;
        float p2 = -93.5f;
        float p3 = 0.118f;

        lat = lat * Mathf.Deg2Rad;

        _metersPerLat = m1 + (m2 * Mathf.Cos(2 * (float)lat)) + (m3 * Mathf.Cos(4 * (float)lat)) + (m4 * Mathf.Cos(6 * (float)lat));
        _metersPerLon = (p1 * Mathf.Cos((float)lat)) + (p2 * Mathf.Cos(3 * (float)lat)) + (p3 * Mathf.Cos(5 * (float)lat));
    }
}