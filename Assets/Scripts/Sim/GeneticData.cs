using System.Collections.Generic;
using EvoSim.Core;
using UnityEngine;

namespace EvoSim.Sim
{
    [System.Serializable]
    public class Data
    {
        public int generation = 0;

        /// Node

        public Vector2 nodePositionMin = Vector2.zero;
        public Vector2 nodePositionMax = Vector2.zero;

        public float frictionMax = 2f;
        public float frictionMin = 2f;

        public float distanceBetweenNodesMin = 1.2f;
        public float distanceBetweenNodesMax = 2f;

        public int minNodes = 2;
        public int maxNodes = 6;

        /// muscle

        public float muscleStrengthMax = 20f;
        public float muscleStrengthMin = 50f;

        public float extendedLengthMax = 2f;
        public float extendedLengthMin = 0.5f;

        public float contractedLengthMax = 0.5f;
        public float contractedLengthMin = 0.1f;

        ///clock

        public float extendedTimeMax = 0.9f;
        public float extendedTimeMin = 0.3f;

        public static Data Init()
        {
            var data = new Data();

            data.nodePositionMax = new Vector2(float.MinValue, float.MinValue);
            data.nodePositionMin = new Vector2(float.MaxValue, float.MaxValue);

            data.frictionMax = float.MinValue;
            data.frictionMin = float.MaxValue;

            data.maxNodes = int.MaxValue;
            data.minNodes = int.MinValue;

            data.distanceBetweenNodesMax = float.MaxValue;
            data.distanceBetweenNodesMin = float.MinValue;

            data.muscleStrengthMax = float.MinValue;
            data.muscleStrengthMin = float.MaxValue;

            data.extendedLengthMax = float.MinValue;
            data.extendedLengthMin = float.MaxValue;

            data.contractedLengthMax = float.MinValue;
            data.contractedLengthMin = float.MaxValue;

            data.extendedTimeMax = float.MinValue;
            data.extendedTimeMin = float.MaxValue;

            return data;
        }
    }

    [CreateAssetMenu]
    public class GeneticData : ScriptableObject
    {
        [SerializeField] private Data _data;

        public Data SimData => _data;

        public Data _currenSimData;

        /// utils

        public static float RandomizeFloat(float min, float max) => UnityEngine.Random.Range(min, max);

        public static Vector2 RandomizeVector2(Vector2 min, Vector2 max) => new Vector2(RandomizeFloat(min.x, max.x), RandomizeFloat(min.y, max.y));

        /// <summary>
        /// anythibg below should be in a separate DB class, but oh well!
        /// </summary>

        public void InitNewData() => _currenSimData = Data.Init();

        public void SetData(Data data) => _data = data;

        public void ProcessAndAddToDB(GameObject go, float displacement)
        {
            var nodesCount = 0;
            foreach(Transform t in go.transform)
            {
                if(t.gameObject.name.Contains("m_"))
                {
                    var m = t.GetComponent<Muscle>();
                    if(m.Length > _currenSimData.extendedLengthMax)
                        _currenSimData.extendedLengthMax = m.Length;
                    if(m.Length < _currenSimData.extendedLengthMin)
                        _currenSimData.extendedLengthMin = m.Length;

                    if(m.Strength > _currenSimData.muscleStrengthMax)
                        _currenSimData.muscleStrengthMax = m.Strength;
                    if(m.Strength < _currenSimData.muscleStrengthMin)
                        _currenSimData.muscleStrengthMin = m.Strength;

                    var clock = m.GetComponent<Clock>();
                    if(clock.ExtendedTimeDuration > _currenSimData.extendedTimeMax)
                        _currenSimData.extendedTimeMax = clock.ExtendedTimeDuration;
                    if(clock.ExtendedTimeDuration < _currenSimData.extendedTimeMin)
                        _currenSimData.extendedTimeMin = clock.ExtendedTimeDuration;
                }
                else
                {
                    var n = t.GetComponent<Node>();
                    if(n.transform.position.x > _currenSimData.nodePositionMax.x ||
                        n.transform.position.y > _currenSimData.nodePositionMax.y)
                        _currenSimData.nodePositionMax = n.transform.position;
                    if(n.transform.position.x < _currenSimData.nodePositionMin.x ||
                        n.transform.position.y < _currenSimData.nodePositionMin.y)
                        _currenSimData.nodePositionMin = n.transform.position;

                    if(n.Friction > _currenSimData.frictionMax)
                        _currenSimData.frictionMax = n.Friction;
                    if(n.Friction < _currenSimData.frictionMin)
                        _currenSimData.frictionMin = n.Friction;

                    nodesCount++;
                }
            }

            if(nodesCount > _currenSimData.maxNodes)
                _currenSimData.maxNodes = nodesCount;
            if(nodesCount < _currenSimData.minNodes)
                _currenSimData.minNodes = nodesCount;
        }

        public void OnSimulationComplete() => _data = _currenSimData;
    }
}