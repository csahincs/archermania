using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Utility.UniTaskExtension
{
    public static class UniTaskHelper
    {
        public static async UniTaskVoid WaitAndDo(float waitDuration, Action action, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(waitDuration), cancellationToken: token);
            
            action();
        }
    }
}
