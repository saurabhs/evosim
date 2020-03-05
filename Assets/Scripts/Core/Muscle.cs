using UnityEngine;

namespace EvoSim.Core
{
    [RequireComponent(typeof(Clock))]
    public class Muscle : MonoBehaviour
    {
        private Clock _clock;

        [SerializeField] private float _length = 0.4f;
        [SerializeField] private float _contractedLength = 0.2f;
        [SerializeField, NaughtyAttributes.MinMaxSlider(5, 50)] private Vector2 _strengthRange = new Vector2(5, 25);

        [NaughtyAttributes.ReadOnly] private float _strength = 0f;

        public float Strength => _strength;

        private void Awake() => _clock = GetComponent<Clock>();

        private void Start() => _strength = Random.Range(_strengthRange.x, _strengthRange.y);

        private void Update()
        {
            if(_clock.State == EState.Extended)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(_length, transform.localScale.y), _strength * Time.deltaTime);
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(_contractedLength, transform.localScale.y), _strength * Time.deltaTime);
            }
        }
    }
}