using Ryujinx.HLE.HOS.Kernel.Threading;
using System;
using Ryujinx.HLE.HOS.Kernel.Common;
using Ryujinx.Common.Logging;
using Ryujinx.HLE.HOS.Ipc;

namespace Ryujinx.HLE.HOS.Services.Notification
{
    [Service("notif:onsea")] // 9.0.0+
    class INotificationSystemEventAccessor : IpcService
    {
        KEvent _evt;
        int _evtHandle;

        public INotificationSystemEventAccessor(ServiceCtx context) {
            _evt = new KEvent(context.Device.System.KernelContext);
        }

        [CommandHipc(0)]
        public ResultCode UnknownCmd0(ServiceCtx context)
        {
            if (_evtHandle == 0)
            {
                if (context.Process.HandleTable.GenerateHandle(_evt.ReadableEvent, out _evtHandle) != KernelResult.Success)
                {
                    throw new InvalidOperationException("Out of handles!");
                }
            }

            context.Response.HandleDesc = IpcHandleDesc.MakeCopy(_evtHandle);

            Logger.Stub?.Print(LogClass.Service, "Notification");
            return ResultCode.Success;
        }
    }
}