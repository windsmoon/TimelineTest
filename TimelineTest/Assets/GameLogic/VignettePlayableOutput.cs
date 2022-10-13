using UnityEngine.Playables;

namespace GameLogic
{
    public struct VignettePlayableOutput : IPlayableOutput
    {
        #region fields
        private PlayableOutputHandle handle;
        #endregion

        #region constructors
        public VignettePlayableOutput(string name)
        {
            UnityEngine.Object handleObject = new UnityEngine.Object();
            handle = new PlayableOutputHandle();
        }

        #endregion

        #region methods
        // public static VignettePlayableOutput Create()
        // {
            // handl
        // }
        
        public PlayableOutputHandle GetHandle()
        {
            return handle;
        }
        #endregion
    }
}