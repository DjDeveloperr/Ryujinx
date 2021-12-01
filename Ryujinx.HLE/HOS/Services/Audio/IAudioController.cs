using Ryujinx.Common.Logging;

namespace Ryujinx.HLE.HOS.Services.Audio
{
    [Service("audctl")]
    class IAudioController : IpcService
    {
        public IAudioController(ServiceCtx context) { }

        [CommandHipc(13)]
        public ResultCode GetOutputModeSetting(ServiceCtx context)
        {
            Logger.Stub?.PrintStub(LogClass.ServiceAudio);
            return ResultCode.Success;
        }

        [CommandHipc(31)]
        public ResultCode IsSpeakerAutoMuteEnabled(ServiceCtx context)
        {
            context.ResponseData.Write(false);
            Logger.Stub?.PrintStub(LogClass.ServiceAudio);
            return ResultCode.Success;
        }

        [CommandHipc(18)]
        public ResultCode GetHeadphoneOutputLevelMode(ServiceCtx context)
        {
            context.ResponseData.Write((uint)1);
            Logger.Stub?.PrintStub(LogClass.ServiceAudio);
            return ResultCode.Success;
        }
    }
}