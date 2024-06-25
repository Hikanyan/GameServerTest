using UnityEngine;
using UnityEngine.UI;

namespace HikanyanLaboratory.Script.TitleScene
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField] private GameObject _backgroundCanvas;
        [SerializeField] private GameObject _titleUICanvas;
        [SerializeField] private GameObject _titleMenuCanvas;

        
        public void ShowTitleMenu()
        {
            _backgroundCanvas.SetActive(false);
            _titleUICanvas.SetActive(false);
            _titleMenuCanvas.SetActive(true);
        }
        
        public void ShowTitleUI()
        {
            _backgroundCanvas.SetActive(false);
            _titleUICanvas.SetActive(true);
            _titleMenuCanvas.SetActive(false);
        }
        
        public void ShowBackground()
        {
            _backgroundCanvas.SetActive(true);
            _titleUICanvas.SetActive(false);
            _titleMenuCanvas.SetActive(false);
        }
        
        public void HideAll()
        {
            _backgroundCanvas.SetActive(false);
            _titleUICanvas.SetActive(false);
            _titleMenuCanvas.SetActive(false);
        }
    }
}