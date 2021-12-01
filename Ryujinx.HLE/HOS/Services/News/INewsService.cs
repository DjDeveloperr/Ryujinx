using Ryujinx.Common.Logging;
using Ryujinx.HLE.HOS.Kernel.Threading;
using System;
using Ryujinx.HLE.HOS.Ipc;
using Ryujinx.HLE.HOS.Kernel.Common;

namespace Ryujinx.HLE.HOS.Services.News
{
    [Service("news:ns")]
    class INewsService : IpcService 
    {
        // KEvent _unknownEvent1;
        // int _unknownEvent1Handle;

        public INewsService(ServiceCtx context) {
            // _unknownEvent1 = new KEvent(context.Device.System.KernelContext);
        }

        [CommandHipc(30100)]
        public ResultCode GetSubscriptionStatus(ServiceCtx context)
        {
            context.ResponseData.Write((uint)0);
            Logger.Stub?.PrintStub(LogClass.Service, "Service.News");
            return ResultCode.Success;
        }
    }
}
