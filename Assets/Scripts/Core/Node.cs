using UnityEngine;

namespace EvoSim.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Node : MonoBehaviour
    {
        private Rigidbody2D _rb = null;
        private Muscle _muscle = null;

        [SerializeField, NaughtyAttributes.MinMaxSlider(1f, 3f)]
        private Vector2 _frictionRange = new Vector2(1, 1.25f);

        [SerializeField] private float _friction = 0.5f;
        
        private float _forceDir = 0;

        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        private void Start() => _friction = Random.Range(_frictionRange.x, _frictionRange.y);

        private void FixedUpdate()
        {
            if(_muscle == null)
                return;

            var force = new Vector2((_muscle.Strength * _forceDir) / _friction, 0);
            print($"{gameObject.name} -> {force}");
            _rb.AddForce(force);
        }

        public void SetJointProperty(Muscle muscle, float forceDir)
        {
            _muscle = muscle;
            _forceDir = forceDir;
        }
    }
}