using EvoSim.Sim;
using UnityEngine;

namespace EvoSim.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Node : MonoBehaviour
    {
        [SerializeField] private GeneticData _data = null;

        private Rigidbody2D _rb = null;
        private Muscle _muscle = null;

        private float _friction = 0.5f;
        private float _forceDir = 0;

        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        private void Start() => _friction = Random.Range(_data.frictionMin, _data.frictionMax);

        private void FixedUpdate()
        {
            if(_muscle == null)
                return;

            var force = new Vector2((_muscle.Strength * _forceDir) / _friction, 0);
            _rb.AddForce(force);
        }

        public void SetJointProperty(Muscle muscle, float forceDir)
        {
            _muscle = muscle;
            _forceDir = forceDir;
        }
    }
}