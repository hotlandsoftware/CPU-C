/* 
 * Code written by Hotlands Software, Inc. 
 * This here spaghetti code is released under the public domain.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace GetComputerInfo
{
    class GetCPUInstructionSets
    {
        [DllImport("kernel32.dll")]
        static extern bool IsProcessorFeaturePresent(uint ProcessorFeature);

        /* 
         * TODO: Implement a better system than this. It's OK and does the basic job, but it doesn't include a lot of CPU flags, like SSE4.1.
        */

        public const int PF_FLOATING_POINT_PRECISION_ERRATA = 0; // Processor is vulnerable to Pentium floating-point divide bug
        public const int PF_FLOATING_POINT_EMULATED = 1; // The processor does not have floating-point hardware.
        public const int PF_COMPARE_EXCHANGE_DOUBLE = 2; // The atomic compare and exchange operation (CMPXCHG) is available.
        public const int PF_MMX_INSTRUCTIONS_AVAILABLE = 3; // The processor supports MMX instructions.
        public const int PF_XMMI_INSTRUCTIONS_AVAILABLE = 6; // The processor supports SSE instructions.
        public const int PF_3DNOW_INSTRUCTIONS_AVAILABLE = 7; // The processor suppots AMD 3DNow!
        public const int PF_RDTSC_INSTRUCTION_AVAILABLE = 8; // The processor supports RDTSC instructions.
        public const int PF_PAE_ENABLED = 9; // The processor supports PAE instructions.
        public const int PF_XMMI64_INSTRUCTIONS_AVAILABLE = 10; // The processor supports SSE2 instructions. (Requires Windows XP RTM+!)
        public const int PF_SSE_DAZ_MODE_AVAILABLE = 11; // The processor supports DAZ for SSE instructions. (Requires Windows Vista RTM+!)
        public const int PF_NX_ENABLED = 12; // The processor supports NX (Data Execution Prevention) instructions. (Requires Windows XP SP2+!)
        public const int PF_SSE3_INSTRUCTIONS_AVAILABLE = 13; // The processor supports SS3 instructions. (Requires Windows Vista RTM+!)
        public const int PF_COMPARE_EXCHANGE128 = 14; // The atomic compare and exchange 128-bit operation (CMPXCHG16B) is available. (Requires Windows Vista RTM+!)
        public const int PF_COMPARE64_EXCHANGE128 = 15; // The atomic compare 64-bit and exchange 128-bit operation (CMP8XCHG16) is available. (Requires Windows Vista RTM+!)
        public const int PF_XSAVE_ENABLED = 17; // The processor supports XSAVE & XRSTOR instructions. (Requires Windows 7+!)
        public const int PF_VIRT_FIRMWARE_ENABLED = 21; // Virtualization is enabled.
        public const int PF_RDWRFSGSBASE_AVAILABLE = 22; // RDFSBASE, RDGSBASE, WRFSBASE, and WRGSBASE instructions are available.

        public static string instructions = "";

        public static void GetInstructions()
        {
            try
            {
                if (IsProcessorFeaturePresent(PF_3DNOW_INSTRUCTIONS_AVAILABLE))
                {
                    instructions += " 3DNow,";
                }
                if (IsProcessorFeaturePresent(PF_COMPARE_EXCHANGE_DOUBLE))
                {
                    instructions += " CMPXCHG,";
                }
                if (IsProcessorFeaturePresent(PF_COMPARE_EXCHANGE128))
                {
                    instructions += " CMPXCHG16B,";
                }
                if (IsProcessorFeaturePresent(PF_COMPARE64_EXCHANGE128))
                {
                    instructions += " CMP8XCHG16,";
                }
                if (IsProcessorFeaturePresent(PF_MMX_INSTRUCTIONS_AVAILABLE))
                {
                    instructions += " MMX,";
                }
                if (IsProcessorFeaturePresent(PF_NX_ENABLED))
                {
                    instructions += " NX,";
                }
                if (IsProcessorFeaturePresent(PF_PAE_ENABLED))
                {
                    instructions += " PAE,";
                }
                if (IsProcessorFeaturePresent(PF_XMMI_INSTRUCTIONS_AVAILABLE))
                {
                    instructions += " SSE,";
                }
                if (IsProcessorFeaturePresent(PF_XMMI64_INSTRUCTIONS_AVAILABLE))
                {
                    instructions += " SSE2,";
                }
                if (IsProcessorFeaturePresent(PF_SSE3_INSTRUCTIONS_AVAILABLE))
                {
                    instructions += " SSE3,";
                }
                if (IsProcessorFeaturePresent(PF_RDTSC_INSTRUCTION_AVAILABLE))
                {
                    instructions += " RDTSC,";
                }
                if (IsProcessorFeaturePresent(PF_VIRT_FIRMWARE_ENABLED))
                {
                    instructions += " VMX,";
                }
                if (IsProcessorFeaturePresent(PF_XSAVE_ENABLED))
                {
                    instructions += " XSAVE XRSTOR,";
                }
            }
            catch { }
        }
    }
}
