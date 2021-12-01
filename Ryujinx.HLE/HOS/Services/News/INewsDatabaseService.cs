using Ryujinx.Common.Logging;
using Ryujinx.HLE.HOS.Kernel.Threading;
using System;
using Ryujinx.HLE.HOS.Ipc;
using Ryujinx.HLE.HOS.Kernel.Common;

namespace Ryujinx.HLE.HOS.Services.News
{
    [Service("news:ndbs")]
    class INewsDatabaseService : IpcService 
    {

        public INewsDatabaseService(ServiceCtx context) { }

        [CommandHipc(1)]
        public ResultCode Count(ServiceCtx context)
        {
            context.ResponseData.Write((uint)0);
            Logger.Stub?.PrintStub(LogClass.Service, "(in INewsDatabaseService) input [" + string.Join(", ", context.RequestData.ReadBytes(9)) + "]");
            return ResultCode.Success;
        }
    }
}
