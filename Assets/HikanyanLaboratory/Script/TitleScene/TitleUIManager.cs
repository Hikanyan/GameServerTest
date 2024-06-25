﻿using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEditor;

namespace HikanyanLaboratory.Script.TitleScene
{
    public class TitleUIManager : MonoBehaviour
    {
        [Header("Canvas")] 
        [SerializeField] private GameObject _backgroundCanvas;
        [SerializeField] private GameObject _titleUICanvas;
        [SerializeField] private GameObject _titleMenuCanvas;
        [SerializeField] private GameObject _optionCanvas;
        [SerializeField] private GameObject _LoginCanvas;
        [SerializeField] private GameObject _debugCanvas;

        [Header("Button")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionButton;
        [SerializeField] private Button _exitButton;


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