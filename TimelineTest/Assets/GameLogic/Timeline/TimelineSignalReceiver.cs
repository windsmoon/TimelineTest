using System;
using UnityEngine;

namespace GameLogic.Timeline
{
    public class TimelineSignalReceiver : MonoBehaviour
    {
        #region events
        public static Action TimelineFinishEvent;
        #endregion

        #region methods
        public void OnTimelineFinish()
        {
            Debug.Log("will fire timeline finish event");
            TimelineFinishEvent?.Invoke();
        }
        #endregion
    }
}   