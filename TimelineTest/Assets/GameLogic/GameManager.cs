using System;
using GameLogic.Timeline;
using GameLogic.Utils;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace GameLogic
{
    public class GameManager : MonoBehaviour
    {
        #region constants
        public const string CharacterAnimation = "CharacterAnimation";
        public const string SignalTrack = "Signal Track";
        #endregion
        
        #region fields
        [SerializeField]
        private PlayableDirector playableDirector;
        [SerializeField]
        private GameObject character;
        [SerializeField]
        private SignalReceiver signalReceiver;
        private PlayableBinding playableBinding;
        private TimelineBindingTool timelineBindingTool;
        #endregion

        #region unity methods
        private void Awake()
        {
            timelineBindingTool = new TimelineBindingTool(playableDirector);
            timelineBindingTool.BindObject(CharacterAnimation, character);
            timelineBindingTool.BindObject(SignalTrack, signalReceiver);
            playableDirector.Play();
        }
        #endregion
    }
}