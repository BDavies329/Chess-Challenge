namespace ChessChallengeToolkit
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyBotComplexityCommand : BaseCommand<MyBotComplexityCommand>
    {
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            return MyBotComplexity.ShowAsync();
        }
    }
}
