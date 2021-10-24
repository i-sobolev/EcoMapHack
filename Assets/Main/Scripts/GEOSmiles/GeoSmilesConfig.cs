using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GeoSmilesConfig : ScriptableObject
{
    [SerializeField] private List<GeoSmile> _geoSmiles;

    public IReadOnlyList<GeoSmile> GeoSmiles => _geoSmiles;
}