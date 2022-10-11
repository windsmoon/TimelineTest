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
            TimelineSignalReceiver.TimelineFinishiEvent += OnTimelineFinish;
        }

        private void OnDestroy()
        {
            TimelineSignalReceiver.TimelineFinishiEvent -= OnTimelineFinish;
        }

        #endregion

        #region methods
        private void OnTimelineFinish()
        {
            Debug.Log("receive timeline finishi event");
            animator.SetBool(AnimatorParameterIDReference.ReadyToFight, true);
        }
        #endregion
    }
}