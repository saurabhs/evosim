
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
        [SerializeField] private float _extendedTime = 0.7f;

        [SerializeField] private EState _state = EState.None;

        private float _contractedTime = 0.3f;

        private float _time = 0f;

        public EState State => _state;

        private void Start() => _contractedTime = 1 - _extendedTime;

        private void Update()
        {
            _time += Time.deltaTime;
            if(_time > 1)
                _time = 0f;

            _state = _time < _extendedTime ? EState.Extended : EState.Contracted;
        }
    }
}