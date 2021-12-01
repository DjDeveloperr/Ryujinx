using Ryujinx.Common.Logging;
using Ryujinx.HLE.HOS.Kernel.Threading;
using System;
using Ryujinx.HLE.HOS.Ipc;
using Ryujinx.HLE.HOS.Kernel.Common;

namespace Ryujinx.HLE.HOS.Services.News
{
    [Service("news:oeh")]
    class IOverwriteEventHolder : IpcService 
    {
        KEvent _unknownEvent1;
        int _unknownEvent1Handle;

        public IOverwriteEventHolder(ServiceCtx context) {
            _unknownEvent1 = new KEvent(context.Device.System.KernelContext);
        }

        [CommandHipc(0)]
        public ResultCode Get(ServiceCtx context)
        {
            if (_unknownEvent1Handle == 0)
            {
                if (context.Process.HandleTable.GenerateHandle(_unknownEvent1.ReadableEvent, out _unknownEvent1Handle) != KernelResult.Success)
                {
                    throw new InvalidOperationException("Out of handles!");
                }
            }

            context.Response.HandleDesc = IpcHandleDesc.MakeCopy(_unknownEvent1Handle);
            // _unknownEvent1.WritableEvent.Signal();

            Logger.Stub?.PrintStub(LogClass.Service);
            return ResultCode.Success;
        }
    }
}
