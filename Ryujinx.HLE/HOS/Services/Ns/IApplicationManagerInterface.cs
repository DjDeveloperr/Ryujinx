using Ryujinx.Common.Logging;
using Ryujinx.HLE.HOS.Kernel.Threading;
using System;
using Ryujinx.HLE.HOS.Ipc;
using Ryujinx.HLE.HOS.Kernel.Common;

namespace Ryujinx.HLE.HOS.Services.Ns
{
    [Service("ns:am")]
    class IApplicationManagerInterface : IpcService
    {
        KEvent _applicationRecordUpdateSystemEvent;
        int _applicationRecordUpdateSystemEventHandle;

        KEvent _gameCardUpdateDetectionEvent;
        int _gameCardUpdateDetectionEventHandle;

        KEvent _sdCardMountStatusChangedEvent;
        int _sdCardMountStatusChangedEventHandle;

        KEvent _gameCardMountFailureEvent;
        int _gameCardMountFailureEventHandle;

        public IApplicationManagerInterface(ServiceCtx context) {
            _applicationRecordUpdateSystemEvent = new KEvent(context.Device.System.KernelContext);
            _gameCardUpdateDetectionEvent = new KEvent(context.Device.System.KernelContext);
            _sdCardMountStatusChangedEvent = new KEvent(context.Device.System.KernelContext);
            _gameCardMountFailureEvent = new KEvent(context.Device.System.KernelContext);
        }

        [CommandHipc(0)]
        public ResultCode ListApplicationRecord(ServiceCtx context)
        {
            context.ResponseData.Write((uint)0);
            return ResultCode.Success;
        }

        [CommandHipc(2)]
        public ResultCode GetApplicationRecordUpdateSystemEvent(ServiceCtx context)
        {
            if (_applicationRecordUpdateSystemEventHandle == 0)
            {
                if (context.Process.HandleTable.GenerateHandle(_applicationRecordUpdateSystemEvent.ReadableEvent, out _applicationRecordUpdateSystemEventHandle) != KernelResult.Success)
                {
                    throw new InvalidOperationException("Out of handles!");
                }
            }

            context.Response.HandleDesc = IpcHandleDesc.MakeCopy(_applicationRecordUpdateSystemEventHandle);

            return ResultCode.Success;
        }

        [CommandHipc(44)]
        public ResultCode GetSdCardMountStatusChangedEvent(ServiceCtx context)
        {
            if (_sdCardMountStatusChangedEventHandle == 0)
            {
                if (context.Process.HandleTable.GenerateHandle(_sdCardMountStatusChangedEvent.ReadableEvent, out _sdCardMountStatusChangedEventHandle) != KernelResult.Success)
                {
                    throw new InvalidOperationException("Out of handles!");
                }
            }

            context.Response.HandleDesc = IpcHandleDesc.MakeCopy(_sdCardMountStatusChangedEventHandle);

            return ResultCode.Success;
        }

        [CommandHipc(52)]
        public ResultCode GetGameCardUpdateDetectionEvent(ServiceCtx context)
        {
            if (_gameCardUpdateDetectionEventHandle == 0)
            {
                if (context.Process.HandleTable.GenerateHandle(_gameCardUpdateDetectionEvent.ReadableEvent, out _gameCardUpdateDetectionEventHandle) != KernelResult.Success)
                {
                    throw new InvalidOperationException("Out of handles!");
                }
            }

            context.Response.HandleDesc = IpcHandleDesc.MakeCopy(_gameCardUpdateDetectionEventHandle);

            return ResultCode.Success;
        }

        [CommandHipc(400)]
        // GetApplicationControlData(u8, u64) -> (unknown<4>, buffer<unknown, 6>)
        public ResultCode GetApplicationControlData(ServiceCtx context)
        {
            byte  source  = (byte)context.RequestData.ReadInt64();
            ulong titleId = context.RequestData.ReadUInt64();

            ulong position = context.Request.ReceiveBuff[0].Position;

            byte[] nacpData = context.Device.Application.ControlData.ByteSpan.ToArray();

            context.Memory.Write(position, nacpData);

            return ResultCode.Success;
        }

        [CommandHipc(505)]
        public ResultCode GetGameCardMountFailureEvent(ServiceCtx context)
        {
            if (_gameCardMountFailureEventHandle == 0)
            {
                if (context.Process.HandleTable.GenerateHandle(_gameCardMountFailureEvent.ReadableEvent, out _gameCardMountFailureEventHandle) != KernelResult.Success)
                {
                    throw new InvalidOperationException("Out of handles!");
                }
            }

            context.Response.HandleDesc = IpcHandleDesc.MakeCopy(_gameCardMountFailureEventHandle);

            return ResultCode.Success;
        }
    }
}