using System;
using System.Collections.Generic;

namespace ProjetoPDG {
    class PausDeGiz {
        private int peso;

        //"p" é o peso do giz em gramas
        public PausDeGiz(int p) 
        {
                Peso = p;
         }
          public int Peso {
                get
                {
                    return peso;
                }
                set 
                {
                    if (value <= 0) value = 1;
                    peso = value;
                }
            }
    }
    class FabricaDeGiz {
        public List<PausDeGiz> ObterGiz(int p) 
        {
            List<PausDeGiz> temp = new List<PausDeGiz>();
            for (int i = 0; i < p; i++)
                temp.Add(new PausDeGiz(1));
            return temp;
        }
        public bool MudarPeso(PausDeGiz pau, int novoPeso) {
            pau.Peso = novoPeso;
            if (pau.Peso == novoPeso)
                return true;
            else
                return false;
        }
    };
    class Belial 
    { 
        private List<PausDeGiz> AMinhaCopiaPrivadaDeGiz;
        public Belial(List<PausDeGiz> lista)
        {
            AMinhaCopiaPrivadaDeGiz = lista;
        }
        public struct PausEPeso 
        {
            int nPausDeGiz;
            int PesoTotal;
        };
        public int nPausDeGiz 
        {
            get { return AMinhaCopiaPrivadaDeGiz.Count; }
            set {
                if (nPausDeGiz != value)
                    AMinhaCopiaPrivadaDeGiz = (new FabricaDeGiz()).ObterGiz(value);
            }
        }
        public int PesoTotal
        {
            get {
                int pesoacumulado = 0;
                foreach (PausDeGiz pau in AMinhaCopiaPrivadaDeGiz)
                    pesoacumulado += pau.Peso;
                return pesoacumulado;
            }
            set {
                if (PesoTotal != value)
                    AMinhaCopiaPrivadaDeGiz = ((new FabricaDeGiz()).ObterGiz(value));
            }
        }
        public bool MudarPesoTotal(int novoPeso, List<PausDeGiz> lista) 
        {
            int pesoacumulado = 0;
            for (int i = 0; i < lista.Count; i++) {
                PausDeGiz pau = lista[i];
                if (pesoacumulado + pau.Peso <= novoPeso) pesoacumulado += pau.Peso;
                else { lista.RemoveRange(i, lista.Count - i); }
            }
            if (pesoacumulado < novoPeso)
                lista.Add(new PausDeGiz(novoPeso - pesoacumulado));
            return true;
        }
        public bool MudarPesoTotal(int novoPeso) {
            if (PesoTotal == novoPeso) return true;
            int pesoacumulado = 0;
            for (int i = 0; i < AMinhaCopiaPrivadaDeGiz.Count; i++) {
                PausDeGiz pau = AMinhaCopiaPrivadaDeGiz[i];
                if (pesoacumulado + pau.Peso <= novoPeso) pesoacumulado += pau.Peso;
                else AMinhaCopiaPrivadaDeGiz.RemoveRange(i, AMinhaCopiaPrivadaDeGiz.Count - i);
            }
            if (PesoTotal < novoPeso)
                AMinhaCopiaPrivadaDeGiz.Add(new PausDeGiz(novoPeso - PesoTotal));
            return true;
        }
        public int PesoMedio() {
            return PesoTotal / nPausDeGiz;
        }
    }
    class Program  {
        private FabricaDeGiz fabrica = new FabricaDeGiz();
        static private List<PausDeGiz> OMeuGiz;
        public void Amadis(int gramas)
        {
            OMeuGiz = fabrica.ObterGiz(gramas);
        }
        static void Main(string[] args) {
            Program programa = new Program();
            //Obter giz para o cliente Dante.
            programa.Amadis(10);
            Console.WriteLine( "Obtive corretamente 10 gramas de giz para o cliente Dante.");
            //Obter giz para o cliente Gil Vicente.
            //Usar para o exemplo do garbage collector
            programa.Amadis(44);
            Console.WriteLine("Obtive corretamente 44 gramas de giz para o cliente Gil Vicente.");
            Belial belial = new Belial(OMeuGiz);
            Console.WriteLine("Não sabem que meti aqui esta linha, mas posso dizer que os " + belial.PesoTotal + " gramas de giz vêm em " + belial.nPausDeGiz + " paus.");
            Console.WriteLine("Em média, " + belial.PesoTotal / belial.nPausDeGiz + " gramas por pau.");
            belial.MudarPesoTotal(100);
            Console.WriteLine("No Belial estão " + belial.nPausDeGiz + " paus de giz, que pesam " + belial.PesoTotal);
            belial.MudarPesoTotal(85);
            Console.WriteLine("No Belial estão agora " + belial.nPausDeGiz + " paus de giz, que pesam " + belial.PesoTotal);
            belial.MudarPesoTotal(88);
            Console.WriteLine("No Belial estão agora " + belial.nPausDeGiz + " paus de giz, que pesam " + belial.PesoTotal);
            List<PausDeGiz> ListaDeTeste = (new FabricaDeGiz()).ObterGiz(20);
            Console.WriteLine("Na lista de teste estão agora " + ListaDeTeste.Count + " paus de giz, que pesam " + ListaDeTeste.Count + " gramas.");
            belial.MudarPesoTotal(100, ListaDeTeste);
            Console.WriteLine("Na lista de teste estão agora " + ListaDeTeste.Count + " paus de giz, que pesam 100 gramas.");
            belial.MudarPesoTotal(18, ListaDeTeste);
            Console.WriteLine("Na lista de teste estão agora " + ListaDeTeste.Count + " paus de giz, que pesam 18 gramas.");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(Environment.NewLine);
            FabricaDeGiz fabrica = new FabricaDeGiz();
            PausDeGiz paudegiz = new PausDeGiz(10);
            Console.WriteLine("O pau tem " + paudegiz.Peso + " gramas.");
            fabrica.MudarPeso(paudegiz, 20);
            Console.WriteLine("O pau tem " + paudegiz.Peso + " gramas. Devia ter 20, tem?");
            Console.WriteLine("<<CARO ALUNO: pode pressionar qualquer tecla para concluir este exemplo.>>");
            Console.ReadKey();
        }
    }
}