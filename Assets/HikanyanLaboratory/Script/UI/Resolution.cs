using UnityEngine;

namespace HikanyanLaboratory.UI
{
    /// <summary>
    /// 画面解像度を変更するクラス
    /// </summary>
    public class Resolution : MonoBehaviour
    {
        private const float AspectRatio = 16.0f / 9.0f;

        public void ChangeResolutionByWidth(int width)
        {
            //画面比に合わせて高さを計算
            int height = (int)(width / AspectRatio);

            bool isFullScreen = Screen.fullScreen;

            Screen.SetResolution(width, height, isFullScreen);
        }
    }
}