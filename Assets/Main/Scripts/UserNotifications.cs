using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UserNotifications : MonoBehaviour
{
    [SerializeField] private Image _circle;
    [SerializeField] private Text _text;
    [SerializeField] private Image _arrow;

    private void Start()
    {
        _circle.fillAmount = 0;
        _arrow.fillAmount = 0;
        _text.DOFade(0f, 0f);

        ShowLoader();
    }

    private void OnEnable()
    {
        ARPlaneRaycaster.PlaneRaycasted += OnPlaneRaycasted;
        
        UserGeoSmileSeletor.Selected += (_) => GeoSmilePlacer.GeoSmilePlaced += ShowSuccessMessage;
    }

    private void OnDisable()
    {
        ARPlaneRaycaster.PlaneRaycasted -= OnPlaneRaycasted;
        GeoSmilePlacer.GeoSmilePlaced -= ShowSuccessMessage;
    }

    private void OnPlaneRaycasted(Vector3 position)
    {
        HideLoader();

        ARPlaneRaycaster.PlaneRaycasted -= OnPlaneRaycasted;
    }

    public void ShowLoader()
    {
        _text.DOFade(1f, 1.5f);

        DOTween.Sequence()
            .Append(_circle.transform.DOLocalRotate(new Vector3 { z = 360f }, 3f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear))
            .SetLoops(-1)
            .SetId(_circle);

        DOTween.Sequence()
            .Append(_circle.DOFillAmount(1, 3f).SetEase(Ease.InOutQuad))
            .Append(_circle.DOFillAmount(0, 3f).SetEase(Ease.InOutQuad))
            .SetLoops(-1, LoopType.Yoyo)
            .SetId(_circle);
    }

    public Tween HideLoader()
    {
        DOTween.Kill(_circle);

        _text.DOFade(0f, 1.5f);

        return DOTween.Sequence()
            .Append(_circle.transform.DOLocalRotate(new Vector3(), 0.5f).SetEase(Ease.Linear))
            .Append(_circle.DOFillAmount(0, 0.5f).SetEase(Ease.InOutQuad))
            .SetId(_circle);
    }

    public void ShowSuccessMessage()
    {
        DOTween.Sequence()
            .Append(_circle.DOFillAmount(1, 0.5f).SetEase(Ease.InOutQuad))
            .Join(_arrow.DOFillAmount(1, 0.5f).SetEase(Ease.InOutQuad))
            .AppendInterval(1.5f)
            .Append(_circle.DOFillAmount(0, 0.5f).SetEase(Ease.InOutQuad))
            .Join(_arrow.DOFillAmount(0, 0.5f).SetEase(Ease.InOutQuad))
            .AppendCallback(() => Destroy(gameObject));
    }
}