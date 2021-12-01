using Ryujinx.Common.Logging;
using Ryujinx.HLE.HOS.Kernel.Threading;
using System;
using Ryujinx.HLE.HOS.Ipc;
using Ryujinx.HLE.HOS.Kernel.Common;

namespace Ryujinx.HLE.HOS.Services.Ns
{
    [Service("ns:dt")]
    class IDownloadTaskInterface : IpcService
    {
        private bool autoCommit;

        public IDownloadTaskInterface(ServiceCtx context) {}

        [CommandHipc(707)]
        public ResultCode EnableAutoCommit(ServiceCtx context)
        {
            autoCommit = true;
            return ResultCode.Success;
        }
    }
}