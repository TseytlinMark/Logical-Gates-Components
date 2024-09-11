using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a FullAdder, taking as input 3 bits - 2 numbers and a carry, and computing the result in the output, and the carry out.
    class FullAdder : TwoInputGate
    {
        public Wire CarryInput { get; private set; }
        public Wire CarryOutput { get; private set; }
        private HalfAdder HA1;
        private HalfAdder HA2;
        private OrGate Orgate;


        public FullAdder()
        {
            CarryInput = new Wire();
            HA1 = new HalfAdder();
            HA2 = new HalfAdder();
            Orgate = new OrGate();
            CarryOutput = new Wire();
            HA1.ConnectInput1(Input1);
            HA1.ConnectInput2(Input2);
            HA2.ConnectInput1(CarryInput);
            HA2.ConnectInput2(HA1.Output);
            Orgate.ConnectInput1(HA2.CarryOutput);
            Orgate.ConnectInput2(HA1.CarryOutput);
            Output.ConnectInput(HA2.Output);
            CarryOutput.ConnectInput(Orgate.Output);
        }


        public override string ToString()
        {
            return Input1.Value + "+" + Input2.Value + " (C" + CarryInput.Value + ") = " + Output.Value + " (C" + CarryOutput.Value + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            CarryInput.Value = 0;
            if (Output.Value != 0 & CarryOutput.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 0;
            CarryInput.Value = 1;
            if (Output.Value != 1 & CarryOutput.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            CarryInput.Value = 0;
            if (Output.Value != 1 & CarryOutput.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            CarryInput.Value = 1;
            if (Output.Value != 0 & CarryOutput.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            CarryInput.Value = 0;
            if (Output.Value != 1 & CarryOutput.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            CarryInput.Value = 1;
            if (Output.Value != 0 & CarryOutput.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            CarryInput.Value = 0;
            if (Output.Value != 0 & CarryOutput.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            CarryInput.Value = 1;
            if (Output.Value != 1 || CarryOutput.Value != 1)
                return false;
            return true;

        }
    }
}
