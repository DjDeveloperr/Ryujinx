using Ryujinx.Common.Logging;

namespace Ryujinx.HLE.HOS.Services.BluetoothManager
{
    [Service("btm:sys")]
    class IBtmSystem : IpcService
    {
        public IBtmSystem(ServiceCtx context) { }

        [CommandHipc(0)]
        public ResultCode GetCore(ServiceCtx context)
        {
            MakeObject(context, new IBtmSystemCore(context));
            Logger.Stub?.PrintStub(LogClass.ServiceBtm);
            return ResultCode.Success;
        }
    }
}
