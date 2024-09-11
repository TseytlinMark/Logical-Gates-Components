using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class Program
    {
        static void Main(string[] args)
        {
            //This is an example of a testing code that you should run for all the gates that you create

            //Create a gate
            
            AndGate and = new AndGate();
            Console.WriteLine(and + "");
            //Test that the unit testing works properly
            if (!and.TestGate())
                Console.WriteLine("bugbug");

            /*
            NAndGate.Corrupt = true;
            if (and.TestGate())
                Console.WriteLine("bugbug");
           */

            BitwiseMultiwayMux m = new BitwiseMultiwayMux(7, 3);
            if (!m.TestGate())
                Console.WriteLine("M IS A BAD BOY");
            else
                Console.WriteLine("GOOD BOY M");


            Console.WriteLine("done");
            
            OrGate X = new OrGate();
            Console.WriteLine(X + "");
            //Test that the unit testing works properly
            if (!X.TestGate())
                Console.WriteLine("bugbug");
            else
                Console.WriteLine("OrWorksRight");
            
            XorGate Itzik = new XorGate();
            Console.WriteLine(Itzik + "");
            if (!Itzik.TestGate())
                Console.WriteLine("Bug");
            else
                Console.WriteLine("XorWorksRight");
  
            MuxGate Shlomi = new MuxGate();
            Console.WriteLine(Shlomi + "");
            if (!Shlomi.TestGate())
                Console.WriteLine("Bug");
            else
                Console.WriteLine("MuxWorksRight");
            
            Demux Avi = new Demux();
            Console.WriteLine(Avi + "");
            if (!Avi.TestGate())
                Console.WriteLine("Bug");
            else
                Console.WriteLine("DeMuxWorksRight");

            MultiBitAndGate Tzachi = new MultiBitAndGate(5);
            if (!Tzachi.TestGate())
                Console.WriteLine("MultiBitAndGate Bad");
            else
                Console.WriteLine("MultiBitAndGate good");

            MultiBitAndGate Shimi = new MultiBitAndGate(15);
            if (!Shimi.TestGate())
                Console.WriteLine("MultiBitAndGate Bad");
            else
                Console.WriteLine("MultiBitAndGate good");

            MultiBitOrGate Rami = new MultiBitOrGate(5);
            if (!Rami.TestGate())
                Console.WriteLine("MultiBitOrGate Bad");
            else
                Console.WriteLine("MultiBitOrGate Good");

            MultiBitOrGate Menashe = new MultiBitOrGate(17);
            if (!Rami.TestGate())
                Console.WriteLine("MultiBitOrGate Bad");
            else
                Console.WriteLine("MultiBitOrGate Good");

            BitwiseAndGate Yossi = new BitwiseAndGate(10);
            if (!Yossi.TestGate())
                Console.WriteLine("BitWiseAndGate Bad");
            else
                Console.WriteLine("BitWiseAndGate Good");

            BitwiseAndGate Ami = new BitwiseAndGate(6);
            if (!Ami.TestGate())
                Console.WriteLine("BitWiseAndGate Bad");
            else
                Console.WriteLine("BitWiseAndGate Good");

            BitwiseNotGate Shlomo = new BitwiseNotGate(10);
            if(!Shlomo.TestGate())
                Console.WriteLine("BitWiseNotGate Bad");
            else
                Console.WriteLine("BitWiseNotGate Good");


            BitwiseOrGate Ronen = new BitwiseOrGate(15);
            if (!Ronen.TestGate())
                Console.WriteLine("BitWiseOrGate Bad");
            else
                Console.WriteLine("BitWiseOrGate Good");
            BitwiseMux Haim = new BitwiseMux(16);
            if (!Haim.TestGate())
                Console.WriteLine("BitWiseMux Bad");
            else
                Console.WriteLine("BitWiseMux Good");

            BitwiseDemux Yoni = new BitwiseDemux(8);
            if (!Yoni.TestGate())
                Console.WriteLine("BitWiseDemux Bad");
            else
                Console.WriteLine("BitWiseDemux Good");
            /*
            BitwiseMultiwayMux Perah = new BitwiseMultiwayMux(2, 3);
            if (!Perah.TestGate())
                Console.WriteLine("BitwiseMultiwayMux Bad");
            else
                Console.WriteLine("BitwiseMultiwayMux Good");

            BitwiseMultiwayMux Peri = new BitwiseMultiwayMux(16, 5);
            if (!Peri.TestGate())
                Console.WriteLine("BitwiseMultiwayMux Bad");
            else
                Console.WriteLine("BitwiseMultiwayMux Good");
            
            BitwiseMultiwayDemux Art = new BitwiseMultiwayDemux(4, 3);
            if(!Art.TestGate())
                Console.WriteLine("BitwiseMultiwayDeMux Bad");
            else
                Console.WriteLine("BitwiseMultiwayDeMux Good");
            */
            HalfAdder Menash = new HalfAdder();
            if (!Menash.TestGate())
                Console.WriteLine("HalfAdder Bad");
            else
                Console.WriteLine("HalfAdder Good");
            FullAdder Avihu = new FullAdder();
            if (!Avihu.TestGate())
                Console.WriteLine("FullAdder Bad");
            else
                Console.WriteLine("FullAdder Good");
            
            MultiBitAdder Moti = new MultiBitAdder(5);
            if (!Moti.TestGate())
                Console.WriteLine("MultiBitAdder Bad");
            else
                Console.WriteLine("MultiBitAdder Good");
            MultiBitAdder Misha = new MultiBitAdder(28);
            if (!Moti.TestGate())
                Console.WriteLine("MultiBitAdder Bad");
            else
                Console.WriteLine("MultiBitAdder Good");
            MultiBitAdder Masha = new MultiBitAdder(2343);
            if (!Moti.TestGate())
                Console.WriteLine("MultiBitAdder Bad");
            else
                Console.WriteLine("MultiBitAdder Good");
            /*
            BitwiseMultiwayDemux I = new BitwiseMultiwayDemux(16, 4);
            
            BitwiseMultiwayDemux B = new BitwiseMultiwayDemux(32, 5);
            BitwiseMultiwayDemux C = new BitwiseMultiwayDemux(22, 6);
            BitwiseMultiwayDemux FF = new BitwiseMultiwayDemux(3, 8);
            
            if (!I.TestGate())
                Console.WriteLine("I Problemo");
            else
                Console.WriteLine("I good");

            
            if (!B.TestGate())
                Console.WriteLine("B Problemo");
            else
                Console.WriteLine("B good");
            if (!C.TestGate())
                Console.WriteLine("C Problemo");
            else
                Console.WriteLine("C good");
            if (!FF.TestGate())
                Console.WriteLine("FF Problemo");
            else
                Console.WriteLine("FF good");
            */
            WireSet Marco = new WireSet(4);
            Console.WriteLine(Marco);
            Console.WriteLine("Integer Value inside is : " + Marco.Get2sComplement());
            Marco.Set2sComplement(3);
            Console.WriteLine("Integer Value inside is : "+ Marco.Get2sComplement());
            Console.WriteLine(Marco);
            Marco.Set2sComplement(-3);
            Console.WriteLine("Integer Value inside is : " + Marco.Get2sComplement());
            Console.WriteLine(Marco);
            Marco.Set2sComplement(5);
            Console.WriteLine("Integer Value inside is : " + Marco.Get2sComplement());
            Console.WriteLine(Marco);
            Marco.Set2sComplement(-5);
            Console.WriteLine(Marco);
            Console.WriteLine("Integer Value inside is : " + Marco.Get2sComplement());
            ALU shoo = new ALU(16);
            if (!shoo.TestGate())
                Console.WriteLine("shoo bad");
            else
                Console.WriteLine("Shoo good");

            SingleBitRegister Sami = new SingleBitRegister();
            if(!Sami.TestGate())
                Console.WriteLine("1BR BAD");
            else
                Console.WriteLine("1BR GOOD");
            MultiBitRegister Shimon = new MultiBitRegister(4);
            if (!Shimon.TestGate())
                Console.WriteLine("NBR BAD");
            else
                Console.WriteLine("NBR Good");
            MultiBitRegister MMM = new MultiBitRegister(22);
            if (!MMM.TestGate())
                Console.WriteLine("NBR BAD");
            else
                Console.WriteLine("NBR Good");
            MultiBitRegister VVV = new MultiBitRegister(244);
            if (!VVV.TestGate())
                Console.WriteLine("NBR BAD");
            else
                Console.WriteLine("NBR Good");


            Memory Michaela = new Memory(4, 16);
            if (!Michaela.TestGate())
                Console.WriteLine("Michaela BAD");
            else
                Console.WriteLine("Michaela Good");
            

            Console.ReadLine();
        
        }
    }
}
