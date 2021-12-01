using Ryujinx.HLE.HOS.Kernel.Threading;
using Ryujinx.HLE.HOS.Kernel.Common;
using System;
using Ryujinx.HLE.HOS.Ipc;
using Ryujinx.Common.Logging;

namespace Ryujinx.HLE.HOS.Services.Olsc
{
    [Service("olsc:s")] // 4.0.0+
    class IOlscServiceForSystemService : IpcService
    {
        public IOlscServiceForSystemService(ServiceCtx context) { }

        [CommandHipc(0)]
        public ResultCode Unknown0(ServiceCtx context)
        {
            MakeObject(context, new ITransferTaskListController(context));
            return ResultCode.Success;
        }
    }
}