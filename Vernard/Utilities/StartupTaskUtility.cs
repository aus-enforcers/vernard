using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Vernard.Utilities
{
    internal static class StartupTaskUtility
    {
        public static async Task<bool> IsEnabled()
        {
            return CheckStartupTaskStateIsEnabled((await GetStartupTask()).State);
        }

        public static async Task<bool> Toggle(bool enabled)
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

        private static async Task<StartupTask> GetStartupTask()
        {
            return await StartupTask.GetAsync("YellowCorps.Vernard.StartupTask");
        }

        private static bool CheckStartupTaskStateIsEnabled(StartupTaskState state)
        {
            return (state == StartupTaskState.Enabled || state == StartupTaskState.EnabledByPolicy);
        }
    }
}
