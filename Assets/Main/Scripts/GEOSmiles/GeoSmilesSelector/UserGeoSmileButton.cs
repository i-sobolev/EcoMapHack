using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserGeoSmileButton : MonoBehaviour, IPointerClickHandler
{
    public Action<UserGeoSmileButton> Selected;

    [SerializeField] private Image _imageComponent;

    private UserGeoSmileSelectorElementAnimator _animator = new UserGeoSmileSelectorElementAnimator();

    public int LinkedGeoSmileId { get; private set; }

    public void Set(Sprite sprite, int geosmileId)
    {
        LinkedGeoSmileId = geosmileId;
        _imageComponent.sprite = sprite; 
    }

    public void OnPointerClick(PointerEventData eventData) => Selected?.Invoke(this);

    public Tween Show() => _animator.Show(transform);

    public Tween PlaySelectedAnimation() => _animator.Select(transform, _imageComponent);

    public Tween Hide() => _animator.Hide(_imageComponent);

}

public class UserGeoSmileSelectorElementAnimator
{
    public Tween Show(Transform transform) => transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);

    public Tween Select(Transform transform, Image image)
    {
        return DOTween.Sequence()
            .Append(transform.DOScale(1.3f, 0.34f).SetEase(Ease.OutSine))
            .Join(image.DOFade(0, 0.34f).SetEase(Ease.OutSine));
    }

    public Tween Hide(Image image) => image.DOFade(0, 0.25f);
}