using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a memory unit, containing k registers, each of size n bits.
    class Memory : SequentialGate
    {
        //The address size determines the number of registers
        public int AddressSize { get; private set; }
        //The word size determines the number of bits in each register
        public int WordSize { get; private set; }

        //Data input and output - a number with n bits
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //The address of the active register
        public WireSet Address { get; private set; }
        //A bit setting the memory operation to read or write
        public Wire Load { get; private set; }
        public BitwiseMultiwayDemux AddressDeMux { get; private set;}
        public BitwiseMultiwayMux OutputMux { get; private set; }
        public MultiBitRegister[] nBRs { get; private set; }

        //your code here

        public Memory(int iAddressSize, int iWordSize)
        {
            AddressSize = iAddressSize;
            WordSize = iWordSize;

            Input = new WireSet(WordSize);
            Output = new WireSet(WordSize);
            Address = new WireSet(AddressSize);
            //Bit of W/R
            Load = new Wire();
            WireSet LoadWireSet = new WireSet(1);
            LoadWireSet[0].ConnectInput(Load);
            int nBRsSize = (int)Math.Pow(2, AddressSize);
            nBRs = new MultiBitRegister[nBRsSize];
            AddressDeMux = new BitwiseMultiwayDemux(1, AddressSize);
            AddressDeMux.ConnectControl(Address);
            AddressDeMux.ConnectInput(LoadWireSet);
            OutputMux = new BitwiseMultiwayMux(WordSize, AddressSize);
            OutputMux.ConnectControl(Address);
            for (int i = 0; i < nBRsSize; i++)
            {
                nBRs[i] = new MultiBitRegister(WordSize);
                nBRs[i].Load.ConnectInput(AddressDeMux.Outputs[i][0]);
                nBRs[i].ConnectInput(Input);
                OutputMux.Inputs[i].ConnectInput(nBRs[i].Output);
            }
            Output.ConnectInput(OutputMux.Output);
        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectAddress(WireSet wsAddress)
        {
            Address.ConnectInput(wsAddress);
        }


        public override void OnClockUp()
        {
        }

        public override void OnClockDown()
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override bool TestGate()
        {
            Load.Value = 1;
            Input.SetValue(1);
            Address.SetValue(0);
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 1)
                return false;
            Input.SetValue(0);
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 0)
                return false;
            Input.SetValue((int)Math.Pow(2, WordSize) - 1);
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 0)
                return false;
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != (int)Math.Pow(2, WordSize) - 1)
                return false;
            Address.SetValue(1);
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != (int)Math.Pow(2, WordSize) - 1)
                return false;

            return true;
        }
    }
    
}
