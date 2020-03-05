using UnityEngine;

namespace EvoSim.Core
{
    [RequireComponent(typeof(Muscle))]
    public class JointManager : MonoBehaviour
    {
        private FixedJoint2D[] _joints = null;
        private Muscle _muscle = null;

        private void Awake()
        {
            _muscle = GetComponent<Muscle>();
            _joints = GetComponents<FixedJoint2D>();

            SetMuscleToNode();
        }

        private void SetMuscleToNode()
        {
            for(var i = 0; i < _joints.Length; i++)
            {
                var node = _joints[i].connectedBody.GetComponent<Node>();
                node.SetJointProperty(gameObject.GetComponent<Muscle>(), (_joints[i].anchor.x < 0 ? -1 : 1));
            }
        }
    }
}