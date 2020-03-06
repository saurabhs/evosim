using EvoSim.Sim;
using UnityEngine;

namespace EvoSim.Core
{
    [RequireComponent(typeof(Clock))]
    public class Muscle : MonoBehaviour
    {
        [SerializeField] GeneticData _data = null;

        [SerializeField] private float _fakeDelay = 1.5f;
        private float _delayTimer = 0f;

        private Clock _clock;
        private float _length = 0.4f;
        private float _contractedLength = 0.2f;
        private float _strength = 0f;

        public float Length => _length;
        public float ContractedLength => _contractedLength;
        public float Strength => _strength;

        private void Awake() => _clock = GetComponent<Clock>();

        private void OnEnable()
        {
            _length = Random.Range(_data.SimData.extendedLengthMin, _data.SimData.extendedLengthMax);
            _contractedLength = Random.Range(_data.SimData.contractedLengthMin, _data.SimData.contractedLengthMax);
            _strength = Random.Range(_data.SimData.muscleStrengthMin, _data.SimData.muscleStrengthMax);
        }

        private void FixedUpdate()
        {
            if(_delayTimer < _fakeDelay)
            {
                _delayTimer += Time.deltaTime;
                return;
            }

            transform.localScale =
                Vector3.Lerp
                (
                    transform.localScale,
                    new Vector2(_clock.State == EState.Extended ? _length : _contractedLength, transform.localScale.y),
                    _strength * Time.deltaTime
                );
        }
    }
}