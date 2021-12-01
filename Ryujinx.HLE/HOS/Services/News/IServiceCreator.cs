using Ryujinx.Common.Logging;
using Ryujinx.HLE.HOS.Kernel.Threading;
using System;
using Ryujinx.HLE.HOS.Ipc;
using Ryujinx.HLE.HOS.Kernel.Common;

namespace Ryujinx.HLE.HOS.Services.News
{
    [Service("news:a")]
    [Service("news:c")]
    [Service("news:m")]
    [Service("news:p")]
    [Service("news:v")]
    class IServiceCreator : IpcService
    {
        public IServiceCreator(ServiceCtx context) { }

        [CommandHipc(0)]
        public ResultCode CreateNewsService(ServiceCtx context)
        {
            MakeObject(context, new INewsService(context));
            Logger.Stub?.PrintStub(LogClass.Service);
            return ResultCode.Success;
        }

        [CommandHipc(1)]
        public ResultCode CreateNewlyArrivedEventHolder(ServiceCtx context)
        {
            MakeObject(context, new INewlyArrivedEventHolder(context));
            Logger.Stub?.PrintStub(LogClass.Service);
            return ResultCode.Success;
        }

        [CommandHipc(2)]
        public ResultCode CreateNewsDataService(ServiceCtx context)
        {
            MakeObject(context, new INewsDataService(context));
            Logger.Stub?.PrintStub(LogClass.Service);
            return ResultCode.Success;
        }

        [CommandHipc(3)]
        public ResultCode CreateNewsDatabaseService(ServiceCtx context)
        {
            MakeObject(context, new INewsDatabaseService(context));
            Logger.Stub?.PrintStub(LogClass.Service);
            return ResultCode.Success;
        }

        [CommandHipc(4)]
        public ResultCode CreateOverwriteEventHolder(ServiceCtx context)
        {
            MakeObject(context, new IOverwriteEventHolder(context));
            Logger.Stub?.PrintStub(LogClass.Service);
            return ResultCode.Success;
        }
    }
}