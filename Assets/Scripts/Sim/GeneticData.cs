using UnityEngine;

namespace EvoSim.Sim
{
    [CreateAssetMenu]
    public class GeneticData : ScriptableObject
    {
        public int generation = 0;

        /// Node

        public Vector2 nodePositionMin = Vector2.zero;
        public Vector2 nodePositionMax = Vector2.zero;

        public float frictionMax = 2f;
        public float frictionMin = 2f;

        public float _distanceBetweenNodesMin = 1.2f;
        public float _distanceBetweenNodesMax = 2f;

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

        /// utils

        public static float RandomizeFloat(float min, float max) => Random.Range(min, max);

        public static Vector2 RandomizeVector2(Vector2 min, Vector2 max) => new Vector2(RandomizeFloat(min.x, max.x), RandomizeFloat(min.y, max.y));
    }
}