using UnityEngine;

namespace EvoSim.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Node : MonoBehaviour
    {
        private Rigidbody2D _rb = null;
        private Muscle _muscle = null;

        [SerializeField, NaughtyAttributes.MinMaxSlider(0f, 3f)]
        private Vector2 _frictionMinMax = new Vector2(0.25f, 1.25f);

        [SerializeField] private float _friction = 0.5f;
        
        private float _forceDir = 0;

        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        private void Start() => _friction = Random.Range(_frictionMinMax.x, _frictionMinMax.y);

        private void FixedUpdate()
        {
            if(_muscle == null)
                return;

            var force = new Vector2(_muscle.Strength * _friction * _forceDir, 0);
            _rb.AddForce(force);
        }

        public void SetJointProperty(Muscle muscle, float forceDir)
        {
            _muscle = muscle;
            _forceDir = forceDir;
        }
    }
}