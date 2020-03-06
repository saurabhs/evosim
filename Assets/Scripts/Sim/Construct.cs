using System.Collections.Generic;
using UnityEngine;

namespace EvoSim.Sim
{
    public class Construct : MonoBehaviour
    {
        [SerializeField] private GeneticData _data = null;

        [SerializeField] private GameObject _nodePrefab;
        [SerializeField] private GameObject _musclePrefab;

        private int _nodesAllowed = 2;

        private List<GameObject> _nodes = new List<GameObject>();

        private GameObject _parent = null;

        [NaughtyAttributes.Button]
        public GameObject Create()
        {
            _parent = new GameObject("Creature");
            _parent.tag = "Creature";
            _nodesAllowed = Random.Range(_data.SimData.minNodes, _data.SimData.maxNodes);

            _nodes.Clear();

            CreateNode();
            CreateMuslce();

            return _parent;
        }

        private void CreateNode()
        {
            var positions = new List<Vector2>();
            while(_nodes.Count < _nodesAllowed)
            {
                var inValidPostion = false;
                var pos = new Vector2(
                    Random.Range(_data.SimData.nodePositionMin.x, _data.SimData.nodePositionMax.x),
                    Mathf.Floor(Random.Range(_data.SimData.nodePositionMin.y, _data.SimData.nodePositionMax.y))
                );

                foreach(var p in positions)
                {
                    if(Vector2.Distance(p, pos) < GeneticData.RandomizeFloat(_data.SimData.distanceBetweenNodesMin, _data.SimData.distanceBetweenNodesMax))
                    {
                        inValidPostion = true;
                        break;
                    }
                }

                if(inValidPostion)
                    continue;

                positions.Add(pos);

                var nodeGO = Instantiate(_nodePrefab, pos, Quaternion.identity, _parent.transform);
                nodeGO.name = $"node_{_nodes.Count}";
                _nodes.Add(nodeGO);
            }
        }

        private void CreateMuslce()
        {
            ///using single muscle per node to
            ///not complicate the creature setup

            for(var i = 0; i < _nodes.Count; i++)
            {
                var from = _nodes[i];
                var to = _nodes[(i == _nodes.Count - 1) ? 0 : i + 1];

                var d = to.transform.position - from.transform.position;
                var mid = from.transform.position + (d / 2);

                var muscleGO = Instantiate(_musclePrefab, mid, Quaternion.identity, _parent.transform);
                muscleGO.name = $"m_{from.name}_{to.name}";

                muscleGO.transform.localScale = new Vector3(Mathf.Abs(d.x), 0.125f, 1);

                var joints = muscleGO.GetComponents<Joint2D>();
                joints[0].connectedBody = from.GetComponent<Rigidbody2D>();
                joints[1].connectedBody = to.GetComponent<Rigidbody2D>();

                var angle = Mathf.Atan2(to.transform.position.y - from.transform.position.y,
                    to.transform.position.x - from.transform.position.x) * (180 / Mathf.PI);
                muscleGO.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}