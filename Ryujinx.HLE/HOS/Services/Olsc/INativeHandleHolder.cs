using Ryujinx.HLE.HOS.Kernel.Threading;
using Ryujinx.HLE.HOS.Kernel.Common;
using System;
using Ryujinx.HLE.HOS.Ipc;
using Ryujinx.Common.Logging;

namespace Ryujinx.HLE.HOS.Services.Olsc
{
    [Service("olsc:snh")]
    class INativeHandleHolder: IpcService {
        public INativeHandleHolder(ServiceCtx context) { }

        [CommandHipc(0)]
        public ResultCode GetNativeHandle(ServiceCtx context) {
            Logger.Stub?.PrintStub(LogClass.ServiceOlsc);
            return ResultCode.Success;
        }
    }
}