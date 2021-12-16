using ARMeilleure.Memory;
using System;
using System.Diagnostics;

namespace ARMeilleure.State
{
    public class DefaultExecutionContext: IExecutionContext
    {
        private const int MinCountForCheck = 4000;

        private NativeContext _nativeContext;

        internal IntPtr NativeContextPtr => _nativeContext.BasePtr;

        private bool _interrupted;

        public uint CtrEl0   => 0x8444c004;
        public uint DczidEl0 => 0x00000004;

        public ulong CntfrqEl0 { get; set; }

        public ulong GetCntfrqEl0()
        {
            return CntfrqEl0;
        }

        public void SetCntfrqEl0(ulong value)
        {
            CntfrqEl0 = value;
        }

        public ulong CntpctEl0
        {
            get
            {
                double ticks = ExecutionContext.ElapsedTicks * ExecutionContext.TickFrequency;

                return (ulong)(ticks * CntfrqEl0);
            }
        }

        public ulong CntvctEl0 => CntpctEl0;

        public long TpidrEl0 { get; set; }
        public long Tpidr    { get; set; }

        public long GetTpidrEl0()
        {
            return TpidrEl0;
        }

        public void SetTpidrEl0(long value)
        {
            TpidrEl0 = value;
        }

        public long GetTpidr()
        {
            return Tpidr;
        }

        public void SetTpidr(long value)
        {
            Tpidr = value;
        }

        public FPCR Fpcr { get; set; }
        public FPSR Fpsr { get; set; }

        public FPCR StandardFpcrValue => (Fpcr & (FPCR.Ahp)) | FPCR.Dn | FPCR.Fz;

        public FPCR GetFpcr()
        {
            return Fpcr;
        }

        public void SetFpcr(FPCR value)
        {
            Fpcr = value;
        }

        public FPSR GetFpsr()
        {
            return Fpsr;
        }

        public void SetFpsr(FPSR value)
        {
            Fpsr = value;
        }

        public bool IsAarch32 { get; private set; }

        public bool GetIsAarch32()
        {
            return IsAarch32;
        }

        public void SetAarch32(bool value)
        {
            IsAarch32 = value;
        }

        internal ExecutionMode ExecutionMode
        {
            get
            {
                if (IsAarch32)
                {
                    return GetPstateFlag(PState.TFlag)
                        ? ExecutionMode.Aarch32Thumb
                        : ExecutionMode.Aarch32Arm;
                }
                else
                {
                    return ExecutionMode.Aarch64;
                }
            }
        }

        public bool Running
        {
            get => _nativeContext.GetRunning();
            private set => _nativeContext.SetRunning(value);
        }

        public void SetRunning(bool val)
        {
            Running = val;
        }

        public bool IsRunning()
        {
            return Running;
        }

        public event EventHandler<EventArgs>              Interrupt;
        public event EventHandler<InstExceptionEventArgs> Break;
        public event EventHandler<InstExceptionEventArgs> SupervisorCall;
        public event EventHandler<InstUndefinedEventArgs> Undefined;

        public DefaultExecutionContext(IJitMemoryAllocator allocator)
        {
            _nativeContext = new NativeContext(allocator);

            Running = true;

            _nativeContext.SetCounter(MinCountForCheck);
        }

        public ulong GetX(int index)              => _nativeContext.GetX(index);
        public void  SetX(int index, ulong value) => _nativeContext.SetX(index, value);

        public V128 GetV(int index)             => _nativeContext.GetV(index);
        public void SetV(int index, V128 value) => _nativeContext.SetV(index, value);

        public bool GetPstateFlag(PState flag)             => _nativeContext.GetPstateFlag(flag);
        public void SetPstateFlag(PState flag, bool value) => _nativeContext.SetPstateFlag(flag, value);

        public bool GetFPstateFlag(FPState flag) => _nativeContext.GetFPStateFlag(flag);
        public void SetFPstateFlag(FPState flag, bool value) => _nativeContext.SetFPStateFlag(flag, value);

        public void CheckInterrupt()
        {
            if (_interrupted)
            {
                _interrupted = false;

                Interrupt?.Invoke(this, EventArgs.Empty);
            }

            _nativeContext.SetCounter(MinCountForCheck);
        }

        public void RequestInterrupt()
        {
            _interrupted = true;
        }

        public void OnBreak(ulong address, int imm)
        {
            Break?.Invoke(this, new InstExceptionEventArgs(address, imm));
        }

        public void OnSupervisorCall(ulong address, int imm)
        {
            SupervisorCall?.Invoke(this, new InstExceptionEventArgs(address, imm));
        }

        public void OnUndefined(ulong address, int opCode)
        {
            Undefined?.Invoke(this, new InstUndefinedEventArgs(address, opCode));
        }

        public void StopRunning()
        {
            Running = false;

            _nativeContext.SetCounter(0);
        }

        public void Dispose()
        {
            _nativeContext.Dispose();
        }
    }
}