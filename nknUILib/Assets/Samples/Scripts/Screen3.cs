using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace nkn.UIScreen
{
    public class Screen3 : UIScreen
    {
        [SerializeField]
        private ObservableButton backButton;

        [SerializeField]
        ObservableButton nextButton;

        [SerializeField]
        UIScreen panel4;

        private void Start()
        {
            backButton.SetAction(BackScreenAsync);

            nextButton.SetAction(async () =>
            {
                await ChangeScreenAsync(panel4);
            });
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