using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.ToolsWindows
{
    class KKS
    {
        public string Indeks { get; set; }
        public string TypZmiennej { get; set; }
        public string SymbolUrzadzenia { get; set; }
        public string SymbolSzafy { get; set; }
        public string Opis { get; set; }
        public string Komentarz { get; set; }

        public KKS(string Id, string TpZ, string TypU, string SmbSz, string Ops, string Kom)
        {
            Indeks = Id;
            TypZmiennej = TpZ;
            SymbolUrzadzenia = TypU;
            SymbolSzafy = SmbSz;
            Opis = Ops;
            Komentarz = Kom;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5}", Indeks, TypZmiennej, SymbolUrzadzenia, SymbolSzafy, Opis, Komentarz);
        }
    }
}

