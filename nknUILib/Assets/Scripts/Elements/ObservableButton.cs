using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace nkn.UIScreen
{
    [RequireComponent(typeof(Button))]
    public class ObservableButton : MonoBehaviour
    {
        private Button button;

        private static ReactiveProperty<bool> activeAll = new(true);

        // 非同期処理対応のためにUniTaskを返り値にしてる
        Func<UniTask> func;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            button.onClick.AddListener(OnClick);
            activeAll
                .Subscribe(x => SetActiveAll(x))
                .AddTo(this);
        }

        /// <summary>
        /// ボタンが押された時の処理を設定
        /// </summary>
        /// <param name="func"></param>
        public void SetAction(Func<UniTask> func)
        {
            this.func = func;
        }

        private async void OnClick()
        {
            SetActiveAll(false);
            await func.Invoke();
            SetActiveAll(true);
        }

        public static void SetActiveAll(bool iaActive)
        {
            activeAll.Value = iaActive;
        }
    }
}