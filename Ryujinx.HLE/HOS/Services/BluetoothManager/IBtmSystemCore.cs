using Ryujinx.HLE.HOS.Kernel.Threading;
using System;
using Ryujinx.HLE.HOS.Kernel.Common;
using Ryujinx.Common.Logging;
using Ryujinx.HLE.HOS.Ipc;

namespace Ryujinx.HLE.HOS.Services.BluetoothManager
{
    [Service("btm:core")]
    class IBtmSystemCore : IpcService
    {
        KEvent _radioEvent;
        int _radioEventHandle;

        KEvent _gamepadPairingEvent;
        int _gamepadPairingEventHandle;

        public IBtmSystemCore(ServiceCtx context) {
            _radioEvent = new KEvent(context.Device.System.KernelContext);
            _gamepadPairingEvent = new KEvent(context.Device.System.KernelContext);
        }

        [CommandHipc(7)]
        public ResultCode AcquireGamepadEvent(ServiceCtx context)
        {
            if (_gamepadPairingEventHandle == 0)
            {
                if (context.Process.HandleTable.GenerateHandle(_gamepadPairingEvent.ReadableEvent, out _gamepadPairingEventHandle) != KernelResult.Success)
                {
                    throw new InvalidOperationException("Out of handles!");
                }
            }

            context.Response.HandleDesc = IpcHandleDesc.MakeCopy(_gamepadPairingEventHandle);
            _gamepadPairingEvent.WritableEvent.Signal();

            Logger.Stub?.PrintStub(LogClass.Service);

            return ResultCode.Success;
        }

        [CommandHipc(8)]
        public ResultCode AcquireRadioPairingEvent(ServiceCtx context)
        {
            if (_radioEventHandle == 0)
            {
                if (context.Process.HandleTable.GenerateHandle(_radioEvent.ReadableEvent, out _radioEventHandle) != KernelResult.Success)
                {
                    throw new InvalidOperationException("Out of handles!");
                }
            }

            context.Response.HandleDesc = IpcHandleDesc.MakeCopy(_radioEventHandle);

            Logger.Stub?.PrintStub(LogClass.Service);

            return ResultCode.Success;
        }
    }
}