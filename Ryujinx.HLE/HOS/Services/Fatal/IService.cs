using Ryujinx.Common.Logging;
using System;

namespace Ryujinx.HLE.HOS.Services.Fatal
{
    [Service("fatal:u")]
    class IService : IpcService
    {
        public IService(ServiceCtx context) { }

        [CommandHipc(2)]
        public ResultCode ThrowFatalWithCpuContext(ServiceCtx context)
        {   
            Logger.Stub?.PrintStub(LogClass.Service, "fatal:u::ThrowFatalWithCpuContext");
            return ResultCode.Success;
        }
    }
}