using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System;

namespace Picker3D.Platform.CheckPointSystem
{
    public class CheckPoint : MonoBehaviour
    {
        public int HowManyWeHave { get; set; } = 0;
        public bool isActivated { get; set; } = false;


        [SerializeField] private int howManyNeeded;
        [SerializeField] private TextMeshPro text;

        [SerializeField] private Transform partOfThePlatform;
        [SerializeField] private Transform gatesPlatform;
        [SerializeField] private Transform barrier1;
        [SerializeField] private Transform barrier2;


        private float _timer = 0;
        private bool _isCompleted = false;

        public static event Action Completed = delegate { };

       
        void Start()
        {
            text.text = "0 / " + howManyNeeded.ToString();
        }


        void Update()
        {
            
            text.text = HowManyWeHave.ToString() + " / " + howManyNeeded.ToString();

            if (_isCompleted || !isActivated)
                return;

            _timer += Time.deltaTime;

            if ( HowManyWeHave >= howManyNeeded)
            {
                StartCoroutine(Achieved());
                Debug.Log("1");
                _isCompleted = true;
            }
            if(_timer >= 2)
            {
                Debug.Log("2");
                GameManager.Instance.ChangeGameState(GameManager.StateOfGame.Fail);
                _timer = 0;
            }
        }


        IEnumerator Achieved()
        {

            yield return new WaitForSeconds(0.75f);

            partOfThePlatform.DOLocalMoveY(gatesPlatform.localPosition.y, 1f);

            barrier1.DOLocalMove(new Vector3(-1.25f, 0, -1.25f), 1);
            barrier2.DOLocalMove(new Vector3(-1.25f,0,-1.25f),1);
            barrier1.DOLocalRotate(new Vector3(0, -45, 0), 1);
            barrier2.DOLocalRotate(new Vector3(0, -45, 0), 1);


            yield return new WaitForSeconds(1f);


            Completed();
        }

    }
}

