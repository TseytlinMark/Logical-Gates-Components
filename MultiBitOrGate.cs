using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)

    class MultiBitOrGate : MultiBitGate
    {

        public MultiBitOrGate(int iInputCount)
            : base(iInputCount)
        {
            OrGate[] OrGates = new OrGate[iInputCount];
            if (iInputCount == 1)
            {
                throw new ArgumentException("1 Bit is not enough for MultiBitGate");
            }
            if (iInputCount == 2)
            {
                OrGate two = new OrGate();
            }
            else
            {
                OrGates[0] = new OrGate();
                OrGates[0].ConnectInput1(m_wsInput[0]);
                OrGates[0].ConnectInput2(m_wsInput[1]);
                for (int i = 1; i < iInputCount - 1; i++)
                {
                    OrGates[i] = new OrGate();
                    OrGates[i].ConnectInput1(OrGates[i - 1].Output);
                    OrGates[i].ConnectInput2(m_wsInput[i + 1]);
                }
                Output = OrGates[iInputCount - 2].Output;

            }

        }

        public override bool TestGate()
        {
            if (m_wsInput.Size > 2)
            {
                for (int i = 0; i < m_wsInput.Size; i++)
                {
                    m_wsInput[i].Value = 0;
                }
                if (Output.Value != 0)
                    return false;
                for (int i = 0; i < m_wsInput.Size; i++)
                {
                    m_wsInput[i].Value = 1;
                }
                if (Output.Value != 1)
                    return false;
                for (int i = 0; i < m_wsInput.Size; i++)
                {
                    m_wsInput[i].Value = 1;
                }
                m_wsInput[1].Value = 0;
                if (Output.Value != 1)
                    return false;
                for (int i = 0; i < m_wsInput.Size; i++)
                {
                    m_wsInput[i].Value = 0;
                }
                m_wsInput[0].Value = 1;
                m_wsInput[1].Value = 1;
                if (Output.Value != 1)
                    return false;
                return true;
            }
            else
                return true;//based on assuring that AndGate Works good for OrGate " two "
        }
    }
}
