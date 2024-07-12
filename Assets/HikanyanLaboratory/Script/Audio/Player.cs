using CriWare;
using UnityEngine;

namespace HikanyanLaboratory.Audio
{
    public class Player
    {
        private readonly ICriAudioPlayerService _playerService;
        private readonly string _cueName;
        private float _volume = 1f;
        private bool _isLoop = false;

        public Player(ICriAudioPlayerService playerService, string cueName)
        {
            _playerService = playerService;
            _cueName = cueName;
        }

        public Player Volume(float volume)
        {
            _volume = volume;
            return this;
        }

        public Player Loop(bool isLoop)
        {
            _isLoop = isLoop;
            return this;
        }

        public void Play()
        {
            _playerService.Play(_cueName, _volume, _isLoop);
        }

        public void Play3D(GameObject gameObject)
        {
            _playerService.Play3D(gameObject, _cueName, _volume, _isLoop);
        }
    }
}