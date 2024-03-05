using Cysharp.Threading.Tasks;
using UnityEngine;

namespace nkn.UIScreen
{
    /// <summary>
    /// UIスクリーンの継承用クラス
    /// </summary>
    public abstract class UIScreen : MonoBehaviour
    {
        protected IScreenManager ScreenManager { get; private set; }
        protected RectTransform rectTransform;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void SetManager(IScreenManager screenManager)
        {
            ScreenManager = screenManager;
        }

        public async UniTask ChangeScreenAsync(UIScreen nextScreenPrefab)
        {
            await ScreenManager.ChangeScreenAsync(nextScreenPrefab);
            nextScreenPrefab.SetManager(ScreenManager);
        }

        public async UniTask BackScreenAsync()
        {
            await ScreenManager.BackScreenAsync();
        }

        public virtual UniTask OnEnterScreenAsync()
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnExitScreenAsync()
        {
            return UniTask.CompletedTask;
        }
    }
}
