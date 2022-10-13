using GameLogic.Timeline;
using GameLogic.Utils;
using UnityEngine;

namespace GameLogic.Character
{
    public class CharacterController : MonoBehaviour
    {
        #region fileds
        private Animator animator;
        #endregion
        
        #region unity methods
        private void Awake()
        {
            animator = GetComponent<Animator>();
            TimelineSignalReceiver.TimelineFinishEvent += OnTimelineFinish;
        }

        private void OnDestroy()
        {
            TimelineSignalReceiver.TimelineFinishEvent -= OnTimelineFinish;
        }

        #endregion

        #region methods
        private void OnTimelineFinish()
        {
            Debug.Log("receive timeline finish event");
            animator.SetBool(AnimatorParameterIDReference.ReadyToFight, true);
        }
        #endregion
    }
}