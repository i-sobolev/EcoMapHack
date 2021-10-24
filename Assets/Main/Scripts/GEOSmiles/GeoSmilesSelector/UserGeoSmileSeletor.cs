using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UserGeoSmileSeletor : MonoBehaviour
{
    public Action<GeoSmile> Selected; 

    [SerializeField] private GeoSmilesConfig _geoSmilesConfig;
    [Space]
    [SerializeField] private Image _background;
    [SerializeField] private Text _text;

    private List<UserGeoSmileButton> _geoSmilesButtons = new List<UserGeoSmileButton>();

    private void Start()
    {
        InitializeButtons();
    }


    public void Show()
    {
        var delay = 0f;
        var delayStep = 0.045f;

        _geoSmilesButtons.ForEach(button => button.Show().SetDelay(delay += delayStep));

        _background.DOFade(0.65f, 0.34f);
        _text.DOFade(1f, 0.34f);
    }

    public void Hide(UserGeoSmileButton selectedButton)
    {
        var startAnimationIndex = _geoSmilesButtons.IndexOf(selectedButton);

        selectedButton.PlaySelectedAnimation();

        var delay = 0f;
        var delayStep = 0.025f;

        for (int i = (startAnimationIndex + 1) % _geoSmilesButtons.Count; i != startAnimationIndex; i = (i + 1) % _geoSmilesButtons.Count)
            _geoSmilesButtons[i].Hide().SetDelay(delay += delayStep);


        _background.DOFade(0, 0.34f);
        _text.DOFade(0, 0.34f);
    }

    private void InitializeButtons()
    {
        _geoSmilesButtons = GetComponentsInChildren<UserGeoSmileButton>().ToList();

        for (int i = 0; i < _geoSmilesConfig.GeoSmiles.Count; i++)
        {
            var currentGeoSmile = _geoSmilesConfig.GeoSmiles[i];
            var currentButton = _geoSmilesButtons[i];

            currentButton.Set(currentGeoSmile.Sprite, currentGeoSmile.Id);
            currentButton.transform.localScale *= 0;

            currentButton.Selected += OnSomeButtonSelected;
        }

        Show();
    }

    private void OnSomeButtonSelected(UserGeoSmileButton button)
    {
        Hide(button);

        Selected?.Invoke(_geoSmilesConfig.GeoSmiles.First(geoSmile => geoSmile.Id == button.LinkedGeoSmileId));
    }
}