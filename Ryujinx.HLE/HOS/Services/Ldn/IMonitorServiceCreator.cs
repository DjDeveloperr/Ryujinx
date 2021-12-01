namespace Ryujinx.HLE.HOS.Services.Ldn
{
    [Service("lp2p:m")]
    [Service("ldn:m")]
    class IMonitorServiceCreator : IpcService
    {
        public IMonitorServiceCreator(ServiceCtx context) { }

        [CommandHipc(0)]
        public ResultCode CreateMonitorService(ServiceCtx context)
        {
            MakeObject(context, new IMonitorService(context));
            return ResultCode.Success;
        }
    }
}