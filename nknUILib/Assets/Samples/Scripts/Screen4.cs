using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace nkn.UIScreen
{
    public class Screen4 : UIScreen
    {
        [SerializeField]
        private ObservableButton backButton;

        private void Start()
        {
            backButton.SetAction(BackScreenAsync);
        }

        public async override UniTask OnEnterScreenAsync()
        {
            rectTransform.anchoredPosition = new Vector3(0, 1500, 0);
            await rectTransform.DOAnchorPosY(
                endValue: 0f,
                duration: 0.1f)
                .SetEase(Ease.OutSine);
        }

        public async override UniTask OnExitScreenAsync()
        {
            rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            await rectTransform.DOAnchorPosY(
                endValue: 1500f,
                duration: 0.1f)
                .SetEase(Ease.OutSine);
        }
    }
}