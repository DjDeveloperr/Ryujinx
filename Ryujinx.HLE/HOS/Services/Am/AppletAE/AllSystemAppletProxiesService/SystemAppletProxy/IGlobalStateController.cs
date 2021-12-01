using Ryujinx.Common.Logging;
using Ryujinx.HLE.HOS.Kernel.Threading;
using System;
using Ryujinx.HLE.HOS.Ipc;
using Ryujinx.HLE.HOS.Kernel.Common;

namespace Ryujinx.HLE.HOS.Services.Am.AppletAE.AllSystemAppletProxiesService.SystemAppletProxy
{
    class IGlobalStateController : IpcService
    {
        KEvent _hdcpAuthenticationFailed;
        int _hdcpAuthenticationFailedHandle;

        public IGlobalStateController(ServiceCtx context) {
            _hdcpAuthenticationFailed = new KEvent(context.Device.System.KernelContext);   
        }

        [CommandHipc(15)]
        public ResultCode GetHdcpAuthenticationFailedEvent(ServiceCtx context)
        {
            if (_hdcpAuthenticationFailedHandle == 0)
            {
                if (context.Process.HandleTable.GenerateHandle(_hdcpAuthenticationFailed.ReadableEvent, out _hdcpAuthenticationFailedHandle) != KernelResult.Success)
                {
                    throw new InvalidOperationException("Out of handles!");
                }
            }

            context.Response.HandleDesc = IpcHandleDesc.MakeCopy(_hdcpAuthenticationFailedHandle);
            Logger.Stub?.PrintStub(LogClass.ServicePctl);

            return ResultCode.Success;
        }
    }
}