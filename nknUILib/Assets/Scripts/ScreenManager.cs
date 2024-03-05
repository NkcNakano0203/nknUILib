using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace nkn.UIScreen
{
    public class ScreenManager : MonoBehaviour, IScreenManager
    {
        [SerializeField]
        private UIScreen firstScreenPrefab;

        private UIScreen currentScreen;
        private UIScreen currentScreenPrefab;

        // 戻る機能のためのスタック
        private Stack<UIScreen> ScreenPrefabStack = new(8);

        Transform myTransform;

        private void Awake()
        {
            myTransform = transform;
        }

        private void Start()
        {
            BootScreenAsync(firstScreenPrefab).Forget();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        private async UniTask BootScreenAsync(UIScreen firstScreenPrefab)
        {
            currentScreen = Instantiate(firstScreenPrefab, myTransform);
            currentScreenPrefab = firstScreenPrefab;

            currentScreen.SetManager(this);
            await currentScreen.OnEnterScreenAsync();
        }

        //memo:遷移先はScreen側が参照して渡せばいいかな
        // アウトゲームの潤沢なリソースなら生成削除でいいかな
        public async UniTask ChangeScreenAsync(UIScreen nextScreen)
        {
            // 前スクリーンを退場させる
            await currentScreen.OnExitScreenAsync();
            ScreenPrefabStack.Push(currentScreenPrefab);
            Destroy(currentScreen.gameObject);

            // 次スクリーンの登場
            currentScreen = Instantiate(nextScreen, myTransform);
            currentScreen.SetManager(this);
            currentScreenPrefab = nextScreen;
            await currentScreen.OnEnterScreenAsync();
        }

        public async UniTask BackScreenAsync()
        {
            await currentScreen.OnExitScreenAsync();
            Destroy(currentScreen.gameObject);

            // 一つ前のScreenプレハブを読み込む
            if (ScreenPrefabStack.TryPop(out UIScreen prevScreenPrefab))
            {
                currentScreen = Instantiate(prevScreenPrefab, myTransform);
                currentScreenPrefab = prevScreenPrefab;

                currentScreen.SetManager(this);
                await currentScreen.OnEnterScreenAsync();
            }
            else
            {
                await BootScreenAsync(firstScreenPrefab);
            }
        }

        public async UniTask ChageScreenStateAsync(UIScreen current, UIScreen[] history)
        {
            // 履歴を置き換える
            ScreenPrefabStack.Clear();
            for (int i = 0; i < history.Length; i++)
            {
                ScreenPrefabStack.Push(history[i]);
            }

            currentScreenPrefab = current;

            await BootScreenAsync(currentScreenPrefab);
        }
    }
}