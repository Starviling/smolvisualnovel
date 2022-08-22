namespace Visual_Novel.Scenery
{
    /// <summary>
    /// Interface for scene transitions.
    /// </summary>
    public abstract class SceneEffectCommand
    {
        /// <summary>
        /// Executes the command using the given TransitionTween.
        /// </summary>
        /// <param name="bGSceneTween">TransitionTween holds the nodes being operated
        /// on. Also contains information on active node.</param>
        public abstract void ExecuteCommand(TransitionTween bGSceneTween);
    }
}
