using Ryujinx.HLE.HOS.Kernel.Threading;
using Ryujinx.HLE.HOS.Kernel.Common;
using System;
using Ryujinx.HLE.HOS.Ipc;
using Ryujinx.Common.Logging;

namespace Ryujinx.HLE.HOS.Services.Olsc
{
    [Service("olsc:sc")]
    class ITransferTaskListController: IpcService {
        public ITransferTaskListController(ServiceCtx context) { }

        [CommandHipc(5)]
        public ResultCode GetNativeHandleHolder(ServiceCtx context) {
            MakeObject(context, new INativeHandleHolder(context));
            Logger.Stub?.PrintStub(LogClass.ServiceOlsc);
            return ResultCode.Success;
        }

        [CommandHipc(9)]
        public ResultCode GetNativeHandleHolder9(ServiceCtx context) {
            MakeObject(context, new INativeHandleHolder(context));
            Logger.Stub?.PrintStub(LogClass.ServiceOlsc);
            return ResultCode.Success;
        }
    }
}