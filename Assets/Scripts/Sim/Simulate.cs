using System;
using System.Collections;
using UnityEngine;

namespace EvoSim.Sim
{
    public class Simulate : MonoBehaviour
    {
        [SerializeField] private GeneticData _data = null;

        [SerializeField] private Construct _construct = null;

        [SerializeField] private int _population = 1000;

        [SerializeField] private int _duration = 15;
        
        [SerializeField] private int _generationLimit = 20;

        private int _runs = 0;

        private WaitForSeconds _waitBeforeRun = null;
        private WaitForSeconds _waitDuringRun = null;

        private void Start() => Reset();

        private void Reset()
        {
            _data.InitNewData();

            _waitBeforeRun = new WaitForSeconds(1f);
            _waitDuringRun = new WaitForSeconds(_duration);

            StartCoroutine(Execute());
        }

        public IEnumerator Execute()
        {
            var creature = _construct.Create();

            yield return _waitDuringRun;

            var position = 0f;
            var count = 0;

            GetCombinedPositionAndCount(creature, ref position, ref count);
            AddToDatabase(creature, position / count);
            Next();
        }

        private void GetCombinedPositionAndCount(GameObject go, ref float position, ref int count)
        {
            foreach(Transform t in go.transform)
            {
                if(!t.gameObject.name.Contains("m_"))
                {
                    count++;
                    position += t.position.x;
                }
            }
        }

        private void AddToDatabase(GameObject go, float displacement)
        {
            if(displacement <= 0)
                return;

            print($"Moved {displacement}");
            _data.ProcessAndAddToDB(go, displacement);
        }

        private void Next()
        {
            var go = GameObject.FindGameObjectWithTag("Creature");
            if(go != null)
                Destroy(go);

            if(++_runs < _population)
                StartCoroutine(Execute());
            else
                OnSimulationComplete();
        }

        private void OnSimulationComplete()
        {
            _data.OnSimulationComplete();

            if(++_data.SimData.generation < _generationLimit)
                StartCoroutine(Execute());
        }
    }
}