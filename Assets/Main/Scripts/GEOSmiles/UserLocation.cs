using System.Collections;
using UnityEngine;

public class UserLocation : MonoBehaviour
{
    public static Coordinates UserCoordinates;

    private void Start()
    {
        InitializeLocation().Start(this);
    }

    private IEnumerator InitializeLocation()
    {
        Input.location.Start();

        yield return null;

        while (true)
        {
            if (Input.location.status == LocationServiceStatus.Running)
            {
                UserCoordinates.Latitude = Input.location.lastData.latitude;
                UserCoordinates.Latitude = Input.location.lastData.longitude;
            }

            yield return null;
        }

        yield return null;
    }

    public struct Coordinates
    {
        public double Latitude;
        public double Logitude;
    }
}
