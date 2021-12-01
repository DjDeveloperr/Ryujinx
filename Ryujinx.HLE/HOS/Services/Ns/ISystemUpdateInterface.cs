using Ryujinx.HLE.HOS.Kernel.Threading;
using Ryujinx.HLE.HOS.Kernel.Common;
using System;
using Ryujinx.HLE.HOS.Ipc;
using Ryujinx.Common.Logging;

namespace Ryujinx.HLE.HOS.Services.Ns
{
    [Service("ns:su")]
    class ISystemUpdateInterface : IpcService
    {
        private KEvent _systemUpdateNotificationEventForContentDeliveryEvent;
        private int _systemUpdateNotificationEventForContentDeliveryEventHandle;

        public ISystemUpdateInterface(ServiceCtx context) {
            _systemUpdateNotificationEventForContentDeliveryEvent = new KEvent(context.Device.System.KernelContext);
        }

        [CommandHipc(9)]
        public ResultCode GetSystemUpdateNotificationEventForContentDelivery(ServiceCtx context)
        {
            if (_systemUpdateNotificationEventForContentDeliveryEventHandle == 0)
            {
                if (context.Process.HandleTable.GenerateHandle(_systemUpdateNotificationEventForContentDeliveryEvent.ReadableEvent, out _systemUpdateNotificationEventForContentDeliveryEventHandle) != KernelResult.Success)
                {
                    throw new InvalidOperationException("Out of handles!");
                }
            }

            context.Response.HandleDesc = IpcHandleDesc.MakeCopy(_systemUpdateNotificationEventForContentDeliveryEventHandle);
            // _systemUpdateNotificationEventForContentDeliveryEvent.WritableEvent.Signal();
            
            Logger.Stub?.PrintStub(LogClass.ServiceNs);
            return ResultCode.Success;
        }
    }
}