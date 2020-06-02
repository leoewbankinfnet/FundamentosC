using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TP03___Leonardo_Ewbank
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuPrincipal();
        }


        public static void MenuPrincipal()
        {
            Escrever("Lista de Aniversariantes");
            Escrever("Menu principal");
            Escrever("Selecione uma opção:");
            Escrever("1 - Cadastrar Pessoa");
            Escrever("2 - Consultar Pessoa");
            Escrever("3 - Sair");

            char operacao = Console.ReadLine().ToCharArray()[0];

            if (operacao == '1')
            {
                CadastroPessoa();
            }
            else if (operacao =='2')
            {
                ConsultarPessoa();
            }
            else
            {
                Escrever("Operação Inválida");
                MenuPrincipal();
            }

        }
        static void Escrever(string texto)
        {
            Console.WriteLine(texto);
        }
        static void Limpar()
        {
            Console.Clear();
        }
        public static void CadastroPessoa()
        {
            Limpar();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Escrever("Ficha de Cadastro, Preencha os dados solicitados(nome e sobrenome e data de aniversário)");
            Console.ForegroundColor = ConsoleColor.Red;
            Escrever("Digite o nome da Pessoa");
            string nome = Console.ReadLine();

            Escrever("Digite o sobrenome da Pessoa");
            string sobrenome = Console.ReadLine();

            nome = nome + " " + sobrenome;

            Escrever("Entre a Data de Nascimento da Pessoa");
            DateTime aniversario = DateTime.Parse(Console.ReadLine());


            var pessoa = new Pessoas();

            pessoa.Nome = nome;
         
            pessoa.DataAniversario = aniversario;

            BancoDeDados.Salvar(pessoa);

         

            Escrever("Cadastrado Com Sucesso");
            System.Threading.Thread.Sleep(4000);
            Limpar();

            MenuPrincipal();
    
        }
        public static void ConsultarPessoa()
        {
            Limpar();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Escrever("Consultar Pessoa");
            Console.ForegroundColor = ConsoleColor.Red;
            Escrever("Escolha uma opção abaixo para a consulta:");
            Escrever("1 - Listar todas as pessoas cadastradas");
            Escrever("2 - Buscar por Nome");

            char operacao = Console.ReadLine().ToCharArray()[0];

            if (operacao == '1')
            {
                ListarTodos();
            }
            else if (operacao == '2')
            {
                BuscarPorNome();
            }
            else
            {
                Escrever("Operação Inválida, retornando ao menu principal");
                Limpar();
                MenuPrincipal();
             
            }
        }

        static void ListarTodos()
        {
            Limpar();
            foreach (var pessoa in BancoDeDados.PessoasNoSistema() )
            {
                Escrever($"{BancoDeDados.PessoasNoSistema().IndexOf(pessoa)}) {pessoa.Nome}");
            }
            Escrever("Escolha um numero para ver mais detalhes ou 0 para retornar ao menu principal");

            int operacao = int.Parse(Console.ReadLine());

            if (operacao == '0')
            {
                MenuPrincipal();
            }
            else
            {
                foreach(var pessoa in BancoDeDados.PessoasNoSistema())
                {
                    if(BancoDeDados.PessoasNoSistema().IndexOf(pessoa) == operacao)
                    {
                        Escrever($"Nome: {pessoa.Nome}, Data de Nascimento {pessoa.DataAniversario}");
                        TempoParaAniversario(pessoa.DataAniversario,pessoa.Nome);
                        Escrever("Pressione uma tecla para retornar ao menu principal");
                        Console.ReadKey();
                        Limpar();
                        MenuPrincipal();

                    }
                }
            }

        }
        static void BuscarPorNome()
        {
            Escrever("Busca por Nome");
            Escrever("Digite o nome ou sobrenome da Pessoa");
            string nome = Console.ReadLine();

            var pessoasBuscadas = BancoDeDados.PessoasNoSistema().Where(pessoa => pessoa.Nome.Contains(nome,StringComparison.InvariantCultureIgnoreCase));
            var pessoasEncontradas = new List<Pessoas>();

            foreach(var pessoa in pessoasBuscadas)
            {
                pessoasEncontradas.Add(pessoa);
            }

            foreach(var pessoa in pessoasEncontradas)
            {
                Console.WriteLine($"{pessoasEncontradas.IndexOf(pessoa)}){pessoa.Nome}");
            }

            Escrever("Escolha um numero para ver mais detalhes");
            Escrever(" ");

            int operacao = int.Parse(Console.ReadLine());

           
                foreach (var pessoa in pessoasEncontradas)
                {
                    if (pessoasEncontradas.IndexOf(pessoa) == operacao)
                    {
                        Escrever($"Nome: {pessoa.Nome}, Data de Nascimento {pessoa.DataAniversario}");
                        TempoParaAniversario(pessoa.DataAniversario, pessoa.Nome);
                        Escrever("Pressione uma tecla para retornar ao menu principal");
                        Console.ReadKey();
                        Limpar();
                    }
                }
            
            MenuPrincipal();
        }
        

        static void TempoParaAniversario(DateTime pessoa, string nome)
        {
            DateTime hoje = DateTime.Today.Date;
            DateTime prox = new DateTime(hoje.Year,pessoa.Month,pessoa.Day );
            if (prox < hoje)
            {
                prox = prox.AddYears(1);
            }
            int faltam = (prox - hoje).Days;
            Escrever($"Faltam {faltam} dias para o aniversário da {nome}");
        }

    }
}
