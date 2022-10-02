namespace Visual_Novel.Audio
{
    // TODO: Refactor with other command class to inherit from common interface
    // LineInformation should then have a list of commands to execute along side it
    // AudioPlayer and BGScene should be refactored to inherit from common interface
    public abstract class AudioStreamCommand
    {
        public abstract void ExecuteCommand(AudioPlayer audioPlayer);
    }
}
