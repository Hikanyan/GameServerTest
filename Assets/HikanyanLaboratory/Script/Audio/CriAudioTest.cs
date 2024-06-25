using UnityEngine;
using VContainer;

namespace HikanyanLaboratory
{
    public class CriAudioTest : MonoBehaviour
    {
        private CriAudioPresenter _presenter;

        [Inject]
        public void Construct(CriAudioPresenter presenter)
        {
            _presenter = presenter;
        }

        private void Start()
        {
            _presenter.Start();
            _presenter.Play(CueSheet.BGM, CueName.BGM_Title);
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
        }
    }
}