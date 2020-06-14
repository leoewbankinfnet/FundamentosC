using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.IO;
using System.Linq;

namespace TP03___Leonardo_Ewbank.Controller
{
    public class BancoDeDadosEmArquivo
    {
        private static string ObterNomeArq()
        {
            var pastaDesktop = Environment.SpecialFolder.Desktop; //buscando a pasta

            var localDaPastaDesktop = Environment.GetFolderPath(pastaDesktop); //Pegando o caminho da pasta

            var nomeDoArquivo = @"\ListaAniversariantes.txt";

            return localDaPastaDesktop + nomeDoArquivo;
        }

        public static void Salvar(Pessoas pessoa)
        {
            //Salvar funcionario  no arquivo

            var existente = BuscarPeloNome(pessoa.Nome);
            if (existente == null)
            {
                string salvarNoArq = $"{pessoa.Nome},{pessoa.DataAniversario.Date.ToString()}\n";

                File.AppendAllText(ObterNomeArq(), salvarNoArq); //cria/abre o arquivo, adiciona o conteudo ao arquivo e fecha;
            }
            else
            {
                Apresentacao.Limpar();
                Apresentacao.Escrever("Ja cadastrado. Pressione uma tecla para voltar ao menu principal");
                Console.ReadKey();
                Apresentacao.MenuPrincipal();
            }

            // File.WriteAllText(localDaPastaDesktop + nomeDoArquivo, nome); abre o arquivo, sobrescreve o arquivo e fecha o arquivo

        }
        public static void Remover(Pessoas pessoa)
        {
            string nome = pessoa.Nome;

            var antigos = File.ReadAllLines(ObterNomeArq());
            var novas = antigos.Where(linha => !linha.Contains(nome));
            File.WriteAllLines(ObterNomeArq(), novas);
        }

        public static List<Pessoas> PessoasNoSistema()
        {


            var nomeDoArquivo = ObterNomeArq();
            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo))
            {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }

            //Identificar as pessoas

            string[] pessoas = File.ReadAllLines(ObterNomeArq()).ToArray();
            List<Pessoas> pessoasCadastradasNoArq = new List<Pessoas>();

            for (int i = 0; i < pessoas.Length; i++)
            {
                //Identificando cada pessoa
                string[] dadosPessoa = pessoas[i].Split(',');
                string nome = dadosPessoa[0];
                DateTime dataNasc = DateTime.Parse(dadosPessoa[1]);

                //Criando a pessoa
                Pessoas pessoa = new Pessoas()
                {
                    Nome = nome,
                    DataAniversario = dataNasc
                };

                //adicionando a lista
                pessoasCadastradasNoArq.Add(pessoa);
            }
            return pessoasCadastradasNoArq;
        }

       public static IEnumerable<Pessoas> PessoasNoSistema(string nome)
        {
            return (from x in PessoasNoSistema()
                    where x.Nome.Contains(nome)
                    orderby x.Nome
                    select x);

        }
        public static  IEnumerable<Pessoas> PessoasNoSistema(DateTime data)
        {
            return (from x in PessoasNoSistema()
                    where x.DataAniversario.Date.Equals(data)
                    orderby x.Nome
                    select x);
        }
        public static Pessoas BuscarPeloNome(string nomeCompleto)
        {
            return (from x in PessoasNoSistema()
                    where x.Nome == nomeCompleto
                    select x).FirstOrDefault();
        }

    }
}


