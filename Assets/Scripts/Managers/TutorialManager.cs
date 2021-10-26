using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.GenericSingletons;

namespace Tutorial
{


    public class TutorialManager : MonoBehaviourSingleton<TutorialManager>
    {

        [SerializeField] private GameObject[] tutorialNodes;

        private int tutorialNodeIndex = 0;

        void Awake()
        {
            tutorialNodeIndex = 0;
            DisableAllTutorialNodes();
        }


        public void StartTutorial(Platform platform)
        {
            MapController.instance.onCreateNewPlatform += OnJumpToNewPlatform;
            SetTutorialOnPlatform(platform);
            DisplayTutorialNode();
        }


        private void DisplayTutorialNode()
        {
            Debug.Log("Display tutorial number: " + tutorialNodeIndex);
            tutorialNodes[tutorialNodeIndex].SetActive(true);

            tutorialNodeIndex += 1;

            if (tutorialNodeIndex > tutorialNodes.Length - 1)
            {
                EndTutorialMode();
            }
        }

        private void OnJumpToNewPlatform(Platform platform)
        {
            SetTutorialOnPlatform(platform);
            DisplayTutorialNode();
        }

        private void SetTutorialOnPlatform(Platform platform)
        {
            tutorialNodes[tutorialNodeIndex].transform.position = platform.transform.position;
        }

        private void EndTutorialMode()
        {
            Debug.Log("Ended Tutorial");
            MapController.instance.onCreateNewPlatform -= OnJumpToNewPlatform;
        }



        private void DisableAllTutorialNodes()
        {
            foreach (var item in tutorialNodes)
            {
                item.SetActive(false);
            }
        }
    }
}