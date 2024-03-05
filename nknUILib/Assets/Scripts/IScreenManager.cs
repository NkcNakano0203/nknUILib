using Cysharp.Threading.Tasks;

namespace nkn.UIScreen
{
    public interface IScreenManager
    {
        /// <summary>
        /// スクリーンを切り替える
        /// </summary>
        /// <param name="nextScreen">
        /// 表示したいスクリーン
        /// </param>
        public UniTask ChangeScreenAsync(UIScreen nextScreen);

        /// <summary>
        /// スクリーンを1つ戻す
        /// </summary>
        public UniTask BackScreenAsync();

        /// <summary>
        /// スクリーンを切り替え、スクリーン履歴を置換する
        /// </summary>
        /// <param name="current">
        /// 出したいスクリーン
        /// </param>
        /// <param name="history">
        /// スクリーンの履歴
        /// </param>
        public UniTask ChageScreenStateAsync(UIScreen current, UIScreen[] history);
    }
}