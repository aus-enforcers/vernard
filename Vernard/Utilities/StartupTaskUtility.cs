using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Vernard.Utilities
{
    internal static class StartupTaskUtility
    {
        public async static Task<bool> IsEnabled()
        {
            return CheckStartupTaskStateIsEnabled((await GetStartupTask()).State);
        }

        public async static Task<bool> Toggle(bool enabled)
        {
            var startupTask = await GetStartupTask();

            if (startupTask != null)
            {
                if (enabled)
                {
                    return CheckStartupTaskStateIsEnabled(await startupTask.RequestEnableAsync());
                }
                else
                {
                    startupTask.Disable();
                }
            }
            return false;
        }

        private async static Task<StartupTask> GetStartupTask()
        {
            return await StartupTask.GetAsync("YellowCorps.Vernard.StartupTask");
        }

        private static bool CheckStartupTaskStateIsEnabled(StartupTaskState state)
        {
            return (state == StartupTaskState.Enabled || state == StartupTaskState.EnabledByPolicy);
        }
    }
}
