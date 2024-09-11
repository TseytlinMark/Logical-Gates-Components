using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a demux with k outputs, each output with n wires. The input also has n wires.

    class BitwiseMultiwayDemux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Input { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Outputs { get; private set; }

        public BitwiseMultiwayDemux(int iSize, int cControlBits)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Outputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            int outputlength = (int)Math.Pow(2, cControlBits);
            BitwiseDemux[] bitwiseDeMuxes = new BitwiseDemux[outputlength - 1];
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i] = new WireSet(Size);
            }
            for (int i = 0; i < outputlength - 1; i++)
            {
                bitwiseDeMuxes[i] = new BitwiseDemux(Size);
            }
            bitwiseDeMuxes[0].ConnectInput(Input);
            bitwiseDeMuxes[0].ConnectControl(Control[Control.Size - 1]);
            int Gatenumber = 0;
            /*
            int controllerCounter = cControlBits-1;
            */
            //FIXING
            int controllerCounter = 2;
            int controlhelper = 2;
            int gatecounter = 0;
            int c = 0;
            while (Gatenumber + 2 < bitwiseDeMuxes.Length)
            {
                bitwiseDeMuxes[Gatenumber + 1].ConnectInput(bitwiseDeMuxes[c].Output1);
                bitwiseDeMuxes[Gatenumber + 1].ConnectControl(Control[Control.Size - controllerCounter]);
                bitwiseDeMuxes[Gatenumber + 2].ConnectInput(bitwiseDeMuxes[c].Output2);
                bitwiseDeMuxes[Gatenumber + 2].ConnectControl(Control[Control.Size - controllerCounter]);
                Gatenumber = Gatenumber + 2;
                gatecounter = gatecounter + 2;
                c++;
                if (gatecounter == controlhelper)
                {
                    controlhelper = controlhelper * 2;
                    controllerCounter++;
                    gatecounter = 0;
                }
            }
            int j = Outputs.Length - 1;
            for (int a = bitwiseDeMuxes.Length - 1; a > Outputs.Length / 2  - 2; a--)
            {
                Outputs[j].ConnectInput(bitwiseDeMuxes[a].Output2);
                Outputs[j-1].ConnectInput(bitwiseDeMuxes[a].Output1);
                j= j-2;
            }
        }


        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }

  
        public override bool TestGate()
        {
            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 1;
            }
            for (int i = 0; i < Size; i++)
            {
                if (Outputs[0][i].Value != 1)
                    return false;
            }
            Control[0].Value = 1;
            for (int i = 0; i < Size; i++)
            {
                if (Outputs[1][i].Value != 1)
                    return false;
            }
            Control[0].Value = 0;
            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 1;
            }
            for (int i = 0; i < Control.Size; i++)
            {
                Control[i].Value = 1;
            }
            for (int i = 0; i < Size; i++)
            {
                if (Outputs[Outputs.Length - 1][i].Value != 1)
                    return false;
            }
            return true;
        }
    }
}
