using UnityEngine;
using Cysharp.Threading.Tasks;

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

        [SerializeField]
        float changeDuration = 0.1f;
        public async override UniTask OnEnterScreenAsync()
        {
            rectTransform.anchoredPosition = new Vector2(0, 1500);

            Vector2 startPosition = rectTransform.anchoredPosition;
            Vector2 endPosition = new Vector2(0, 0);

            float t = 0f;
            while (t < changeDuration)
            {
                t += Time.deltaTime;
                float normalizedTime = t / changeDuration;
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, normalizedTime);
                await UniTask.Yield();
            }

            rectTransform.anchoredPosition = endPosition;
        }
        public async override UniTask OnExitScreenAsync()
        {
            rectTransform.anchoredPosition = new Vector2(0, 0);

            Vector2 startPosition = rectTransform.anchoredPosition;
            Vector2 endPosition = new Vector2(0, 1500);

            float t = 0f;
            while (t < changeDuration)
            {
                t += Time.deltaTime;
                float normalizedTime = t / changeDuration;
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, normalizedTime);
                await UniTask.Yield();
            }

            rectTransform.anchoredPosition = endPosition;
        }
    }
}