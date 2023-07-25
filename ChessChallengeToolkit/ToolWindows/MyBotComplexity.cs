using Microsoft.VisualStudio.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ChessChallengeToolkit
{
    public class MyBotComplexity : BaseToolWindow<MyBotComplexity>
    {
        public override string GetTitle(int toolWindowId) => "MyBot Complexity";

        public override Type PaneType => typeof(Pane);

        private SolutionItem _myBotClassFile;
        private MyBotComplexityControl _MyBotComplexityControl;

        public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {

            _MyBotComplexityControl = new MyBotComplexityControl();

            _myBotClassFile = GetMyBotClassFile();

            if (_myBotClassFile == null)
            {
                VS.Events.SolutionEvents.OnAfterOpenProject += SolutionEvents_OnAfterOpenProject;
            }
            else
            {
                UpdateComplexity(_myBotClassFile.FullPath);
                VS.Events.DocumentEvents.Saved += DocumentEvents_Saved;
            }

            return Task.FromResult<FrameworkElement>(_MyBotComplexityControl);
        }

        private void SolutionEvents_OnAfterOpenProject(Project project)
        {
            if (project.Name == "Chess-Challenge" && project.Parent?.Name == "Chess-Challenge.sln")
            {
                _myBotClassFile = GetMyBotClassFile();
                if (_myBotClassFile != null)
                {
                    UpdateComplexity(_myBotClassFile.FullPath);
                    VS.Events.DocumentEvents.Saved += DocumentEvents_Saved;
                    VS.Events.SolutionEvents.OnAfterOpenProject -= SolutionEvents_OnAfterOpenProject;
                }
            }
        }

        private SolutionItem GetMyBotClassFile()
        {
            var projects = VS.Solutions.GetAllProjectsAsync().Result;
            var chessChallengeProject = projects.FirstOrDefault(p => p.Name == "Chess-Challenge");
            var srcFolder = chessChallengeProject?.Children.FirstOrDefault(p => p.Text == "src");
            var myBotFolder = srcFolder?.Children.FirstOrDefault(p => p.Text == "My Bot");
            return myBotFolder?.Children.FirstOrDefault(p => p.Text == "MyBot.cs");
        }

        private void DocumentEvents_Saved(string file)
        {
            if (_myBotClassFile.FullPath == file)
            {
                UpdateComplexity(file);
            }
        }

        private void UpdateComplexity(string file)
        {
            using StreamReader reader = new(file);
            string txt = reader.ReadToEnd();
            var tokenCount = ChessChallenge.Application.TokenCounter.CountTokens(txt);
            _MyBotComplexityControl.UpdateProgress(tokenCount);
        }

        [Guid("e8c4274f-3ae5-413f-807f-92ab26bc1bfb")]
        internal class Pane : ToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }
    }
}