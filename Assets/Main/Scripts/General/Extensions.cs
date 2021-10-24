using System.Collections;
using UnityEngine;

public static class Extensions
{
    public static T GetRandomObject<T>(this T[] array)
    {
        var randomIndex = Random.Range(0, array.Length);
        return array[randomIndex];
    }

    public static Coroutine Start(this IEnumerator x, MonoBehaviour sender) => sender.StartCoroutine(x);
    public static Color Transparent(this Color x) => new Color(x.r, x.g, x.b, 0);
}
