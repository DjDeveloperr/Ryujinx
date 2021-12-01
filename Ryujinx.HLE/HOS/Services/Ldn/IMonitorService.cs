using Ryujinx.Common.Logging;

namespace Ryujinx.HLE.HOS.Services.Ldn
{
    [Service("ldn:ms")]
    class IMonitorService : IpcService
    {
        public IMonitorService(ServiceCtx context) { }

        [CommandHipc(0)]
        public ResultCode GetStateForMonitor(ServiceCtx context)
        {
            context.ResponseData.Write((uint)0);
            Logger.Stub?.PrintStub(LogClass.ServiceLdn);
            return ResultCode.Success;
        }

        [CommandHipc(100)]
        public ResultCode InitializeMonitor(ServiceCtx context)
        {
            Logger.Stub?.PrintStub(LogClass.ServiceLdn);
            return ResultCode.Success;
        }

        [CommandHipc(288)]
        public ResultCode Unknown288(ServiceCtx context)
        {
            Logger.Stub?.PrintStub(LogClass.ServiceLdn);
            return ResultCode.Success;
        }
    }
}