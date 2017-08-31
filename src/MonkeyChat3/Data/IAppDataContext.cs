using AzureMobileClient.Helpers;
using MonkeyChat3.Models;

namespace MonkeyChat3.Data
{
    public interface IAppDataContext
    {
        ICloudSyncTable<TodoItem> TodoItems { get; }
    }
}