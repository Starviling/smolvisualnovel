using Godot;

namespace Visual_Novel.Scenery
{
    /// <summary>
    /// Command for cross fading backgrounds.
    /// </summary>
    public class CrossFadeCommand : SceneEffectCommand
    {
        private string resourcePathTrans;

        /// <summary>
        /// Crossfades the active scene with a new background.
        /// </summary>
        /// <param name="resourcePathTrans"></param>
        /// <param name="fadeTime"></param>
        public CrossFadeCommand(string resourcePathTrans, float fadeTime = 2f)
        {
            this.resourcePathTrans = resourcePathTrans;
            this.FadeTime = fadeTime;
        }

        /// <summary>
        /// Executes the transition for the backgrounds.
        /// </summary>
        public override void ExecuteCommand(TransitionTween BGSceneTween)
        {
            BGSceneTween.RemoveAll();
            BGSceneTween.NodeActive.Modulate = new Color(1, 1, 1, 1);
            BGSceneTween.NodeTransitionary.Modulate = new Color(1, 1, 1, 0);

            // Assuming nodeB is a TextureRect
            ((TextureRect)BGSceneTween.NodeTransitionary).Texture = (Texture)GD.Load(resourcePathTrans);
            BGSceneTween.InterpolateProperty(BGSceneTween.NodeActive, "modulate:a", null, 0.0f, FadeTime);
            BGSceneTween.InterpolateProperty(BGSceneTween.NodeTransitionary, "modulate:a", null, 1.0f, FadeTime);
            BGSceneTween.Start();
            BGSceneTween.SwapActiveNode();
        }
    }

    /// <summary>
    /// Command for fading background to black.
    /// </summary>
    public class FadeToBlackCommand : SceneEffectCommand
    {
        /// <summary>
        /// Crossfades the active scene with a new background.
        /// </summary>
        /// <param name="fadeTime"></param>
        public FadeToBlackCommand(float fadeTime = 2f)
        {
            this.FadeTime = fadeTime;
        }

        /// <summary>
        /// Executes the transition for the backgrounds.
        /// </summary>
        public override void ExecuteCommand(TransitionTween BGSceneTween)
        {
            BGSceneTween.RemoveAll();
            BGSceneTween.NodeActive.Modulate = new Color(1, 1, 1, 1);
            BGSceneTween.NodeTransitionary.Modulate = new Color(1, 1, 1, 0);

            // Assuming nodeB is a TextureRect
            ((TextureRect)BGSceneTween.NodeTransitionary).Texture = BGSceneTween.BlackTexture;
            BGSceneTween.InterpolateProperty(BGSceneTween.NodeActive, "modulate:a", null, 0.0f, FadeTime);
            BGSceneTween.InterpolateProperty(BGSceneTween.NodeTransitionary, "modulate:a", null, 1.0f, FadeTime);
            BGSceneTween.Start();
            BGSceneTween.SwapActiveNode();
        }
    }
}
