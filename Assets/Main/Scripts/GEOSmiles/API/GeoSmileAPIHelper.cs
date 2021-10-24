using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GeoSmileAPIHelper : MonoBehaviour
{
    private readonly string _apiUri = "http://weather.makievksy.ru.com/api/UserGeosmile";

    private void Start()
    {
        Get().Start(this);

        var testGeoSmile = new GeoSmileModel
        {
            GeosmileId = 1,
            Latitude = 666,
            Longitude = 666
        };

        Post(testGeoSmile).Start(this);
    }

    public IEnumerator Get()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(_apiUri);
        webRequest.SetRequestHeader("Content-type", "application/json");

        yield return webRequest.SendWebRequest();

        while (!webRequest.isDone)
            yield return null;

        byte[] result = webRequest.downloadHandler.data;

        if (result != null)
        {
            string json = Encoding.UTF8.GetString(result);

            JsonHelper.FixJson(ref json);
            Debug.Log(json);
        }
    }

    public IEnumerator Post(GeoSmileModel geoSmileModel)
    {
        string json = JsonUtility.ToJson(geoSmileModel);

        Debug.Log(json);

        var webRequest = new UnityWebRequest(_apiUri, "POST");

        var post = Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(post);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-type", "application/json");

        yield return webRequest.SendWebRequest();
    }
}