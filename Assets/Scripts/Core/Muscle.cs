using EvoSim.Sim;
using UnityEngine;

namespace EvoSim.Core
{
    [RequireComponent(typeof(Clock))]
    public class Muscle : MonoBehaviour
    {
        [SerializeField] GeneticData _data = null;

        private Clock _clock;
        private float _length = 0.4f;
        private float _contractedLength = 0.2f;
        private float _strength = 0f;

        public float Strength => _strength;

        private void Awake() => _clock = GetComponent<Clock>();

        private void Start()
        {
            _length = Random.Range(_data.extendedLengthMin, _data.extendedLengthMax);
            _contractedLength = Random.Range(_data.contractedLengthMin, _data.contractedLengthMax);
            _strength = Random.Range(_data.muscleStrengthMin, _data.muscleStrengthMax);
        }

        private void FixedUpdate()
        {
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