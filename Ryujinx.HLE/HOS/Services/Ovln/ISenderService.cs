using Ryujinx.Common.Logging;

namespace Ryujinx.HLE.HOS.Services.Ovln
{
    [Service("ovln:snd")]
    class ISenderService : IpcService
    {
        public ISenderService(ServiceCtx context) { }

        [CommandHipc(0)]
        public ResultCode OpenSender(ServiceCtx context)
        {
            Logger.Stub?.PrintStub(LogClass.Service);
            return ResultCode.Success;
        }
    }
}