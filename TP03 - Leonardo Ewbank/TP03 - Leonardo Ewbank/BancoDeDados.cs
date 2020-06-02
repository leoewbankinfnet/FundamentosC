using System;
using System.Collections.Generic;
using System.Text;

namespace TP03___Leonardo_Ewbank
{
    class BancoDeDados
    {
        private static List<Pessoas> pessoasCadastradas = new List<Pessoas>();

        public static void Salvar (Pessoas pessoa)
        {
            pessoasCadastradas.Add(pessoa);
        }
        public static List<Pessoas> PessoasNoSistema()
        {
            return pessoasCadastradas;
        }
    }
}
