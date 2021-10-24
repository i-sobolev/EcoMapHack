using DG.Tweening;
using UnityEngine;

public class UserGeoSmile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public GeoSmile LinkedGeoSmile { get; set; }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    public void SetSprite(GeoSmile geoSmile)
    {
        LinkedGeoSmile = geoSmile;
        _spriteRenderer.sprite = geoSmile.Sprite;
    }

    public void PlaySpawnAnimation()
    {
        _spriteRenderer.DOFade(1, 0.3f);

        var animationDuration = 0.6f;

        DOTween.Sequence()
            .Append(transform.DOScaleY(1.15f, animationDuration).SetEase(Ease.Linear))
            .Join(transform.DOJump(transform.position, 0.5f, 1, animationDuration).SetEase(Ease.Linear))
            .Append(transform.DOScaleY(1, animationDuration * 0.5f).SetEase(Ease.OutBack));
    }

    public void Fade(float endValue) => _spriteRenderer.DOFade(endValue, 0.34f);
}