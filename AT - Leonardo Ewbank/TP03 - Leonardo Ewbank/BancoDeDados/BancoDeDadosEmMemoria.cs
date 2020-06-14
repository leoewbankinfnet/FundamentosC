using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.IO;
using System.Linq;

namespace TP03___Leonardo_Ewbank
{
    class BancoDeDadosEmMemoria
    {
        //lista em memoria
         static List<Pessoas> pessoasCadastradas = new List<Pessoas>();

        public static void Salvar(Pessoas pessoa)
        {

            //Salvar em memória

            var jaCad = pessoasCadastradas.Find(x => x.Nome == pessoa.Nome) ;

            if (jaCad != null)
            {
                Apresentacao.Escrever("Já cadastrado");
                System.Threading.Thread.Sleep(1000);
                Apresentacao.Limpar();
                Apresentacao.MenuPrincipal();
               
            }   
            pessoasCadastradas.Add(pessoa);

        }
        public static void Remover(Pessoas pessoa)
        {
            pessoasCadastradas.Remove(pessoa);
        }

        public static List<Pessoas> PessoasNoSistema()
        {
            return pessoasCadastradas;
        }

        //retornar IEnumerable em pesquisas e depois converter se necessário

        public static IEnumerable<Pessoas> PessoasNoSistema(string nome)
        {
            return pessoasCadastradas.Where(pessoa => pessoa.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase));
        }
        public static IEnumerable<Pessoas> PessoasNoSistema(DateTime Data)
        {
            return pessoasCadastradas.Where(pessoa => pessoa.DataAniversario.Month.Equals(Data.Month) && pessoa.DataAniversario.Day.Equals(Data.Day));
        }

        public static Pessoas BuscarPeloNome(string nomeCompleto)
        {
            return pessoasCadastradas.Find(pessoa => pessoa.Nome == nomeCompleto);
        }
    }
}
