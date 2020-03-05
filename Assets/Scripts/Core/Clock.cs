
using UnityEngine;

namespace EvoSim.Core
{
    public enum EState
    {
        None,
        Extended,
        Contracted
    }

    public class Clock : MonoBehaviour
    {
        [SerializeField] private float _extendedTimeDuration = 0.7f;

        [SerializeField] private float _tickLimit = 1f;

        private Vector2 _extendedTimeRange = new Vector2(0, 0.7f);

        [SerializeField] private EState _state = EState.None;

        private float _tick = 0f;

        private float _circularTickMax = 0f;

        public EState State => _state;

        private void Start()
        {
            _extendedTimeRange.x = Random.Range(0, _tickLimit - 0.1f);
            _extendedTimeRange.y = _extendedTimeRange.x + _extendedTimeDuration;
            _circularTickMax = _extendedTimeRange.y;

            if(_extendedTimeRange.y > _tickLimit)
                _extendedTimeRange.y -= _tickLimit;
        }

        private void Update()
        {
            _tick += Time.deltaTime;
            if(_tick > _tickLimit)
                _tick = 0f;

            _state = (_tick > _extendedTimeRange.x && _tick < _circularTickMax) ? EState.Extended : EState.Contracted;
        }
    }
}