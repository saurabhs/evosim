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

        private int _runs = 0;

        private WaitForSeconds _waitBeforeRun = null;
        private WaitForSeconds _waitDuringRun = null;

        private void Start()
        {
            _waitBeforeRun = new WaitForSeconds(1f);
            _waitDuringRun = new WaitForSeconds(_duration);
        }

        public IEnumerator Execute()
        {
            _construct.Create();

            yield return _waitDuringRun;

            if(++_runs < _population)
                StartCoroutine(Execute());
        }
    }
}