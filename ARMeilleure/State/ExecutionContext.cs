using ARMeilleure.Memory;
using System;
using System.Diagnostics;
using ARMeilleure.Hypervisor;

namespace ARMeilleure.State
{
    public class ExecutionContext
    {
        private DefaultExecutionContext _defaultContext;
        private AppleHypervisor _appleHvContext;

        private IExecutionContext _context {
            get {
                if (_appleHvContext != null)
                {
                    return _appleHvContext;
                }
                else
                {
                    return _defaultContext;
                }
            }
        }

        public DefaultExecutionContext DefaultContext => _defaultContext;
        public AppleHypervisor AppleHvContext => _appleHvContext;

        private static Stopwatch _tickCounter;

        private static double _hostTickFreq;

        public uint CtrEl0   => 0x8444c004;
        public uint DczidEl0 => 0x00000004;

        public ulong CntfrqEl0 {
            get {
                return _context.GetCntfrqEl0();
            }

            set {
                _context.SetCntfrqEl0(value);
            }
        }
        public ulong CntpctEl0
        {
            get
            {
                double ticks = _tickCounter.ElapsedTicks * _hostTickFreq;

                return (ulong)(ticks * CntfrqEl0);
            }
        }

        // CNTVCT_EL0 = CNTPCT_EL0 - CNTVOFF_EL2
        // Since EL2 isn't implemented, CNTVOFF_EL2 = 0
        public ulong CntvctEl0 => CntpctEl0;

        public static TimeSpan ElapsedTime => _tickCounter.Elapsed;
        public static long ElapsedTicks => _tickCounter.ElapsedTicks;
        public static double TickFrequency => _hostTickFreq;

        public long TpidrEl0 {
            get {
                return _context.GetTpidrEl0();
            }

            set {
                _context.SetTpidrEl0(value);
            }
        }
        public long Tpidr    {
            get {
                return _context.GetTpidr();
            }

            set {
                _context.SetTpidr(value);
            }
        }

        public FPCR Fpcr {
            get {
                return _context.GetFpcr();
            }

            set {
                _context.SetFpcr(value);
            }
        }
        public FPSR Fpsr {
            get {
                return _context.GetFpsr();
            }

            set {
                _context.SetFpsr(value);
            }
        }
        public FPCR StandardFpcrValue => (Fpcr & (FPCR.Ahp)) | FPCR.Dn | FPCR.Fz;

        public bool IsAarch32 { 
            get {
                return _context.GetIsAarch32();
            }
            
            set {
                _context.SetAarch32(value);
            }
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
            get => _context.IsRunning();
            private set => _context.SetRunning(value);
        }

        public event EventHandler<EventArgs> Interrupt {
            add {
                _context.Interrupt += value;
            }
            remove {
                _context.Interrupt -= value;
            }
        }
        
        public event EventHandler<InstExceptionEventArgs> Break {
            add {
                _context.Break += value;
            }
            remove {
                _context.Break -= value;
            }
        }
        public event EventHandler<InstExceptionEventArgs> SupervisorCall {
            add {
                _context.SupervisorCall += value;
            }
            remove {
                _context.SupervisorCall -= value;
            }
        }
        public event EventHandler<InstUndefinedEventArgs> Undefined {
            add {
                _context.Undefined += value;
            }
            remove {
                _context.Undefined -= value;
            }
        }

        static ExecutionContext()
        {
            _hostTickFreq = 1.0 / Stopwatch.Frequency;

            _tickCounter = new Stopwatch();
            _tickCounter.Start();
        }

        public ExecutionContext(DefaultExecutionContext ctx)
        {
            _defaultContext = ctx;
        }

        public ExecutionContext(AppleHypervisor ctx)
        {
            _appleHvContext = ctx;
        }

        public ulong GetX(int index) => _context.GetX(index);
        public void  SetX(int index, ulong value) => _context.SetX(index, value);

        public V128 GetV(int index) => _context.GetV(index);
        public void SetV(int index, V128 value) => _context.SetV(index, value);

        public bool GetPstateFlag(PState flag) => _context.GetPstateFlag(flag);
        public void SetPstateFlag(PState flag, bool value) => _context.SetPstateFlag(flag, value);

        public bool GetFPstateFlag(FPState flag) => _context.GetFPstateFlag(flag);
        public void SetFPstateFlag(FPState flag, bool value) => _context.SetFPstateFlag(flag, value);

        internal void CheckInterrupt() => _context.CheckInterrupt();

        public void RequestInterrupt() => _context.RequestInterrupt();

        internal void OnBreak(ulong address, int imm) => _context.OnBreak(address, imm);

        internal void OnSupervisorCall(ulong address, int imm) => _context.OnSupervisorCall(address, imm);

        internal void OnUndefined(ulong address, int opCode) => _context.OnUndefined(address, opCode);

        public static void SuspendCounter()
        {
            _tickCounter.Stop();
        }

        public static void ResumeCounter()
        {
            _tickCounter.Start();
        }

        public void StopRunning()
        {
            _context.StopRunning();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}