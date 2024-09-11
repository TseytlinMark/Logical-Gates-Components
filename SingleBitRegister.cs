using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a register that can maintain 1 bit.
    class SingleBitRegister : Gate
    {
        public Wire Input { get; private set; }
        public Wire Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }
        public DFlipFlopGate DFF { get; private set; }

        public SingleBitRegister()
        {
            
            Input = new Wire();
            Load = new Wire();
            Output = new Wire();
            DFF = new DFlipFlopGate();
            MuxGate muxGate = new MuxGate();
            muxGate.ConnectControl(Load);
            DFF.ConnectInput(muxGate.Output);
            muxGate.ConnectInput2(Input);
            muxGate.ConnectInput1(DFF.Output);
            Output.ConnectInput(DFF.Output);
        }

        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }

      

        public void ConnectLoad(Wire wLoad)
        {
            Load.ConnectInput(wLoad);
        }


        public override bool TestGate()
        {
            Input.Value = 1;
            Load.Value = 0;
            DFF.OnClockDown();
            DFF.OnClockUp();
            if (Output.Value != 0)
                return false;
            Input.Value = 1;
            Load.Value = 1;
            DFF.OnClockDown();
            DFF.OnClockUp();
            if (Output.Value != 1)
                return false;
            Input.Value = 0;
            Load.Value = 0;
            DFF.OnClockDown();
            DFF.OnClockUp();
            if (Output.Value != 1)
                return false;
            return true;

        }
    }
}
