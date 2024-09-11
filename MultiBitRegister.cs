using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents an n bit register that can maintain an n bit number
    class MultiBitRegister : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }

        //Word size - number of bits in the register
        public int Size { get; private set; }
        public SingleBitRegister[] oBRs;
        

        public MultiBitRegister(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            Load = new Wire();
            oBRs = new SingleBitRegister[Size];
            for (int i = 0; i < Size; i++)
            {
                oBRs[i] = new SingleBitRegister();
                oBRs[i].ConnectInput(Input[i]);
                oBRs[i].ConnectLoad(Load);
                Output[i].ConnectInput(oBRs[i].Output);
            }
        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        
        public override string ToString()
        {
            return Output.ToString();
        }


        public override bool TestGate()
        {

            Input.SetValue(14);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 14)
                return false;
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 14)
                return false;
            Input.SetValue(0);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 0)
                return false;
            Input.SetValue(5);
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 5)
                return false;
            Input.SetValue(6);
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 5)
                return false;
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Input.Set2sComplement(-6);
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.Get2sComplement() != -6)
                return false;
            for (int i = 0; i < 8; i++)
            {
                Input.SetValue(i);
                Clock.ClockDown();
                Clock.ClockUp();
                if (Output.GetValue() != i)
                    return false;
            }

            return true;


        }
    }
}
