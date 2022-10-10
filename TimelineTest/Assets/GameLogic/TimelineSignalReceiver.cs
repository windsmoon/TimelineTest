using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameLogic
{
    public class TimelineSignalReceiver : MonoBehaviour
    {
        #region events
        public static Action TimelineFinishiEvent;
        #endregion
        
        #region methods
        public void OnTimelineFinish()
        {
            Debug.Log("will fire timeline finishi event");
            TimelineFinishiEvent?.Invoke();
        }
        #endregion
    }
}   